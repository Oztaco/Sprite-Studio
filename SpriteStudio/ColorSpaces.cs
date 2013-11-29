using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteStudio
{
    public class ColorSpaces
    {
        public struct SuperColor
        {
            public SuperColor(float Red, float Green, float Blue)
            {
                RGBColor temp = new RGBColor(Red, Green, Blue);
                _RGBcolor = temp;
                _HSVcolor = Converters.RGBtoHSV(temp);
            }
            public SuperColor(float Red, float Green, float Blue, float Alpha)
            {
                RGBColor temp = new RGBColor(Red, Green, Blue, Alpha);
                _RGBcolor = temp;
                _HSVcolor = Converters.RGBtoHSV(temp);
            }
            public SuperColor(RGBColor color)
            {
                _RGBcolor = color;
                _HSVcolor = Converters.RGBtoHSV(color);
            }
            public SuperColor(HSVColor color)
            {
                _HSVcolor = color;
                _RGBcolor = Converters.HSVtoRGB(color);
            }
            public SuperColor(System.Windows.Media.Color color)
            {
                _RGBcolor = new RGBColor(color.R, color.G, color.B, color.A);
                _HSVcolor = Converters.RGBtoHSV(_RGBcolor);
            }

            private RGBColor _RGBcolor;
            private HSVColor _HSVcolor;
            public RGBColor RGBcolor
            {
                get
                {
                    return _RGBcolor;
                }
                set
                {
                    _RGBcolor = value;
                    RecalculateFromRGB();
                }
            }
            public HSVColor HSVcolor
            {
                get
                {
                    return _HSVcolor;
                }
                set
                {
                    _HSVcolor = value;
                    RecalculateFromHSV();
                }
            }

            public float Red
            {
                get
                {
                    return _RGBcolor.Red;
                }
                set
                {
                    _RGBcolor.Red = value;
                    RecalculateFromRGB();
                }
            }
            public float Green
            {
                get
                {
                    return _RGBcolor.Green;
                }
                set
                {
                    _RGBcolor.Green = value;
                    RecalculateFromRGB();
                }
            }
            public float Blue
            {
                get
                {
                    return _RGBcolor.Blue;
                }
                set
                {
                    _RGBcolor.Blue = value;
                    RecalculateFromRGB();
                }
            }
            public float Hue
            {
                get
                {
                    return _HSVcolor.Hue;
                }
                set
                {
                    _HSVcolor.Hue = value;
                    RecalculateFromHSV();
                }
            }
            public float Sat
            {
                get
                {
                    return _HSVcolor.Saturation;
                }
                set
                {
                    _HSVcolor.Saturation = value;
                    RecalculateFromHSV();
                }
            }
            public float Val
            {
                get
                {
                    return _HSVcolor.Value;
                }
                set
                {
                    _HSVcolor.Value = value;
                    RecalculateFromHSV();
                }
            }
            public float Alpha
            {
                get
                {
                    return _RGBcolor.Alpha;
                }
                set
                {
                    _RGBcolor.Alpha = value;
                    _HSVcolor.Alpha = value;
                }
            }

            public void SetRGB(float R, float G, float B)
            {
                _RGBcolor.Red = R;
                _RGBcolor.Green = G;
                _RGBcolor.Blue = B;
                RecalculateFromRGB();
            }
            public void SetRGBA(float R, float G, float B, float A)
            {
                SetRGB(R, G, B);
                Alpha = A;
            }
            public void SetHSV(float H, float S, float V)
            {
                _HSVcolor.Hue = H;
                _HSVcolor.Saturation = S;
                _HSVcolor.Value = V;
                RecalculateFromHSV();
            }
            public void SetHSVA(float H, float S, float V, float A)
            {
                SetHSV(H, S, V);
                Alpha = A;
            }

            public static implicit operator System.Windows.Media.Color(SuperColor s)
            {
                //System.Windows.Forms.MessageBox.Show(String.Concat((byte)s.Red, "...", (byte)s.Green, "...",  (byte)s.Blue, "...",  (byte)s.Alpha));
                return System.Windows.Media.Color.FromArgb((byte)s.Alpha, (byte)s.Red, (byte)s.Green, (byte)s.Blue);
            }

            public void RecalculateFromRGB()
            {
                _HSVcolor = Converters.RGBtoHSV(_RGBcolor);
            }
            public void RecalculateFromHSV()
            {
                _RGBcolor = Converters.HSVtoRGB(_HSVcolor);
            }


            public static void Test()
            {
                //HSVColor a = new HSVColor(154, 71, 66, 72);
                //RGBColor b = Converters.HSVtoRGB(a);
                RGBColor a = new RGBColor(165, 75, 25);
                SuperColor b = new SuperColor(a);
                System.Windows.Forms.MessageBox.Show(string.Concat(b.Red, " , ", b.Green, " , ", b.Blue + " , ", b.Alpha));
            }

            public class Converters
            {
                public static RGBColor HSVtoRGB(HSVColor color)
                {
                    float thue, tsat, tval,
                          R, G, B;
                    R = 0f;
                    G = 0f;
                    B = 0f;
                    // Temporarily Convert to scale from 0-1
                    thue = color.Hue / 360f;
                    tsat = color.Saturation / 100f;
                    tval = color.Value / 100f;

                    if (tsat == 0)
                    {
                        R = tval;
                        G = tval;
                        B = tval;
                    }
                    else
                    {
                        float hextHue = thue * 6f; // Decimal value
                        if (hextHue == 6) hextHue = 0f;
                        int HexHue = (int)Math.Floor(hextHue); // Rounded value

                        float var1, var2, var3;
                        var1 = tval * (1f - tsat);
                        var2 = tval * (1f - tsat * (hextHue - HexHue));
                        var3 = tval * (1f - tsat * (1f - (hextHue - HexHue)));

                        switch (HexHue)
                        {
                            case 0: R = tval; G = var3; B = var1; break;
                            case 1: R = var2; G = tval; B = var1; break;
                            case 2: R = var1; G = tval; B = var3; break;
                            case 3: R = var1; G = var2; B = tval; break;
                            case 4: R = var3; G = var1; B = tval; break;
                            default: R = tval; G = var1; B = var2; break;
                        }
                    }
                    return new RGBColor(R * 255f, G * 255f, B * 255f, color.Alpha);
                }
                public static HSVColor RGBtoHSV(RGBColor color)
                {
                    float tred, tgreen, tblue,
                        Hue, Sat, Val;
                    Hue = 0;
                    Sat = 0;
                    Val = 0;
                    tred = color.Red / 255f;
                    tgreen = color.Green / 255f;
                    tblue = color.Blue / 255f;

                    float Min, Max, Delta;
                    Min = Math.Min(Math.Min(tred, tblue), tgreen);
                    Max = Math.Max(Math.Max(tred, tblue), tgreen);
                    Delta = Max - Min;
                    Val = Max;

                    if (Delta == 0)
                    {
                        Hue = 0;
                        Sat = 0;
                    }
                    else
                    {
                        Sat = Delta / Max;

                        float dred, dgreen, dblue;
                        dred = (((Max - tred) / 6f) + (Delta / 2f)) / Delta;
                        dgreen = (((Max - tgreen) / 6f) + (Delta / 2f)) / Delta;
                        dblue = (((Max - tblue) / 6f) + (Delta / 2f)) / Delta;

                        if (tred == Max) Hue = dblue - dgreen;
                        else if (tgreen == Max) Hue = (1f / 3f) + dred - dblue;
                        else if (tblue == Max) Hue = (2f / 3f) + dgreen - dred;

                        if (Hue < 0) Hue += 1f;
                        if (Hue > 1) Hue -= 1f;
                    }
                    return new HSVColor(Hue * 360f, Sat * 100f, Val * 100f, color.Alpha);
                }
            }
        }
        public struct HSVColor
        {
            /// <summary>
            /// Creates a new, fully opaque HSLColor.
            /// </summary>
            /// <param name="hue">0-360 scale. Shows the pure color of a color without it's brightness, etc. Example: 0=red 180=cyan</param>
            /// <param name="sat">0-100 scale. "How much" of a color there is. Or how vibrant the color is.</param>
            /// <param name="lum">0-100 scale. How bright the color is. 0=black 100=white 50=pure color.</param>
            public HSVColor(float hue, float sat, float val)
            {
                _Hue = hue;
                _Saturation = sat;
                _Value = val;
                _Alpha = 100;
            }
            /// <summary>
            /// Creates a new HSLColor.
            /// </summary>
            /// <param name="hue">0-360 scale. Shows the pure color of a color without it's brightness, etc. Example: 0=red 180=cyan</param>
            /// <param name="sat">0-100 scale. "How much" of a color there is. Or how vibrant the color is.</param>
            /// <param name="lum">0-100 scale. How bright the color is. 0=black 100=white 50=pure color.</param>
            /// <param name="alpha">0-100 scale. How transparent the color is. 0=invisible 100=fully opaque.</param>
            public HSVColor(float hue, float sat, float val, float alpha)
            {
                _Hue = hue;
                _Saturation = sat;
                _Value = val;
                _Alpha = alpha;
            }
            private float _Hue, _Saturation, _Value, _Alpha;
            public float Hue
            {
                get
                {
                    return _Hue;
                }
                set
                {
                    if (value < 0) _Hue = 0;
                    else if (value > 360) _Hue = 360;
                    else _Hue = value;
                }
            }
            public float Saturation
            {
                get
                {
                    return _Saturation;
                }
                set
                {
                    if (value < 0) _Saturation = 0;
                    else if (value > 360) _Saturation = 360;
                    else _Saturation = value;
                }
            }
            public float Value
            {
                get
                {
                    return _Value;
                }
                set
                {
                    // Careful! Don't confuse C#'s "value" keyword with the color's "Value" variable!
                    if (value < 0) _Value = 0;
                    else if (value > 360) _Value = 360;
                    else _Value = value;
                }
            }
            public float Alpha
            {
                get
                {
                    return _Alpha;
                }
                set
                {
                    if (value < 0) _Alpha = 0;
                    else if (value > 360) _Alpha = 360;
                    else _Alpha = value;
                }
            }
        }
        public struct RGBColor
        {
            /// <summary>
            /// Creates a new RGBColor. (Use for internal reasons, better than system)
            /// </summary>
            /// <param name="red">0-255 scale.</param>
            /// <param name="green">0-255 scale.</param>
            /// <param name="blue">0-255 scale.</param>
            public RGBColor(float red, float green, float blue)
            {
                _Red = red;
                _Green = green;
                _Blue = blue;
                _Alpha = 100;
            }
            /// <summary>
            /// Creates a new RGBColor. (Use for internal reasons, better than system)
            /// </summary>
            /// <param name="red">0-255 scale.</param>
            /// <param name="green">0-255 scale.</param>
            /// <param name="blue">0-255 scale.</param>
            /// <param name="alpha">0-100 scale. How transparent the color is. 0=invisible 100=fully opaque.</param>
            public RGBColor(float red, float green, float blue, float alpha)
            {
                _Red = red;
                _Green = green;
                _Blue = blue;
                _Alpha = alpha;
            }
            private float _Red, _Green, _Blue, _Alpha;
            public float Red
            {
                get
                {
                    return _Red;
                }
                set
                {
                    if (value < 0) _Red = 0;
                    else if (value > 255) _Red = 255;
                    else _Red = value;
                }
            }
            public float Green
            {
                get
                {
                    return _Green;
                }
                set
                {
                    if (value < 0) _Green = 0;
                    else if (value > 255) _Green = 255;
                    else _Green = value;
                }
            }
            public float Blue
            {
                get
                {
                    return _Blue;
                }
                set
                {
                    if (value < 0) _Blue = 0;
                    else if (value > 255) _Blue = 255;
                    else _Blue = value;
                }
            }
            public float Alpha
            {
                get
                {
                    return _Alpha;
                }
                set
                {
                    if (value < 0) _Alpha = 0;
                    else if (value > 255) _Alpha = 255;
                    else _Alpha = value;
                }
            }
        }
    }
}
