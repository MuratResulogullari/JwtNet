namespace JwtNet.WebAPI.Models.Dtos
{
    public class TokenResponse
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
