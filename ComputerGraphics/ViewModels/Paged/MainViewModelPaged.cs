﻿using ComputerGraphics.Commands;
using ComputerGraphics.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ComputerGraphics.ViewModels.Paged
{
	class MainViewModelPaged: BasePropertyChanged
	{
		public ICommand NavigateToFractal => new RelayCommand((obj) =>
		{
			MainWindow mw = (MainWindow)App.Current.MainWindow;
			mw.MainFrame.Navigate(new FractalView());
		});

		public ICommand NavigateToColor => new RelayCommand((obj) =>
		{
			MainWindow mw = (MainWindow)App.Current.MainWindow;
			mw.MainFrame.Navigate(new ColorPage());
		});
	}
}
