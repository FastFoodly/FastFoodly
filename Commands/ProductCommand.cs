using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastFoodly.Services;
using FastFoodly.ViewModel;

namespace FastFoodly.Commands
{
    /// <summary>
    /// Classe de Comando para navegar até uma janela de um produto
    /// Herda o a classe CommandBase
    /// </summary>
    public class ProductCommand : CommandBase
    {
        private readonly ParameterNavigationService<string, AddProductViewModel> _navigationService; ///< Atributo que armazena o serviço de navegar entre janelas com parâmetro
        
        /// <summary>
        /// Construtor do comando usado para navegar até o AddProductViewModel passando um parâmetro a ele
        /// Utiliza um serviço de navegação por parâmetro
        /// </summary>
        /// <param name="navigationService">Recebe o serviço de navegação que envia um parâmetro</param>
        public ProductCommand(ParameterNavigationService<string, AddProductViewModel> navigationService)
        {
            _navigationService = navigationService;
        }
        
        /// <summary>
        /// Método que executa a função de navegar para uma outra janela passando um parâmetro para ela
        /// </summary>
        /// <param name="parameter">Objeto genérico que pode ser usado como parâmetro. Nesse caso é uma string que é enviada pela navegação</param>
        public override void Execute(object parameter)
        {
            string buttonName = parameter as string;
            _navigationService.Navigate(buttonName);
        }
    }
}