using System.ComponentModel.DataAnnotations;

namespace IdentityServer.SettingOptions;

public class IdentityServerSettings
{
    public static string ConfigurationSection => "IdentityServer";
 
    [Required]
    public string ClientId { get; init; } = string.Empty;

}