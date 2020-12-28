using ComputerGraphics.ColorScheme;
using ComputerGraphics.Commands;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace ComputerGraphics.ViewModels
{
	
	

	class ColorVM : BaseViewModel
	{
		private string _fileName;
		private ImageSource _originalImage;
		private ImageSource _resultImage;
		private byte _brightness;
		private byte _saturation;
		private ICommand _mouseMoveCommand;
		private string _rgb;
		private string _hsv;
		private string _cmyk;

		public ColorVM()
		{
			_fileName = "No file selected";
			_fragment = 100;
		}

		public string FileName
		{
			get => _fileName;
			set
			{
				_fileName = value;
				OnPropertyChanged(nameof(FileName));
			}
		}

		public ImageSource OriginalImage
		{
			get
			{
				return _originalImage;
			}
			set
			{
				_originalImage = value;
				OnPropertyChanged(nameof(OriginalImage));
			}
		}


		public string RGB
		{
			get { return _rgb; }
			set
			{
				_rgb = value;
				OnPropertyChanged("RGB");
			}
		}


		public string HSV
		{
			get { return _hsv; }
			set
			{
				_hsv = value;
				OnPropertyChanged("HSV");
			}
		}

		public string CMYK
		{
			get { return _cmyk; }
			set
			{
				_cmyk = value;
				OnPropertyChanged("CMYK");
			}
		}

		public ImageSource ResultImage
		{
			get
			{
				return _resultImage;
			}
			set
			{
				_resultImage = value;
				OnPropertyChanged(nameof(ResultImage));

			}
		}

		public byte Brightness
		{
			get
			{
				return _brightness;
			}
			set
			{
				_brightness = value;
				OnPropertyChanged(nameof(Brightness));
				ResultImage = ChangeSaturation(ChangeBrightness((BitmapImage)OriginalImage, _brightness), _saturation);

			}
		}

		public byte Saturation
		{
			get
			{
				return _saturation;
			}
			set
			{
				_saturation = value;
				OnPropertyChanged(nameof(Saturation));
				ResultImage = ChangeSaturation(ChangeBrightness((BitmapImage)OriginalImage, _brightness), _saturation);
			}
		}

		private byte _fragment;
		public byte Fragment
		{
			get
			{
				return _fragment;
			}
			set
			{
				_fragment = value;
				OnPropertyChanged(nameof(Fragment));
				ResultImage = ChangeSaturation(ChangeBrightness((BitmapImage)OriginalImage, _brightness), _saturation);
			}
		}

		public ICommand NavigateToMain => new RelayCommand((obj) =>
		{
			MainWindow mw = (MainWindow)App.Current.MainWindow;
			mw.MainFrame.Navigate(new Views.MainView());
		});

        public ICommand NavigateToHelp => new RelayCommand((obj) =>
        {
            MainWindow mw = (MainWindow)App.Current.MainWindow;
            mw.MainFrame.Navigate(new Views.Help("Color"));
        });

		public ICommand OpenFile => new RelayCommand((obj) =>
		{
			OpenFileDialog op = new OpenFileDialog();
			op.Title = "Select a picture";
			op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
			  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
			  "Portable Network Graphic (*.png)|*.png";

			if (op.ShowDialog() == true)
			{
				OriginalImage = new BitmapImage(new Uri(op.FileName));
				ResultImage = new WriteableBitmap((BitmapImage)OriginalImage);
				FileName = Path.GetFileName(op.FileName);
			}
		});

		public ICommand SavePhotoCommand => new RelayCommand((obj) =>
		{


			SaveFileDialog sfd = new SaveFileDialog();
			sfd.FileName = "CoolPhoto";
			sfd.DefaultExt = ".png";

			if (sfd.ShowDialog() == true)
			{
				var encoder = new PngBitmapEncoder();
				encoder.Frames.Add(BitmapFrame.Create((BitmapSource)ResultImage));
				using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
					encoder.Save(stream);
			}
		});

		public ICommand MouseMoveCommand
		{
			get
			{
				if (_mouseMoveCommand == null)
				{
					_mouseMoveCommand = new RelayCommand(param => ExecuteMouseMove((MouseEventArgs)param));
				}
				return _mouseMoveCommand;
			}
			set { _mouseMoveCommand = value; }
		}

		private Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
		{
			using (MemoryStream outStream = new MemoryStream())
			{
				BitmapEncoder enc = new BmpBitmapEncoder();
				enc.Frames.Add(BitmapFrame.Create(bitmapImage));
				enc.Save(outStream);
				System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

				return new Bitmap(bitmap);
			}
		}
		private void ExecuteMouseMove(MouseEventArgs e)
		{
			Bitmap Img = BitmapImage2Bitmap((BitmapImage)OriginalImage);
			System.Windows.Point p = e.GetPosition(((IInputElement)e.Source));

			if ((p.X >= 0) && (p.X < Img.Width) && (p.Y >= 0) && (p.Y < Img.Height))
			{
				System.Drawing.Color color = Img.GetPixel((int)p.X, (int)p.Y);
				RGB rgb = new RGB(color.R, color.G, color.B);

				double hue = Math.Round(color.GetHue(), 2);
				double saturation = Math.Round(color.GetSaturation(), 2);
				double brightness = Math.Round(color.GetBrightness(), 2);

				RGB = rgb.ToString();
				HSV hsv = new HSV(hue, saturation, brightness);
				HSV = hsv.ToString();
				CMYK = ColorSchemeConverter.RGBToCMYK(rgb).ToString();

			}
		}

		public WriteableBitmap ChangeBrightness(BitmapImage source, byte change_value)
		{
			WriteableBitmap dest = new WriteableBitmap(source);

			byte[] color = new byte[4];

			int stride = (int)(source.PixelWidth) * (source.Format.BitsPerPixel / 8);
			byte[] pixels = new byte[(int)(source.PixelHeight) * stride];
			source.CopyPixels(pixels, stride, 0);

			for (int i = 0; i < pixels.Length; i += 4)
			{
				
				for (int j = 0; j < 4; j++)
				{
					if(j == 0)
                    {
						color[j] = pixels[i + j];
						if ((int)color[j] + change_value > 255)
							color[j] = 255;
						else
							color[j] = (byte)(color[j] + change_value);
						pixels[i + j] = color[j];
					}
				}
			}

			dest.WritePixels(new Int32Rect(0, 0, (int)(source.PixelWidth * (Fragment / 100.0)), source.PixelHeight), pixels, source.PixelWidth * 4, 0);

			return dest;
		}

		public WriteableBitmap ChangeSaturation(WriteableBitmap source, byte change_value)
        {
			WriteableBitmap dest = new WriteableBitmap(source);

			byte[] color = new byte[4];

			int stride = (int)(source.PixelWidth) * (source.Format.BitsPerPixel / 8);
			byte[] pixels = new byte[(int)(source.PixelHeight) * stride];
			source.CopyPixels(pixels, stride, 0);

			for (int i = 0; i < pixels.Length; i = i + 4)
            {
                if (pixels[i] > 50)
                {
                    pixels[i] = (byte)Math.Min(255, pixels[i] + change_value);
                }
                else
                {
                    pixels[i] = (byte)Math.Max(0, pixels[i] - change_value);
                }
                //if (pixels[i + 1] > 225)
                //{
                //    pixels[i + 1] = (byte)Math.Min(255, pixels[i + 1] + change_value);
                //}
                //else
                //{
                //    pixels[i + 1] = (byte)Math.Max(0, pixels[i + 1] - change_value);
                //}
                //if (pixels[i + 2] > 225)
                //{
                //    pixels[i + 2] = (byte)Math.Min(255, pixels[i + 2] + change_value);
                //}
                //else
                //{
                //    pixels[i + 2] = (byte)Math.Max(0, pixels[i + 2] - change_value);
                //}
            }

            //dest.WritePixels(new Int32Rect(0, 0, (int)(source.PixelWidth * (Fragment / 100.0)), source.PixelHeight), pixels, source.PixelWidth * 4, 0);
			dest.WritePixels(new Int32Rect(0, 0, source.PixelWidth, source.PixelHeight), pixels, source.PixelWidth * 4, 0);
			return dest;
		}
	}
}
