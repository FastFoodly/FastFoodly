using System;
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
/// Classe que implementa o ViewModel para a View HomeWindow
/// Herda a classe ViewModelBase
/// </summary>
public class HomeViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;

    /// <summary>
    /// Comando para navegar até a janela de Category
    /// </summary>
    public ICommand NavigateToCategory { get; set; }

    /// <summary>
    /// Comando para procurar um item da barra de pesquisa
    /// </summary>
    public RelayCommand<string> SearchItem { get; set; }

    /// <summary>
    /// Comando para navegar até a janela de um Produto
    /// </summary>
    public ICommand NavigateToProduct { get; }

    /// <summary>
    /// Comando para navegar até a janela do carrinho
    /// </summary>
    public ICommand NavigateToCart { get; }
    private ObservableCollection<Product> _menu; ///< Atributo com uma lista de todos os produtos

    /// <summary>
    /// Propriedade com uma lista de todos os produtos do menu
    /// </summary>
    public ObservableCollection<Product> Menu
    {
        get { return _menu; }
        set => SetProperty(ref _menu, value);
    }

    /// <summary>
    /// Construtor da ViewModel da View Home que mostra ao usuário a página inicial com o cardápio e categorias
	/// Precisa receber o registro de navegação atual para gerar essa View nova
    /// </summary>
    /// <param name="navigationStore"></param>
    public HomeViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
        //Manipulação da tabela do cardapio
        var database = new DbMenuService();
        //Lista todos os itens do menu
        Menu = database.ListAllMenu();

        //cria todos os comandos
        SearchItem = new RelayCommand<string>(SearchItemCommand);

        NavigateToCategory = new CategoryCommand(
            new ParameterNavigationService<string, CategoryViewModel>(
                navigationStore, (parameter) => new CategoryViewModel(parameter, navigationStore)));

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
    /// <param name="item"></param>
    private void SearchItemCommand(string item)
    {
        //Manipulação da tabela do cardapio
        var database = new DbMenuService();
        //Pesquisa por produto
        Menu = database.ListBySearch(item);
    }
}