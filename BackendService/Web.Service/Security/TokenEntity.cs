namespace Web.Service.Security
{
    public class TokenEntity
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; } = DateTime.MinValue;
    }
}
