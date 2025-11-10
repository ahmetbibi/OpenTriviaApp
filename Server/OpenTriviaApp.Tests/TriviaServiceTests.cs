using Moq;
using Moq.Protected;
using OpenTriviaApp.API.Models;
using OpenTriviaApp.API.Services;
using System.Net;

namespace OpenTriviaApp.Tests
{
    public class TriviaServiceTests
    {
        [Fact]
        public async Task FetchQuestionsAsync_ReturnsQuestions()
        {
            // Arrange
            var json = @"{
            ""results"": [
                {
                    ""question"": ""What is 2+2?"",
                    ""correct_answer"": ""4"",
                    ""incorrect_answers"": [""3"", ""5"", ""6""]
                }
            ]
        }";

            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(json),
                });

            var httpClient = new HttpClient(handlerMock.Object);
            var factoryMock = new Mock<IHttpClientFactory>();
            factoryMock.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var service = new TriviaService(factoryMock.Object);

            // Act
            var questions = await service.FetchQuestionsAsync();

            // Assert
            Assert.Single(questions);
            Assert.Equal("What is 2+2?", questions[0].Question);
            Assert.Contains("4", questions[0].Options);
        }

        [Fact]
        public void CheckAnswers_ReturnsCorrectness()
        {
            // Arrange
            var factoryMock = new Mock<IHttpClientFactory>();
            var service = new TriviaService(factoryMock.Object);

            // Simulate internal state
            var id = "test-id";
            var correct = "4";
            var field = typeof(TriviaService).GetField("correctAnswers", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            var dict = (Dictionary<string, string>)field.GetValue(null);
            dict[id] = correct;

            var answers = new List<UserAnswer>
        {
            new UserAnswer { Id = id, SelectedAnswer = "4" },
            new UserAnswer { Id = id, SelectedAnswer = "5" },
            new UserAnswer { Id = "invalid-id", SelectedAnswer = "anything" }
        };

            // Act
            var results = service.CheckAnswers(answers);

            // Assert
            Assert.Equal(new List<string> { "Correct", "Incorrect", "Invalid Question ID" }, results);
        }
    }
}
