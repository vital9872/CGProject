using ComputerGraphics.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ComputerGraphics.ViewModels
{
    public class MainViewModel : BasePropertyChanged
    {
        private BasePropertyChanged selectedViewModel;
        public BasePropertyChanged SelectedViewModel
        {
            get { return selectedViewModel; }
            set
            {
                selectedViewModel = value;
                OnPropertyChanged("SelectedViewModel");
            }
        }

        public ICommand UpdateViewCommand { get; set; }

        public MainViewModel()
        {
            selectedViewModel = this;
            UpdateViewCommand = new UpdateViewCommand(this);
        }

    }
}
