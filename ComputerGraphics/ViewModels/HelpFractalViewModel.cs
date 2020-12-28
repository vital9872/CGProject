using ComputerGraphics.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ComputerGraphics.ViewModels
{
    public class HelpFractalViewModel : BaseViewModel
    {
        public ICommand NavigateBack => new RelayCommand((obj) =>
        {
            MainWindow mw = (MainWindow)App.Current.MainWindow;
            mw.MainFrame.Navigate(new Views.FractalView());
        });
    }
}
