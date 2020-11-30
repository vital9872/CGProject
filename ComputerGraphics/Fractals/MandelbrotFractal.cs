using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ComputerGraphics.Fractals
{
	class MandelbrotFractal: IFractal
	{
		Complex _c;

		public Complex Function(Complex z) => z * z + _c;
		public static bool IsInBounds(Complex z) => z.Magnitude <= 2;

		public byte[] GetPixels(int height, int width, IColorFactory colorFactory, double scale)
		{
			int size = height * width;
			byte[] pixels = new byte[size * 4];

			int p = 0;
			for (int x = 0; x < width; ++x)
			{
				for (int y = 0; y < height; ++y)
				{
					double a = (double)(x - width / 2) / (width / (4 * scale));
					double b = (double)(y - height / 2) / (height / (4 * scale));
					_c = new Complex(b, a);
					Complex z = new Complex(0, 0);
					int i;
					int iterations = 255;
					for (i = 0; i < iterations; ++i)
					{
						z = Function(z);
						if (!IsInBounds(z))
							break;
					}
					
					colorFactory.SetDepth(i);
					for (int j = 0; j < 4; ++j)
						pixels[p++] = colorFactory.GetPixel(j);
				}
			}
			
			return pixels;
		}
	}
}
