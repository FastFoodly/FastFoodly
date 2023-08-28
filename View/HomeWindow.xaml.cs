using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FastFoodly.Models;
using FastFoodly.ViewModel;

namespace FastFoodly.View
{
    /// <summary>
    /// Interação lógica para MainWindow.xaml
    /// </summary>
    public partial class HomeWindow : UserControl
    {
        /// <summary>
        /// Inicializa a janela Inicial Home
        /// </summary>
        public HomeWindow()
        {
            InitializeComponent();
            //MainFrame.Navigate(new Uri("MainWindow.xaml", UriKind.Relative));
        }
        //private void Categoria(object sender, RoutedEventArgs e)
        //{
        //    MainFrame.Navigate(new Uri("Categoria.xaml", UriKind.Relative));
        //}
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MainFrame.Navigate(new Categoria());
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        //private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        //{

        //}
    }
}