using Godot;

namespace Utilities;

public static class Extensions
{
    public static Color ToColor(this (int r, int g, int b) x) => new Color(x.r / 255f, x.g / 255f, x.b / 255f);
}
