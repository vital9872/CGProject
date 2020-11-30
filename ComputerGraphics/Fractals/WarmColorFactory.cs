using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerGraphics.Fractals
{
	class WarmColorFactory : IColorFactory
	{
		byte[] pixels;
		public WarmColorFactory()
		{
			pixels = new byte[4];
			pixels[0] = 0;
			pixels[1] = 0;
			pixels[2] = 255;
			pixels[3] = 255;
		}

		public byte GetPixel(int index) => pixels[index];

		public void SetDepth(int depth)
		{
			if (depth < 10)
			{
				pixels[0] = 0;
				pixels[1] = 16;
				pixels[3] = 32;
			}
			else if (depth < 50)
			{
				pixels[0] = 0;
				pixels[1] = 32;
				pixels[3] = 255;
			}
			else if (depth < 100)
			{
				pixels[0] = 0;
				pixels[1] = 64;
				pixels[3] = 127;
			}
			else if (depth < 150)
			{
				pixels[0] = 0;
				pixels[1] = 127;
				pixels[3] = 255;
			}
			else
			{
				pixels[0] = 0;
				pixels[1] = 255;
				pixels[3] = 255;
			}
		}
	}
}
