using QuestionsAndAnswers.DataLayer.Models;

namespace ProjectAPI.Services
{
    public interface IUserService
    {
        //bool IsUsernameTaken(string username);
        //User Authenticate(string username, string password);
        //User Register(User user, string password);
        AuthenticationResponse Authenticate(AuthenticationRequest model);
        Task<AuthenticationResponse> Register(UserModel userModel/*User user, string password*/);
        IEnumerable<User> GetAll();
        User GetById(long id);
    }
}
