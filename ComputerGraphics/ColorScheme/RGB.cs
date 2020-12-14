using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerGraphics.ColorScheme
{
    public class RGB
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public RGB()
        {

        }
        public RGB(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }
        public override string ToString()
        {
            return String.Format("({0} ,{1} ,{2})", R.ToString(), G.ToString(), B.ToString());
        }
    }
}
