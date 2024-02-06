namespace Services.Common.Models;

public sealed class JwtSettings
{
    public string ValidIssuer { get; set; } = null!;
    public string ValidAudience { get; set; } = null!;
    public string Secret { get; set; } = null!;
    public int ExpireIn { get; set; }

    public static string SectionName = "JwtSettings";
}