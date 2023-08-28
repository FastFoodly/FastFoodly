using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastFoodly.Services;
using FastFoodly.ViewModel;

namespace FastFoodly.Commands
{
    /// <summary>
    /// Classe para executar o comando de navegar para a Janela de Categoria selecionada
    /// Herda a classe CommandBase
    /// </summary>
    public class CategoryCommand : CommandBase
    {
        /// <summary>
        /// Atributo que armazena um serviço de navegar para outra janela do tipo CategoryViewModel 
        /// e enviar um parâmetro do tipo string
        /// </summary>
        private readonly ParameterNavigationService<string, CategoryViewModel> _navigationService;

        /// <summary>
        /// Construtor do comando de navegar usando o serviço como parâmetro
        /// </summary> 
        /// <param name="navigationService">Referência para um serviço de navegar entre janelas com parâmetro</param>
        public CategoryCommand(ParameterNavigationService<string, CategoryViewModel> navigationService)
        {
            _navigationService = navigationService;
        }
        /// <summary>
        /// Método que é executado sempre que o comando é chamado.
        /// Essa função chama o método de navegar, enviando um parâmetro, do serviço registrado 
        /// </summary>
        /// <param name="parameter">Um objeto recebido como parâmetro para executar a função. Nesse caso é uma string que será enviada como parâmetro no comando de Navegação</param>
        public override void Execute(object parameter)
        {
            string buttonName = parameter as string;
            _navigationService.Navigate(buttonName);
        }
    }
}