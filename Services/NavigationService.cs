using System;
using System.Data.SqlClient;
using CommunityToolkit.Mvvm.ComponentModel;
using FastFoodly.Stores;
using FastFoodly.ViewModel;

namespace FastFoodly.Services;
/// <summary>
/// Classe de serviço que possui capacidade de navegar entre classes usadas para representar janelas ViewModelBase
/// </summary>
/// <typeparam name="TViewModel">Generic que pode expandir essa classe para qualquer ViewModelBase</typeparam>
public class NavigationService<TViewModel> : INavigationService where TViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore; ///< atributo que armazena uma classe que guarda a janela que está sendo visualizada no momento
    private readonly Func<TViewModel> _createViewModel; ///< Delegate que executa um método que cria uma ViewModel nova
    
    /// <summary>
    /// Cria o serviço com referência para um registro de navegação atual e um delegate que cria nova VieModel
    /// </summary>
    /// <param name="navigationStore">Recebe o registro de navegação atual para alterar o display da View</param>
    /// <param name="createViewModel">Recebe um delegate que roda uma fnção para gerar uma ViewModel nova</param>
    public NavigationService(NavigationStore navigationStore, Func<TViewModel> createViewModel)
    {
        _navigationStore = navigationStore;
        _createViewModel = createViewModel;
    }

    /// <summary>
    /// Método que executa a navegação do serviço, atualizando o registro de navegação atual.
    /// </summary>
    public void Navigate()
    {
        _navigationStore.CurrentViewModel = _createViewModel();
    }
}