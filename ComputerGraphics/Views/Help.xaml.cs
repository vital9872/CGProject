using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ComputerGraphics.ViewModels;


namespace ComputerGraphics.Views
{
    /// <summary>
    /// Interaction logic for Help.xaml
    /// </summary>
    public partial class Help : Page
    {
        public Help(string type = null)
        {
            InitializeComponent();
            string cur = Environment.CurrentDirectory;
            BasePropertyChanged vm = new BasePropertyChanged();
            switch (type)
            {
                case "Fractal":
                    pdfViewer.Load("../../../Info/Fractals.pdf"); 
                    vm = new ViewModels.HelpFractalViewModel();
                    break;
                case "Color":
                    pdfViewer.Load("../../../Info/ColorModel.pdf"); 
                    vm = new HelpColorViewModel();
                    break;
                case "Affine":
                    pdfViewer.Load("../../../Info/Affine.pdf");
                    vm = new HelpAffineViewModel();
                    break;
            }

            DataContext = vm;
        }
    }
}
