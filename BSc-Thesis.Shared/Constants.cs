using System.Reflection;

namespace BSc_Thesis.Shared;

public static class Constants
{
    private static readonly AssemblyName? AssemblyName = Assembly.GetEntryAssembly()?.GetName();
    public static readonly string AppFriendlyName = "BSc-Thesis";
    public static readonly string AppName = AssemblyName?.Name ?? AppFriendlyName.Replace(" ", "");
    public static readonly string BaseDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;
    public static readonly string TempPath = Path.Combine(Path.GetTempPath(), AppName);
    public static readonly string? Version = Assembly.GetEntryAssembly()?.GetName().Version?.ToString(3);
}