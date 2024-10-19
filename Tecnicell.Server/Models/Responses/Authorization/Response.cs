namespace Tecnicell.Server.Models.Responses.Authorization
{
    public class Response
    {
        public int Success { get; set; }
        public string? Message { get; set; }
        public UserResponse? user { get; set; }
    }
}
