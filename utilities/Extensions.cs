using Godot;

namespace Utilities;

public static class Extensions
{
    public static Color ToColor(this (int r, int g, int b) x) => new Color(x.r / 255f, x.g / 255f, x.b / 255f);

    private static Variant LoadVariant(this ConfigFile configFile, string section, string key, Variant defaultValue = default)
    {
        return configFile.HasSectionKey(section, key) ? configFile.GetValue(section, key) : defaultValue;
    }

    public static int LoadInt(this ConfigFile configFile, string section, string key, int defaultValue = 0)
    {
        return (int)configFile.LoadVariant(section, key, defaultValue);
    }
}
