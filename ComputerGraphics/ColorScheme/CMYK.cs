using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerGraphics.ColorScheme
{
    public class CMYK
    {
        public byte C { get; set; }
        public byte M { get; set; }
        public byte Y { get; set; }
        public byte K { get; set; }

        public CMYK()
        {

        }
        public CMYK(byte c, byte m, byte y, byte k)
        {
            C = c;
            M = m;
            Y = y;
            K = k;
        }

        public override string ToString()
        {
            return String.Format("({0} ,{1} ,{2}, {3})", C.ToString(), M.ToString(), Y.ToString(), K.ToString());
        }
    }
}
