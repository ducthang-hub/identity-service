namespace IdentityServer.Helpers;

public static class DirectoryHelper
{
    public static string? GetParentFolder()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        return currentDirectory;
    }
}