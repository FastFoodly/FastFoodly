using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FastFoodly.ViewModel
{
    /// <summary>
    /// Classe base que representa uma ViewModel
    /// Herda ObservableObject para executar funcionalidades do MVVM Toolkit
    /// Implementa as interfaces INotifyPropertyChanged e IDisposable
    /// </summary>
    public class ViewModelBase : ObservableObject, INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged; ///< Evento de que uma propriedade mudou

        /// <summary>
        /// Método usado para invocar um evento de que uma porpriedade mudou
        /// Usado para informar a View dessa mudança
        /// </summary>
        /// <param name="propertyName">Recebe a propriedade que mudou para invocar um evento</param>
        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Método para ser sobrescrito.
        /// Usado para descontruir objetos caso precise
        /// </summary>
        public virtual void Dispose() { }
    }
}