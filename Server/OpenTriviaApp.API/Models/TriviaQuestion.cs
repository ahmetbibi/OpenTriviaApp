namespace OpenTriviaApp.API.Models
{
    public class TriviaQuestion
    {
        public required string Id { get; set; }
        public required string Question { get; set; }
        public required List<string> Options { get; set; }
    }
}
