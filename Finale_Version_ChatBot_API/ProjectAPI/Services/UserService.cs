using QuestionsAndAnswers.DataLayer.Repository;
using QuestionsAndAnswers.DataLayer.Models;
using System.Security.Cryptography;
using ProjectAPI.Helpers;
using AutoMapper;

namespace ProjectAPI.Services
{
    public class UserService: IUserService
    {
        private readonly ChatBotContext _context;
        private readonly IChatBotContext<User> _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public UserService(ChatBotContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public bool IsUsernameTaken(string username)
        {
            return _context.Users.Any(u => u.Username == username);
        }

        public AuthenticationResponse Authenticate(AuthenticationRequest model)
        {
            //var user = _context.Users.SingleOrDefault(u => u.Username == model.Username);
            var user = _context.Users.FirstOrDefault(u => u.Username == model.Username);

            if (user == null || !VerfyPasswordHash(model.Password, user.Password))
            {
                return null;
            }
            //return user;
            var token = _configuration.GenerateJwtToken(user);
            return new AuthenticationResponse(user, token);
        }
        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetById(long id)
        {
            User appeal = _context.Users.Find(id);
            return appeal;
        }
        /*public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }*/

        /*public User Register(User user, string password)
        {
            if (IsUsernameTaken(user.Username))
            {
                return null;
            }
            user.Password = HashPassword(password);
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }*/
        public async Task<AuthenticationResponse> Register(UserModel userModel/*User user, string password*/)
        {
            if (IsUsernameTaken(userModel.Username))
            {
                return null;
            }
            //var FindUser = _context.Users.FirstOrDefault(u => u.Username == userModel.Username);

            //if(FindUser)
            var user = _mapper.Map<User>(userModel);
            user.Password = HashPassword(user.Password);
            //var addedUser = await _userRepository.Add(user);

            _context.Users.Add(user);
            _context.SaveChanges();

            var response = Authenticate(new AuthenticationRequest
            {
                Username = user.Username,
                Password = userModel.Password
            });
            
            return response;
        }

        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        public static bool VerfyPasswordHash(string password, string hashedPassword)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return AreHashesEqual(buffer3, buffer4);
        }
        static bool AreHashesEqual(byte[] firstHash, byte[] secondHash)
        {
            int _minHashLength = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
            var xor = firstHash.Length ^ secondHash.Length;
            for (int i = 0; i < _minHashLength; i++)
                xor |= firstHash[i] ^ secondHash[i];
            return 0 == xor;
        }
    }
}
