﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ComputerGraphics
{
    public class MouseBehaviour
    {
        public static readonly DependencyProperty MouseMoveCommandProperty =
                   DependencyProperty.RegisterAttached("MouseMoveCommand", typeof(ICommand), typeof(MouseBehaviour), new FrameworkPropertyMetadata(new PropertyChangedCallback(MouseMoveCommandChanged)));

        private static void MouseMoveCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)d;

            element.MouseMove += new MouseEventHandler(element_MouseMove);
        }

        static void element_MouseMove(object sender, MouseEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;

            ICommand command = GetMouseMoveCommand(element);

            command.Execute(e);
        }

        public static void SetMouseMoveCommand(UIElement element, ICommand value)
        {
            element.SetValue(MouseMoveCommandProperty, value);
        }
        public static ICommand GetMouseMoveCommand(UIElement element)
        {
            return (ICommand)element.GetValue(MouseMoveCommandProperty);
        }
    }
}
