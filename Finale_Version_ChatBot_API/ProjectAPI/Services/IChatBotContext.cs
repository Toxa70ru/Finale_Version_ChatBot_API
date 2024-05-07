using QuestionsAndAnswers.DataLayer.Models;

namespace ProjectAPI.Services
{
    public interface IChatBotContext<T> where T : BaseEntity
    {
        List<T> GetAll();
        T GetById(long id);
        Task<long> Add(T entity);
    }
}
