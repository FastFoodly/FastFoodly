using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using FastFoodly.Commands;
using FastFoodly.Services;
using FastFoodly.Stores;
using FastFoodly.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

    public ICommand NavigateToCart { get; }

    /// <summary>
    /// Propriedade que armazena o nome da categoria que será mostrada na tela
    /// </summary>
    public string CategoryName { get; set; }

    private ObservableCollection<Product> _menuByCategory;
    public ObservableCollection<Product> MenuByCategory
    {
        get { return _menuByCategory; }
        set => SetProperty(ref _menuByCategory, value);
    }

    /// <summary>
    /// Construtor da ViewModel da View Category que mostra ao usuário a página da categoria
	  /// Precisa receber o registro de navegação atual para gerar essa View nova
    /// e o parâmetro referente ao nome da categoria que será exposta
    /// </summary>
    /// <param name="categoryName"></param>
    /// <param name="navigationStore"></param>
    public CategoryViewModel(string categoryName, NavigationStore navigationStore)
    {
        CategoryName = categoryName;

        //Manipulação da tabela do cardapio
        var database = new DbMenuService();
        //Lista de itens de uma determinada categoria
        MenuByCategory = database.ListByCategory(CategoryName);

        _navigationStore = navigationStore;
        
        NavigateToHome = new NavigateCommand<HomeViewModel>(
            new NavigationService<HomeViewModel>(
                navigationStore, () => new HomeViewModel(navigationStore)));

        NavigateToCart = new NavigateCommand<CartViewModel>(
            new NavigationService<CartViewModel>(
                navigationStore, () => new CartViewModel(navigationStore)));
    }
}