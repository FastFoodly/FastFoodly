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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FastFoodly.View
{
    /// <summary>
    /// Interação lógica para UserControl.xaml
    /// </summary>
    public partial class CategoryWindow : UserControl
    {
        /// <summary>
        /// Inicializa a janela de Categoria
        /// </summary>
        public CategoryWindow()
        {
            InitializeComponent();
        }

        private void SearchBox_Enter(object sender, KeyEventArgs e)
        {
           if (e.Key == Key.Enter)
            {
                string typedText = txtSearch.Text;

                txtSearch.Text = (typedText + " foi o que você digitou");

                
            }
           
        }
    }
}