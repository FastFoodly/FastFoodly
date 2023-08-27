using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FastFoodly.Commands;
using FastFoodly.Services;
using FastFoodly.Stores;

namespace FastFoodly.ViewModel;

/// <summary>
/// Classe que implementa o ViewModel para a View ConfirmOrderWindow
/// Herda a classe ViewModelBase
/// </summary>
public class ConfirmOrderViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore; ///< atributo que referencia o registro de navegação atual

    /// <summary>
    /// Comando para voltar à tela inicial Home
    /// </summary>
    public ICommand NavigateToHome {get;}

    private int? _orderId; ///< Atributo que representa os ids do pedido.

    /// <summary>
    /// Propriedade OrderId, que representa os ids do pedido.
    /// </summary>
    public int? OrderId
    {
        get { return _orderId; }
        //método SetProperty para notificar os assinantes sobre a alteração e atualizar o valor da propriedade.
        set { SetProperty(ref _orderId, value); }
    }

    /// <summary>
    /// Construtor da ViewModel da View ConfirmOrder que mostra ao usuário a página de conclusão de pedido.
	/// Precisa receber o registro de navegação atual para gerar essa View nova.
    /// </summary>
    /// <param name="navigationStore">Recebe uma referência para o registro de navegação atual</param>
    public ConfirmOrderViewModel(NavigationStore navigationStore)
    {
        var orderDb = new DbOrderService();
        //salva o id do ultimo pedido
        OrderId = orderDb.GetLastOrder();

        _navigationStore = navigationStore;

        NavigateToHome = new NavigateCommand<HomeViewModel>(
            new NavigationService<HomeViewModel>(
                navigationStore, () => new HomeViewModel(navigationStore)));
    }
}