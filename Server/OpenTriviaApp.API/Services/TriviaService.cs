using System.Net.Http;
using System.Text.Json;
using OpenTriviaApp.API.Models;

namespace OpenTriviaApp.API.Services
{
    public class TriviaService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private static readonly Dictionary<string, string> correctAnswers = [];

        public TriviaService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<TriviaQuestion>> FetchQuestionsAsync()
        {
            //using var client = new HttpClient();
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetStringAsync("https://opentdb.com/api.php?amount=5&type=multiple");
            var jsonDoc = JsonDocument.Parse(response);
            var questions = new List<TriviaQuestion>();

            foreach (var item in jsonDoc.RootElement.GetProperty("results").EnumerateArray())
            {
                var id = Guid.NewGuid().ToString();
                var question = item.GetProperty("question").GetString();
                var correct = item.GetProperty("correct_answer").GetString();

                if (string.IsNullOrEmpty(question) || string.IsNullOrEmpty(correct))
                    continue;

                var incorrect = item.GetProperty("incorrect_answers")
                    .EnumerateArray()
                    .Select(x => x.GetString())
                    .Where(x => !string.IsNullOrEmpty(x))
                    .Select(x => x!)
                    .ToList();

                incorrect.Add(correct);
                incorrect = incorrect.OrderBy(_ => Guid.NewGuid()).ToList();

                correctAnswers[id] = correct;

                questions.Add(new TriviaQuestion
                {
                    Id = id,
                    Question = question,
                    Options = incorrect
                });
            }
            return questions;
        }

        public List<string> CheckAnswers(List<UserAnswer> answers)
        {
            var results = new List<string>();
            foreach (var answer in answers)
            {
                if (correctAnswers.TryGetValue(answer.Id, out var correct))
                {
                    results.Add(answer.SelectedAnswer == correct ? "Correct" : "Incorrect");
                }
                else
                {
                    results.Add("Invalid Question ID");
                }
            }
            return results;
        }
    }
}
