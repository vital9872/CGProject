using ComputerGraphics.Fractals;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ComputerGraphics.Fractals
{
	class JuliaFractal: IFractal
	{
		Complex _c;
		double _r;
		public JuliaFractal(Complex c)
		{
			_c = c;
			_r = (1 + Math.Sqrt(1 + 4 * _c.Magnitude)) / 2;
		}

		public Complex Function(Complex z) => z * z + _c;
		public bool IsInBounds(Complex z) => z.Magnitude <= _r;

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
					Complex z = new Complex(b, a);
					int i;
					int iterations = 127;
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
