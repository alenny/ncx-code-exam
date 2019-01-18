namespace Ncx.Exam.Api.Responses
{
    public class AuthenticationResponse
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string JwtToken { get; set; }
    }
}