using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerGraphics.Fractals
{
	interface IColorFactory
	{
		byte GetPixel(int index);
		void SetDepth(int depth);
	}
}
