using System.ComponentModel.DataAnnotations;

namespace IdentityServer.SettingOptions;

public sealed class ApplicationSettings
{
    public static string ConfigurationSection => "Application";
 
    [Required]
    public string UrlHttp { get; init; } = string.Empty;

    [Required]
    public string UrlHttps { get; init; } = string.Empty;
}