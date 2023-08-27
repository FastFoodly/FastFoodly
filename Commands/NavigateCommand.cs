using FastFoodly.Stores;
using FastFoodly.ViewModel;
using System;
using FastFoodly.Services;
using System.Collections.Generic;
using System.Text;
using System.Windows.Documents;

namespace FastFoodly.Commands
{
    /// <summary>
    /// Classe do tipo Generic de Comando para executar uma navegação entre janelas
    /// Herda a classe CommandBase
    /// </summary>
    /// <typeparam name="TViewModel">Possui um generic para ampliar o uso para qualquer tipo de ViewModelBase</typeparam>
    public class NavigateCommand<TViewModel> : CommandBase 
        where TViewModel : ViewModelBase
    {
        private readonly NavigationService<TViewModel> _navigationService; ///< atributo que armazena serviço usado para navegar entre janelas

        /// <summary>
        /// Construtor do comando para navegar entre janelas.
        /// Utiliza o serviço de navegação
        /// </summary>
        /// <param name="navigationService">Recebe o serviço de navegação como parâmetro</param>
        public NavigateCommand(NavigationService<TViewModel> navigationService)
        {
            _navigationService = navigationService;
        }

        /// <summary>
        /// Método que contém lógica utilizada para navegar entre janelas.
        /// Utiliza o método Navigate() do serviço de navegação.
        /// </summary>
        /// <param name="parameter">Objeto genérico que poderia ser usado como parâmetro</param>
        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
            
        }
    }
}
