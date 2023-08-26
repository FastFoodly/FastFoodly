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

public class HomeViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;
    public ICommand NavigateToCategory { get; set; }
    private readonly ObservableCollection<FoodItemsViewModel> _foodItemsViewModels;
    public IEnumerable<FoodItemsViewModel> FoodItemsViewModels => _foodItemsViewModels;

    public RelayCommand<string> SearchItem { get; set; }
    public ICommand NavigateToCategory { get;}
    public ICommand NavigateToProduct { get;}
    public ICommand NavigateToCart { get;}
    private List<Product> menu;
	public List<Product> Menu
	{
		get { return menu; }
		set { menu = value; }
	}

    public HomeViewModel(NavigationStore navigationStore)
    {
        //Manipulação da tabela do cardapio
        var database = new DatabaseService();
        //Lista todos os itens do menu
        menu = database.ListAllMenu();

        SearchItem = new RelayCommand<string>(SearchItemCommand);

        _navigationStore = navigationStore;

        NavigateToCategory = new NavigateCommand<CategoryViewModel>(new NavigationService<CategoryViewModel>(navigationStore, () => new CategoryViewModel(navigationStore)));

        _foodItemsViewModels = new ObservableCollection<FoodItemsViewModel>()
        {
            new FoodItemsViewModel("X-Bacon","Um delicioso hamburguer de carne bovina de 200g com lascas de bacon crocantes e um queijo cheddar derretido de dar �gua na boca"/*, 29.99*/, "Assets/Images/front-view-burgers-stand.jpg"),
            new FoodItemsViewModel("X-Egg","Um delicioso hamburguer de ovo bovino de 200g com lascas de ovo crocantes e um queijo cheddar derretido de dar �gua na boca"/*, 29.99*/, "Assets\\Images\\front-view-burgers-stand.jpg"),
            new FoodItemsViewModel("X-Tudo","Um delicioso hamburguer de tudo bovino de 200g com lascas de tudo crocantes e um queijo cheddar derretido de dar �gua na boca"/*, 29.99*/, "\\Assets\\Images\\front-view-burgers-stand.jpg"),
            new FoodItemsViewModel("X-Tudo","Um delicioso hamburguer de tudo bovino de 200g com lascas de tudo crocantes e um queijo cheddar derretido de dar �gua na boca"/*, 29.99*/, "\\Assets\\Images\\front-view-burgers-stand.jpg"),
            new FoodItemsViewModel("X-Tudo","Um delicioso hamburguer de tudo bovino de 200g com lascas de tudo crocantes e um queijo cheddar derretido de dar �gua na boca"/*, 29.99*/, "\\Assets\\Images\\front-view-burgers-stand.jpg"),
            new FoodItemsViewModel("X-Tudo","Um delicioso hamburguer de tudo bovino de 200g com lascas de tudo crocantes e um queijo cheddar derretido de dar �gua na boca"/*, 29.99*/, "\\Assets\\Images\\front-view-burgers-stand.jpg"),
            new FoodItemsViewModel("X-Tudo","Um delicioso hamburguer de tudo bovino de 200g com lascas de tudo crocantes e um queijo cheddar derretido de dar �gua na boca"/*, 29.99*/, "\\Assets\\Images\\front-view-burgers-stand.jpg"),
            new FoodItemsViewModel("X-Tudo","Um delicioso hamburguer de tudo bovino de 200g com lascas de tudo crocantes e um queijo cheddar derretido de dar �gua na boca"/*, 29.99*/, "\\Assets\\Images\\front-view-burgers-stand.jpg"),
            new FoodItemsViewModel("X-Tudo","Um delicioso hamburguer de tudo bovino de 200g com lascas de tudo crocantes e um queijo cheddar derretido de dar �gua na boca"/*, 29.99*/, "\\Assets\\Images\\front-view-burgers-stand.jpg"),
            new FoodItemsViewModel("X-Tudo","Um delicioso hamburguer de tudo bovino de 200g com lascas de tudo crocantes e um queijo cheddar derretido de dar �gua na boca"/*, 29.99*/, "\\Assets\\Images\\front-view-burgers-stand.jpg"),
            new FoodItemsViewModel("X-Tudo","Um delicioso hamburguer de tudo bovino de 200g com lascas de tudo crocantes e um queijo cheddar derretido de dar �gua na boca"/*, 29.99*/, "\\Assets\\Images\\front-view-burgers-stand.jpg"),
            new FoodItemsViewModel("X-Tudo","Um delicioso hamburguer de tudo bovino de 200g com lascas de tudo crocantes e um queijo cheddar derretido de dar �gua na boca"/*, 29.99*/, "\\Assets\\Images\\front-view-burgers-stand.jpg")
        };
    }
    

        NavigateToCategory = new CategoryCommand(new ParameterNavigationService<string, CategoryViewModel>(navigationStore, (parameter) => new CategoryViewModel(parameter, navigationStore)));
        NavigateToProduct = new ProductCommand(new ParameterNavigationService<string, AddProductViewModel>(navigationStore, (parameter) => new AddProductViewModel(parameter, navigationStore)));
        NavigateToCart = new NavigateCommand<CartViewModel>(new NavigationService<CartViewModel>(navigationStore, () => new CartViewModel(navigationStore)));
    }

    //O m�todo SearchItemCommand() � chamado quando o comando SearchItem � executado. 
	private void SearchItemCommand(string item)
	{
        //Manipula��o da tabela do cardapio
        var database = new DatabaseService();
        //Pesquisa por produto
        menu = database.ListBySearch(item);
	}
}