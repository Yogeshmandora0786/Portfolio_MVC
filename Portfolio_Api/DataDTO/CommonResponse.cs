namespace Portfolio_Api.DataDTO
{
    public class CommonResponse
    {
        public string? Code { get; set; }
        public string? ResponseData { get; set; }
        public string? Message { get; set; }
        public string? Token { get; set; }
        public string? Username { get; set; }
        public string? Role { get; set; }
        public long UserId { get; set; }
        public long PreviousSteps { get; set; }
    }
}
