using ComputerGraphics.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ComputerGraphics.ViewModels
{
	class AffiineVM: BasePropertyChanged
	{
		private Point left, right;
		private double[][] square;
		private DrawingImage affineImage;
		private double xDir, yDir;

		private double angle, scale;
		public AffiineVM()
		{
			left = new Point(0, 0);
			right = new Point(10, 10);
			scale = 1;
			xDir = 0;
			yDir = 0;
			DrawAffine();
		}

		private void SetSquare()
		{
			double side = Math.Abs(right.X - left.X);
			square = new double[4][];
			for(int i = 0; i < 4; ++i)
			{
				square[i] = new double[3];
				square[i][2] = 1;
			}
			//square
			square[0][0] = left.X;
			square[0][1] = left.Y;

			square[1][0] = left.X;
			square[1][1] = left.X + side;

			square[2][0] = right.X;
			square[2][1] = right.Y;

			square[3][0] = right.X;
			square[3][1] = right.Y - side;

			square = MultiplyMatrixes(square, GetTransformationMatrix());
		}

		private double[][] GetTransformationMatrix()
		{
			//angle
			double rad = Angle * Math.PI / 180;
			double[][] angleMatrix = new double[3][];
			for(int i = 0; i < 3; ++i)
				angleMatrix[i] = new double[3];

			angleMatrix[0][0] = Math.Cos(rad);
			angleMatrix[0][1] = Math.Sin(rad);
			angleMatrix[0][2] = 0;


			angleMatrix[1][0] = -Math.Sin(rad);
			angleMatrix[1][1] = Math.Cos(rad);
			angleMatrix[1][2] = 0;

			angleMatrix[2][0] = 0;
			angleMatrix[2][1] = 0;
			angleMatrix[2][2] = 1;

			double[][] scaleMatrix = new double[3][];
			for(int i = 0; i < 3; ++i)
				scaleMatrix[i] = new double[3];

			scaleMatrix[0][0] = scale;
			scaleMatrix[0][1] = 0;
			scaleMatrix[0][2] = 0;

			scaleMatrix[1][0] = 0;
			scaleMatrix[1][1] = scale;
			scaleMatrix[1][2] = 0;

			scaleMatrix[2][0] = 0;
			scaleMatrix[2][1] = 0;
			scaleMatrix[2][2] = 1;

			double[][] moveToCenterMatrix = new double[3][];
			for(int i = 0; i < 3; ++i)
				moveToCenterMatrix[i] = new double[3];

			moveToCenterMatrix[0][0] = 1;
			moveToCenterMatrix[0][1] = 0;
			moveToCenterMatrix[0][2] = 0;

			moveToCenterMatrix[1][0] = 0;
			moveToCenterMatrix[1][1] = 1;
			moveToCenterMatrix[1][2] = 0;

			moveToCenterMatrix[2][0] = Math.Abs(left.X + right.X) / -2;
			moveToCenterMatrix[2][1] = Math.Abs(left.Y + right.Y) / -2;
			moveToCenterMatrix[2][2] = 1;

			var res = MultiplyMatrixes(moveToCenterMatrix, angleMatrix);
			res = MultiplyMatrixes(res, scaleMatrix);
			moveToCenterMatrix[2][0] *= -1;
			moveToCenterMatrix[2][1] *= -1;
			res = MultiplyMatrixes(res, moveToCenterMatrix);

			double[][] moveMatrix = new double[3][];
			for(int i = 0; i < 3; ++i)
				moveMatrix[i] = new double[3];

			moveMatrix[0][0] = 1;
			moveMatrix[0][1] = 0;
			moveMatrix[0][2] = 0;


			moveMatrix[1][0] = 0;
			moveMatrix[1][1] = 1;
			moveMatrix[1][2] = 0;

			moveMatrix[2][0] = xDir;
			moveMatrix[2][1] = -yDir;
			moveMatrix[2][2] = 1;

			return MultiplyMatrixes(res, moveMatrix);
		}

		private double[][] MultiplyMatrixes(double[][] matrix, double[][] multiplier)
		{
			double[][] res = new double[matrix.Length][];
			for(int i = 0; i < res.Length; ++i)
				res[i] = new double[matrix[i].Length];

			for(int i = 0; i < res.Length; ++i)
				for(int j = 0; j < res[i].Length; ++j)
					for(int k = 0; k < multiplier[j].Length; ++k)
						res[i][j] += matrix[i][k] * multiplier[k][j];

			return res;
		}

		private void DrawAffine()
		{
			SetSquare();
			PathFigure pathFigure = new PathFigure();
			pathFigure.StartPoint = new Point(square[0][0], square[0][1]);
			pathFigure.Segments.Add(new LineSegment(new Point(square[0][0], square[0][1]), true));
			pathFigure.Segments.Add(new LineSegment(new Point(square[1][0], square[1][1]), true));
			pathFigure.Segments.Add(new LineSegment(new Point(square[2][0], square[2][1]), true));
			pathFigure.Segments.Add(new LineSegment(new Point(square[3][0], square[3][1]), true));
			pathFigure.IsClosed = true;
			PathGeometry pathGeometry = new PathGeometry();
			pathGeometry.Figures.Add(pathFigure);


			GeometryDrawing geometryDrawing = new GeometryDrawing();
			geometryDrawing.Brush = System.Windows.Media.Brushes.Red;
			geometryDrawing.Geometry = pathGeometry;

			RectangleGeometry filler = new RectangleGeometry(
				new Rect(0, 0, 500, 500));
			GeometryDrawing fillerDrawing = new GeometryDrawing();
			fillerDrawing.Brush = Brushes.Gray;
			fillerDrawing.Geometry = filler;

			DrawingGroup drawingGroup = new DrawingGroup();
			drawingGroup.Children.Add(fillerDrawing);
			drawingGroup.Children.Add(geometryDrawing);

			AffineImage = new DrawingImage(drawingGroup);
		}

		public ICommand MoveCommand => new RelayCommand((obj) =>
		{
			Direction direction = (Direction)obj;
			switch(direction)
			{
				case Direction.Left:
					{
						--xDir;
						break;
					}
				case Direction.Up:
					{
						++yDir;
						break;
					}
				case Direction.Right:
					{
						++xDir;
						break;
					}
				case Direction.Down:
					{
						--yDir;
						break;
					}
			}
			DrawAffine();
		});

		public ICommand ApplyCommand => new RelayCommand((obj) =>
		{
			DrawAffine();
		},
		(obj) =>
		{
			int a = (int)(right.X - left.X);
			int b = (int)(right.Y - left.Y);
			return a == b;
		});

		public ICommand NavigateToMain => new RelayCommand((obj) =>
		{
			MainWindow mw = (MainWindow)App.Current.MainWindow;
			mw.MainFrame.Navigate(new Views.MainView());
		});

		public ICommand NavigateToHelp => new RelayCommand((obj) =>
		{
			MainWindow mw = (MainWindow)App.Current.MainWindow;
			mw.MainFrame.Navigate(new Views.Help("Affine"));
		});

		public DrawingImage AffineImage
		{
			get => affineImage;
			set
			{
				affineImage = value;
				OnPropertyChanged(nameof(AffineImage));
			}
		}

		public double bLeftX
		{
			get => left.X;
			set
			{
				left.X = value;
				OnPropertyChanged(nameof(bLeftX));
			}
		}

		public double bLeftY
		{
			get => left.Y;
			set
			{
				left.Y = value;
				OnPropertyChanged(nameof(bLeftY));
			}
		}
		public double tRightX
		{
			get => right.X;
			set
			{
				right.X = value;
				OnPropertyChanged(nameof(tRightX));
			}
		}

		public double tRightY
		{
			get => right.Y;
			set
			{
				right.Y = value;
				OnPropertyChanged(nameof(tRightY));
			}
		}

		public double Angle
		{
			get => angle;
			set
			{
				angle = value;
				DrawAffine();
				OnPropertyChanged(nameof(Angle));
			}
		}
		public double Scale
		{
			get => scale;
			set
			{
				scale = value;
				DrawAffine();
				OnPropertyChanged(nameof(Scale));
			}
		}
	}
}
