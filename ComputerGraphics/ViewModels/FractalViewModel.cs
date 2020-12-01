using ComputerGraphics.Commands;
using ComputerGraphics.Fractals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ComputerGraphics.ViewModels
{
	public class FractalViewModel : BaseViewModel
	{
		double _real, _imaginatory, _scale;
		int _fractalType, _schemeIndex;
		Visibility _parameterInputVisibility;

		public double Scale
		{
			get => _scale;
			set
			{
				_scale = value;
				OnPropertyChanged(nameof(Scale));
			}
		}

		public int SchemeIndex
		{
			get => _schemeIndex;
			set
			{
				_schemeIndex = value;
				OnPropertyChanged(nameof(SchemeIndex));
			}
		}

		public int FractalType 
		{
			get => _fractalType;
			set
			{
				_fractalType = value;
				OnPropertyChanged(nameof(FractalType));
				ParameterVisibility = (_fractalType > 0) ? 
					Visibility.Visible : Visibility.Collapsed;
			}
		}

		public ImageSource CanvasSource { get; set; }

		public int ImageWidth { get; set; }
		public int ImageHeight { get; set; }

		public Visibility ParameterVisibility
		{
			get => _parameterInputVisibility;
			set
			{
				_parameterInputVisibility = value;
				OnPropertyChanged(nameof(ParameterVisibility));
			}
		}

		public double Real
		{
			get => _real;
			set
			{
				_real = value;
				OnPropertyChanged(nameof(Real));
			}
		}

		public double Imaginatory
		{
			get => _imaginatory;
			set
			{
				_imaginatory = value;
				OnPropertyChanged(nameof(Imaginatory));
			}
		}

		public ICommand NavigateToMain => new RelayCommand((obj) =>
		{
			MainWindow mw = (MainWindow)App.Current.MainWindow;
			mw.MainFrame.Navigate(new Views.MainView());
		});

		public ICommand DrawCommand => new RelayCommand((obj) =>
		{
			App.Current.MainWindow.Cursor = Cursors.Wait;
			IFractal fractal;
			if (FractalType == 0)
				fractal = new MandelbrotFractal();
			else
				fractal = new JuliaFractal(new System.Numerics.Complex(_real, _imaginatory));

			WriteableBitmap wbitmap = new WriteableBitmap(
			   ImageWidth, ImageHeight, 96, 96, PixelFormats.Bgra32, null);

			Int32Rect rect = new Int32Rect(0, 0, ImageWidth, ImageHeight);
			int stride = 4 * ImageWidth;
			IColorFactory colorFactory;
			if (_schemeIndex > 0)
				colorFactory = new WarmColorFactory();
			else
				colorFactory = new ColdColorFactory();
			wbitmap.WritePixels(rect, fractal.GetPixels(ImageWidth, ImageHeight, colorFactory, _scale), stride, 0);
			CanvasSource = wbitmap;
			OnPropertyChanged(nameof(CanvasSource));
			App.Current.MainWindow.Cursor = Cursors.Arrow;
		});

		public ICommand ResetCommand => new RelayCommand((obj)=>
		{ 
			CanvasSource = null;
			OnPropertyChanged(nameof(CanvasSource));
		});

		public FractalViewModel()
		{
			FractalType = 0;
			ImageHeight = 1000;
			ImageWidth = 1200;
			Scale = 1;
		}
	}
}
