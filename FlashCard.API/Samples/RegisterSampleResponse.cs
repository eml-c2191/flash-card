using FlashCard.Abstract.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace FlashCard.API.Samples
{
    public class RegisterSampleResponse : IExamplesProvider<AuthoriseResponse>
    {
        public AuthoriseResponse GetExamples()
        {
            return new AuthoriseResponse
            {
                AccessToken = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjI1MzA2MDE0MCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL21vYmlsZXBob25lIjoiMjUzMDYwMTQwIiwiZXhwIjoxNjgyNTY3NzY2LCJpc3MiOiJodHRwczovL2lkZW50aXR5LWVtbC5jb20uYXUvIiwiYXVkIjoiaHR0cHM6Ly9pZGVudGl0eS1lbWwuY29tLmF1L2F1ZGllbmNlIn0.pCUZeBQSaRMbQ8d2_r6LH6sdyca5jIDaefE8pBio2kF7fjxujBMhCigOUjUjOKaZ_hAETJ2UF4y6Lwm1CwVeAA",
                RefreshToken = "xltGk30NLwFB138X5gj7U+rgcJloL4jgoKRuS4rQSk/tVOEbFtc7mHSyv5tU0Suoane8pekuVkeQav/fnyKUALVUGo1EdCSp8iG6gRUmxypJZMYXROBbfm/fTNwhbIMIrGtCLRjKOr5MAOH/BspVaV0Pt7LR2DYxzUCiIf7cuNs="
            };
        }
    }
}
