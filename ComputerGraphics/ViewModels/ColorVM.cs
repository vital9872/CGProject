using ComputerGraphics.Commands;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ComputerGraphics.ViewModels
{
	class ColorVM: BaseViewModel
	{
		private string _fileName;

		public ColorVM()
		{
			_fileName = "No file selected";
		}

		public ICommand OpenFile => new RelayCommand((obj) =>
		{
			OpenFileDialog ofd = new OpenFileDialog();
			bool? result = ofd.ShowDialog(App.Current.MainWindow);
			if (result == null || result != true)
				return;

			int index = ofd.FileName.LastIndexOf('/');
			FileName = ofd.FileName.Substring(index, ofd.FileName.Length - index);
			
			//TODO: display opened image on view
		});

		public ICommand NavigateToMain => new RelayCommand((obj) =>
		{
			MainWindow mw = (MainWindow)App.Current.MainWindow;
			mw.MainFrame.Navigate(new Views.MainView());
		});

		public string FileName
		{
			get => _fileName;
			set
			{
				_fileName = value;
				OnPropertyChanged(nameof(FileName));
			}
		}
	}
}
