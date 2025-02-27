﻿using BOTE.Views;
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

namespace BOTE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenRules(Object sender, RoutedEventArgs e)
        {
            Rules rulesWin = new Rules();
            rulesWin.Show();
        }

        private void OpenSelect(Object sender, RoutedEventArgs e)
        {
            SelectEmperor selectEmperor = new SelectEmperor(1);
            Application.Current.MainWindow = selectEmperor;
            this.Close();
            selectEmperor.Show();
        }

        private void OpenSelect2(Object sender, RoutedEventArgs e)
        {
            SelectEmperor selectEmperor = new SelectEmperor(2);
            Application.Current.MainWindow = selectEmperor;
            this.Close();
            selectEmperor.Show();
        }
    }
}