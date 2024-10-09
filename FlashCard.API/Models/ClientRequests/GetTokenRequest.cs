namespace FlashCard.API.Models.ClientRequests
{
    public record GetTokenRequest
    {
        public IEnumerable<KeyValuePair<string, string>>? Claims { get; set; }
    }
}
