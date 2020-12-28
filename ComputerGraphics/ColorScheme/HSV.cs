using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerGraphics.ColorScheme
{
    public class HSV
    {
        public double H { get; set; }
        public double S { get; set; }
        public double V { get; set; }

        public HSV()
        {

        }
        public HSV(double h, double s, double v)
        {
            H = h;
            S = s;
            V = v;
        }

        public override string ToString()
        {
            return String.Format("({0} ,{1} ,{2})", H.ToString(), S.ToString(), V.ToString());
        }
    }
}
