using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ComputerGraphics.Fractals
{
	interface IFractal
	{
		public byte[] GetPixels(int width, int height, IColorFactory colorFactory, double scale);
	}
}
