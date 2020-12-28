using System.Windows.Input;
using ComputerGraphics.Commands;

namespace ComputerGraphics.ViewModels
{
    public class HelpColorViewModel : BasePropertyChanged
    {
        public ICommand NavigateBack => new RelayCommand((obj) =>
        {
            MainWindow mw = (MainWindow)App.Current.MainWindow;
            mw.MainFrame.Navigate(new Views.ColorPage());
        });
    }
}