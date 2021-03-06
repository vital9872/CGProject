﻿using ComputerGraphics.ViewModels;
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

namespace ComputerGraphics.Views
{
    /// <summary>
    /// Interaction logic for FractalView.xaml
    /// </summary>
    public partial class FractalView : Page
    {
        public FractalView()
        {
            InitializeComponent();
            FractalViewModel vm = new ViewModels.FractalViewModel();
            DataContext = vm;
        }
    }
}
