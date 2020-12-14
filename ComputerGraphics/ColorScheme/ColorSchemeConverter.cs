using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerGraphics.ColorScheme
{
    public static class ColorSchemeConverter
    {
		public static HSV RGBToHSV(RGB rgb)
		{
			double delta, min;
			double h = 0, s, v;

			min = Math.Min(Math.Min(rgb.R, rgb.G), rgb.B);
			v = Math.Max(Math.Max(rgb.R, rgb.G), rgb.B);
			delta = v - min;

			if (v == 0.0)
				s = 0;
			else
				s = delta / v;

			if (s == 0)
				h = 0.0;

			else
			{
				if (rgb.R == v)
					h = (rgb.G - rgb.B) / delta;
				else if (rgb.G == v)
					h = 2 + (rgb.B - rgb.R) / delta;
				else if (rgb.B == v)
					h = 4 + (rgb.R - rgb.G) / delta;

				h *= 60;

				if (h < 0.0)
					h = h + 360;
			}

			return new HSV(h, s, (v / 255));
		}

		public static CMYK RGBToCMYK(RGB rgb)
		{
			double modifiedR, modifiedG, modifiedB, c, m, y, k;

			modifiedR = rgb.R / 255.0;
			modifiedG = rgb.G / 255.0;
			modifiedB = rgb.B / 255.0;

			k = 1 - new List<double>() { modifiedR, modifiedG, modifiedB }.Max();
			c = (1 - modifiedR - k) / (1 - k);
			m = (1 - modifiedG - k) / (1 - k);
			y = (1 - modifiedB - k) / (1 - k);

			return new CMYK(
				(byte)Math.Round(c * 100),
				(byte)Math.Round(m * 100),
				(byte)Math.Round(y * 100),
				(byte)Math.Round(k * 100));

		}
	}
}
