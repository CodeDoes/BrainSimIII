using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace BrainSimulatorAvalonia
{
    // Extension methods for IList<T>
    public static class IListExtensions
    {
        public static T FindFirst<T>(this IList<T> source, Func<T, bool> condition)
        {
            foreach (T item in source)
                if (condition(item))
                    return item;
            return default(T);
        }
        public static List<T> FindAll<T>(this IList<T> source, Func<T, bool> condition)
        {
            List<T> theList = new List<T>();
            if (source == null) return theList;
            foreach (T item in source)
                if (condition(item))
                    theList.Add(item);
            return theList;
        }
    }

    // Range class (not used)
    class Range
    {
        float minX;
        float minY;
        float maxX;
        float maxY;
        public Range(Avalonia.Point loc, double angle, float length)
        {
            minX = (float)loc.X;
            minY = (float)loc.Y;
            maxX = minX + (float)Math.Cos(angle) * length;
            maxY = minY + (float)Math.Sin(angle) * length;
            if (minX > maxX) { float temp = minX; minX = maxX; maxX = temp; }
            if (minY > maxY) { float temp = minY; minY = maxY; maxY = temp; }
        }
        public bool Overlaps(Range r2, float minOverlap = 0)
        {
            if (r2.minX > maxX + minOverlap) return false;
            if (r2.minY > maxY + minOverlap) return false;
            if (r2.maxX < minX - minOverlap) return false;
            if (r2.maxY < minY - minOverlap) return false;
            return true;
        }
    }

    // HSLColor class (ported, but System.Drawing.Color used for color math)
    public class HSLColor
    {
        public float hue;
        public float saturation;
        public float luminance;
        public HSLColor() { }
        public HSLColor(float h, float s, float l) { hue = h; saturation = s; luminance = l; }
        public HSLColor(Dictionary<string, float> values)
        {
            hue = 0; saturation = 0; luminance = 0;
            try { hue = values["Hue+"]; saturation = values["Sat+"]; luminance = values["Lum+"]; } catch { }
        }
        public HSLColor(byte a, byte r, byte g, byte b)
        {
            var c1 = System.Drawing.Color.FromArgb(255, r, g, b);
            hue = c1.GetHue(); saturation = c1.GetSaturation(); luminance = c1.GetBrightness();
        }
        public HSLColor(System.Drawing.Color c)
        {
            hue = c.GetHue(); saturation = c.GetSaturation(); luminance = c.GetBrightness();
        }
        public HSLColor(HSLColor c)
        {
            if (c == null) return;
            hue = c.hue; saturation = c.saturation; luminance = c.luminance;
        }
        public override string ToString() => $"H:{hue:f2} S:{saturation:f2} L:{luminance:f2}";
        public static float operator -(HSLColor c1, HSLColor c2)
        {
            if (c1.luminance > 0.95) c1.hue = 0.5f;
            else if (c1.luminance < .1) c1.hue = 0.5f;
            else if (c1.saturation < .1) c1.hue = 0.5f;
            if (c2.luminance > 0.95) c2.hue = 0.5f;
            else if (c2.luminance < .1) c2.hue = 0.5f;
            else if (c2.saturation < .1) c2.hue = 0.5f;
            float diff = Math.Abs(c1.hue - c2.hue) * 5 + Math.Abs(c1.saturation - c2.saturation) + Math.Abs(c1.luminance - c2.luminance);
            diff /= 7;
            return diff;
        }
        public bool Equals(HSLColor c1)
        {
            if (c1 == null) return false;
            if (luminance < .05 && c1.luminance < .05) return true;
            if (luminance > .95 && c1.luminance > .95) return true;
            float absHueDiff = Math.Abs(hue - c1.hue);
            if (absHueDiff < 5 || absHueDiff > 355) return true;
            return false;
        }
    }

    public static class Utils
    {
        public static string RebaseFolderToCurrentDevEnvironment(string fullPath)
        {
            int index = fullPath.ToLower().IndexOf("\\networks\\");
            if (index != -1)
            {
                fullPath = fullPath.Substring(index);
                string Path1 = Path.GetFullPath(".");
                string Path2 = Path1.Replace("\\bin\\Debug\\net6.0-windows", "");
                fullPath = Path2 + fullPath;
            }
            return fullPath;
        }
        // TODO: Port additional utility methods from WPF version as needed.
        // Many methods in the original Utils.cs depend on WPF types (e.g., Visual, Control, Brushes, RoutedEventHandler, etc.).
        // These require Avalonia equivalents or reimplementation.
    }
}
