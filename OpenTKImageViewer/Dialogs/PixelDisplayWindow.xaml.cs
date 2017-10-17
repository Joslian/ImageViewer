﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OpenTKImageViewer.UI;

namespace OpenTKImageViewer.Dialogs
{
    /// <summary>
    /// Interaction logic for PixelDisplayWindow.xaml
    /// </summary>
    public partial class PixelDisplayWindow : Window
    {
        private readonly MainWindow parent;

        public PixelDisplayWindow(MainWindow parent)
        {
            this.parent = parent;
            InitializeComponent();

            this.BoxFormat.SelectedIndex = (int) parent.StatusBar.PixelDisplay;
            this.NumRadius.Value = parent.StatusBar.PixelRadius;
            this.BoxAlpha.IsChecked = parent.StatusBar.PixelShowAlpha;
            this.BoxDisplayColor.SelectedIndex = parent.Context.DisplayColorBeforeTonemapping?0:1;
        }

        private void Apply_OnClick(object sender, RoutedEventArgs e)
        {
            if (NumRadius.Value != null) parent.StatusBar.PixelRadius = (int) NumRadius.Value;
            parent.StatusBar.PixelDisplay = (StatusBarControl.PixelDisplayType) BoxFormat.SelectedIndex;
            parent.Context.DisplayColorBeforeTonemapping = (BoxDisplayColor.SelectedIndex == 0);
            if (BoxAlpha.IsChecked != null) parent.StatusBar.PixelShowAlpha = (bool) BoxAlpha.IsChecked;
            Close();
            // must be redrawn when pixel display changed (to aqcuire the texture for a different time)
            parent.RedrawFrame();
        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}