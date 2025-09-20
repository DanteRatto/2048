using Godot;

namespace Utilities;

public static class Extensions
{
    public static Color ToColor(this (int r, int g, int b) x) => new Color(x.r / 255f, x.g / 255f, x.b / 255f);

    public static void SetAndSave(this ConfigFile configFile, string saveFile, string section, string key, Variant value)
    {
        configFile.SetValue(section, key, value);
        configFile.Save(saveFile);
    }

    private static Variant LoadVariant(this ConfigFile configFile, string saveFile, string section, string key, Variant defaultValue = default)
    {
        return configFile.Load(saveFile) == Error.Ok && configFile.HasSectionKey(section, key) ? configFile.GetValue(section, key) : defaultValue;
    }

    public static int LoadInt(this ConfigFile configFile, string saveFile, string section, string key, int defaultValue = 0)
    {
        return (int)configFile.LoadVariant(saveFile, section, key, defaultValue);
    }
}
