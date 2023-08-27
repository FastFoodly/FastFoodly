using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FastFoodly.Commands;
using FastFoodly.Models;
using FastFoodly.Services;
using FastFoodly.Stores;

namespace FastFoodly.ViewModel;

/// <summary>
/// Classe que implementa o ViewModel para a View CartWindow
/// Herda a classe ViewModelBase
/// </summary>
public class CartViewModel : ViewModelBase
{
    private ObservableCollection<CartItem> _cartItems; ///< Atributo para guardar uma ista de itens de carrinho
    
    /// <summary>
    /// Propriedade para guardar uma lista de itens de carrinho
    /// </summary>
    public ObservableCollection<CartItem> CartItems
    {
        get { return _cartItems; }
        set 
        {
            SetProperty(ref _cartItems, value);
            OnPropertyChanged(nameof(CartItems));
        }
    }
    private readonly NavigationStore _navigationStore; ///< Atributo que referencia o registro de navegação atual

    /// <summary>
    /// Comando para deletar um item do carrinho
    /// </summary>
    public RelayCommand<int> DeleteItem { get; set; }

    /// <summary>
    /// Comando para deletar todos os itens do carrinho
    /// </summary>
    public RelayCommand DeleteAllItems { get; set; }

    public RelayCommand InsertOrder { get; set; }
    
    /// <summary>
    /// Comando para navegar até a página inicial novamente
    /// </summary>
    public ICommand NavigateToHome { get; set; }

    public ICommand NavigateToConfirmOrder { get; }
    private Order _order; ///< Atributo para guardar as informações do pedido
    
    /// <summary>
    /// Propriedade para guardar informações do pedido
    /// </summary>
    public Order Order
    {
        get { return _order; }
        set => SetProperty(ref _order, value);
    }

    /// <summary>
    /// Construtor da ViewModel da View Cart que mostra ao usuário a página que mostra o carrinho
	  /// Precisa receber o registro de navegação atual para gerar essa View nova
	  /// </summary>
    /// <param name="navigationStore"></param>
    public CartViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
        //Manipulação da tabela Carrinho
        var cart = new DbCartService();
        //Lista todos os itens do carrinho
        CartItems = cart.ListAllItems();

        Order = new Order
        {
            TotalPrice = CalculateTotal()
        };

        // Cria os comandos
        DeleteItem = new RelayCommand<int>(DeleteItemCommand);
        
        DeleteAllItems = new RelayCommand(DeleteAllItemsCommand);
        
        InsertOrder = new RelayCommand(InsertOrderCommand);

        NavigateToHome = new NavigateCommand<HomeViewModel>(
            new NavigationService<HomeViewModel>(
                navigationStore, () => new HomeViewModel(navigationStore)));

        NavigateToConfirmOrder = new NavigateCommand<ConfirmOrderViewModel>(
            new NavigationService<ConfirmOrderViewModel>(
                navigationStore, () => new ConfirmOrderViewModel(navigationStore)));
    }

    /// <summary>
    /// Método que calcula valor final.
    /// </summary>
	private decimal CalculateTotal()
	{
        decimal totalPrice = 0;
        foreach (var item in CartItems)
        {
            totalPrice += (decimal)item.Price; 
        }
        return totalPrice;
    }

    /// <summary>
    /// Método chamado quando o comando DeleteItem é executado.
    /// </summary>
    /// <param name="itemId"></param>
	  private void DeleteItemCommand(int itemId)
	  {
		var cart = new DbCartService();

        //Deleta um item especifico do carrinho
        cart.DeleteItem(itemId);

        //Atualiza lista de itens do carrinho
        CartItems = cart.ListAllItems();

        //Recalcula preço total
        Order.TotalPrice = CalculateTotal();
    }

    /// <summary>
    /// Método chamado quando o comando DeleteAllItems é executado.
    /// </summary>
	  private void DeleteAllItemsCommand()
	  {
		var cart = new DbCartService();

        //Deleta todos os itens do carrihno
        cart.DeleteAllItems();
        
        //Atualiza lista de itens do carrinho
        CartItems = cart.ListAllItems();

        //Recalcula preço total
        Order.TotalPrice = CalculateTotal();
    }

    /// <summary>
    /// Método chamado quando o comando InsertOrder é executado.
    /// </summary>
    private void InsertOrderCommand()
    {
        var orderDb = new DbOrderService();


        foreach (var item in CartItems)
        {
            Order.ProductIds += item.ProductId.ToString();
            Order.ProductIds += ',';
            Order.Observations += item.ProductId.ToString();
            Order.Observations += ':';
            Order.Observations += item.Observations;
            Order.Observations += ';';
        }
        //Insere o pedido no banco de dados
        orderDb.InsertOrder(Order);
        
        DeleteAllItemsCommand();

        //Navega para a página de confirmação do pedido
        NavigateToConfirmOrder.Execute(new ConfirmOrderViewModel(_navigationStore));
    }
}