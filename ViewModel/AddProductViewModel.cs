using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using FastFoodly.Models;
using FastFoodly.Stores;
using System.Windows.Input;
using FastFoodly.Commands;
using FastFoodly.Services;
using System.Collections.ObjectModel;
using System.IO;


namespace FastFoodly.ViewModel;

/// <summary>
/// Classe que implementa o ViewModel para a View AddProductWindow
/// Herda a classe ViewModelBase
/// </summary>
public class AddProductViewModel : ViewModelBase
{
	private CartItem _cartItem; ///< Atributo que guarda um item do carrinho
  
  /// <summary>
	/// Propriedade que guarda um item do carrinho
	/// </summary>
	public CartItem CartItem
	{
		get { return _cartItem; }
        set => SetProperty(ref _cartItem, value);
	}

	private Product _product; ///< Atributo que guarda um produto
  
  /// <summary>
	/// Propriedade que guarda um produto
	/// </summary>
	public Product Product
	{
		get { return _product; }
        set => SetProperty(ref _product, value);
	}

	private ObservableCollection<Extra> _extras;

	public ObservableCollection<Extra> Extras
	{
		get {return _extras; }
		set => SetProperty(ref _extras, value);
	}
	private readonly NavigationStore _navigationStore;

	/// <summary>
	/// Comando para executar um método que adicione um item no carrinho
	/// O comando é implementado utilizando a classe RelayCommand do Community Toolkit MVVM.
	/// </summary>
	public RelayCommand AddToCart { get; set; }

	/// <summary>
	/// Comando para navegar para a HomeView novamente e visualizar a página inicial novamente
	/// </summary>
	public ICommand NavigateToHome { get; }

	/// <summary>
	/// Comando para navegar para a CartView e visualizar o carrinho
	/// </summary>
	public ICommand NavigateToCart { get; }

	/// <summary>
	/// Propriedade que guarda o nome de um produto
	/// </summary>
	public string ProductName { get; set; }

	/// <summary>
	/// Construtor da ViewModel da View AddProduct que mostra ao usuário a página que descreve um certo produto
	/// Precisa receber o nome do produto e o registro de navegação atual para gerar essa View nova específica para esse produto
	/// </summary>
	/// <param name="productName"></param>
	/// <param name="navigationStore"></param>
	public AddProductViewModel(string productName, NavigationStore navigationStore)
	{
		_navigationStore = navigationStore;
		// encontra o produto selecionado no banco de dados e cria um item de carrinho com ele
		ProductName = productName;
		var database = new DbMenuService();
		Product = database.GetProductByName(ProductName);
		CartItem = new CartItem()
		{
			ProductId = Product.ProductId,
			Name = Product.Name,
			Price = Product.Price,
			Quantity = 1,
			Observations = " ",
			ImagePath = Product.ImagePath
		};

		// cria lista de extras
		_extras = new ObservableCollection<Extra>();
		if (Product.Extras != null)
			foreach (var extraName in Product.Extras)
			{	
				var extra = new Extra()
				{
					Name = extraName,
					Quantity = 0
				};
				_extras.Add(extra);
			}
		Extras = _extras;

		// cria os comandos da ViewModel
		_navigationStore = navigationStore;

		AddToCart = new RelayCommand(AddToCartCommand);

		NavigateToHome = new NavigateCommand<HomeViewModel>(
			new NavigationService<HomeViewModel>(
				navigationStore, () => new HomeViewModel(navigationStore)));

		NavigateToCart = new NavigateCommand<CartViewModel>(
			new NavigationService<CartViewModel>(navigationStore, () => new CartViewModel(navigationStore)));
	}

	/// <summary>
	/// Método que é chamado quando o comando AddToCart é executado.
	/// </summary>
	private void AddToCartCommand()
	{
		var cart = new DbCartService();

		//Configurar observações de acordo com o que foi selecionado.
		//Adição de item ao carrinho
		cart.InsertItem(CartItem);
	}

	/// <summary>
	/// Método que é chamado quando o comando AddExtra é executado.
	/// </summary>
	private void AddExtraCommand(string extraName)
	{
		for (int i = 0; i < Extras.Count; i++)
			if (Extras[i].Name == extraName)
			{
				Extras[i].Quantity++;
				CartItem.Price += 3;
				break;
			}
	}

	/// <summary>
	/// Método que é chamado quando o comando RemoveExtra é executado.
	/// </summary>
	private void RemoveExtraCommand(string extraName)
	{
		for (int i = 0; i < Extras.Count; i++)
			if (Extras[i].Name == extraName)
			{	
				if (Extras[i].Quantity > 0)
				{
					Extras[i].Quantity--;
					CartItem.Price += 3;
				}
				break;
			}
		
	}
}