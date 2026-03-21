using System;
using Godot;
using Vector4 = System.Numerics.Vector4;

namespace Util
{
    public static class Rainbow
    {
        public static Vector4 Hue(float v)
        {
            float num = 6.28318548f * v;
            Vector4 one = Vector4.One;
            one.X = (float)(Math.Sin(num) * 0.5 + 0.5);
            one.Y = (float)(Math.Sin(num + 2.09439516f) * 0.5 + 0.5);
            one.Z = (float)(Math.Sin(num + 4.18879032f) * 0.5 + 0.5);
            one.W = 1f;
            return one;
        }

        public static Color HueToColor(float? time = null)
        {
            time ??= (float)DateTime.Now.TimeOfDay.TotalSeconds;
            Vector4 color = Hue((float)(time % 3.0) / 3f);
            return new Color(color.X, color.Y, color.Z, color.W);
        }
    }
}
