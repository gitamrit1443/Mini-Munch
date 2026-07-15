namespace MiniMunch.Web.Services;

public class JwtOptions
{
    public string Issuer { get; set; } = "MiniMunchMvc";
    public string Audience { get; set; } = "MiniMunchMvc.Client";
    public string SecretKey { get; set; } = "CHANGE_THIS_DEMO_SECRET_KEY_32_CHARS_MINIMUM_2026";
    public int AccessTokenMinutes { get; set; } = 120;
}
