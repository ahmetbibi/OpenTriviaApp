using OpenTriviaApp.API.Models;

namespace OpenTriviaApp.API.Services
{
    public interface ITriviaService
    {
        Task<List<TriviaQuestion>> FetchQuestionsAsync();
        List<string> CheckAnswers(List<UserAnswer> answers);
    }
}
