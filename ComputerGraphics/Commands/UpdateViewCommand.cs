using ComputerGraphics.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ComputerGraphics.Commands
{
    public class UpdateViewCommand : ICommand
    {
        private MainViewModel viewModel;

        public UpdateViewCommand(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parametr)
        {
            return true;
        }

        public void Execute(object parametr)
        {
            switch (parametr.ToString())
            {
                case "Main":
                    viewModel.SelectedViewModel = new MainViewModel();
                    break;
                case "Fractal":
                    viewModel.SelectedViewModel = new FractalViewModel();
                    break;
            }
        }
    }
}
