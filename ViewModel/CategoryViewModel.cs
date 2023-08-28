using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using FastFoodly.Commands;
using FastFoodly.Services;
using FastFoodly.Stores;
using FastFoodly.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

namespace FastFoodly.ViewModel;

/// <summary>
/// Classe que implementa o ViewModel para a View CategoryWindow
/// Herda a classe ViewModelBase
/// </summary>
public class CategoryViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore; ///< Atributo que referencia o registro de navegação atual

    /// <summary>
    /// Comando para navegar de volta à página inicial Home
    /// </summary>
    public ICommand NavigateToHome { get; }

    /// <summary>
    /// Comando para navegar até a janela de um Produto
    /// </summary>
    public ICommand NavigateToProduct { get; }

    /// <summary>
    /// Comando para navegar até a janela de carrinho
    /// </summary>
    public ICommand NavigateToCart { get; }

    /// <summary>
    /// Propriedade que armazena o nome da categoria que será mostrada na tela
    /// </summary>
    public string CategoryName { get; set; }

    private ObservableCollection<Product> _menuByCategory; ///< Atributo que gera uma lista de produtos que pertencem à categoria da página

    /// <summary>
    /// Propriedade que gera uma lista de produtos que pertencem à categoria da página
    /// </summary>
    public ObservableCollection<Product> MenuByCategory
    {
        get { return _menuByCategory; }
        set 
        {
            SetProperty(ref _menuByCategory, value);
            OnPropertyChanged(nameof(MenuByCategory));
        }
    }

    /// <summary>
    /// Comando para procurar um item da barra de pesquisa
    /// </summary>
    public RelayCommand SearchItem { get; set; }

    private string _searchValue; ///< Atributo com uma lista de todos os produtos

    /// <summary>
    /// Variável que armazena o valor a ser consultado no banco de dados
    /// </summary>
    public string SearchValue 
    {         
        get { return _searchValue; }
        set => SetProperty(ref _searchValue, value); 
    }

    /// <summary>
    /// Construtor da ViewModel da View Category que mostra ao usuário a página da categoria
	/// Precisa receber o registro de navegação atual para gerar essa View nova
    /// e o parâmetro referente ao nome da categoria que será exposta
    /// </summary>
    /// <param name="categoryName">Recebe o nome da categoria que deve ser visualizada na página</param>
    public CategoryViewModel(string categoryName, NavigationStore navigationStore)
    {
        CategoryName = categoryName;

        SearchValue = "Qual o tamanho da sua fome?";

        //cria o comando para chamar a pesquisa no banco de dados
        SearchItem = new RelayCommand(SearchItemCommand);

        //Manipulação da tabela do cardapio
        var database = new DbMenuService();
        //Lista de itens de uma determinada categoria
        MenuByCategory = database.ListByCategory(CategoryName);

        _navigationStore = navigationStore;
        
        NavigateToHome = new NavigateCommand<HomeViewModel>(
            new NavigationService<HomeViewModel>(
                navigationStore, () => new HomeViewModel(navigationStore)));

        NavigateToProduct = new ProductCommand(
            new ParameterNavigationService<string, AddProductViewModel>(
                navigationStore, (parameter) => new AddProductViewModel(parameter, navigationStore)));

        NavigateToCart = new NavigateCommand<CartViewModel>(
            new NavigationService<CartViewModel>(
                navigationStore, () => new CartViewModel(navigationStore)));
    }

    /// <summary>
    /// Método chamado quando o comando SearchItem é executado.
    /// Precisa do item a ser procurado como parâmetro
    /// </summary>
    private void SearchItemCommand()
    {
        //Manipulação da tabela do cardapio
        var database = new DbMenuService();

        //Pesquisa por produto
        MenuByCategory = database.ListBySearch(SearchValue);
    }
}