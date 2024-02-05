namespace WebApi.Common.Configurations;

public sealed class JwtSettings
{
    public string ValidIssuer { get; private set; } = null!;
    public string ValidAudience { get; private set; } = null!;
    public string Secret { get; private set; } = null!;
    public int ExpireIn { get; private set; }

    public static string SectionName = "JwtSettings";
}