using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using FastFoodly.Services;
using FastFoodly.Stores;
using FastFoodly.View;
using FastFoodly.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace FastFoodly
{
    /// <summary>
    /// Classe de inicialização da aplicação. Ela gera todas as outras classes e permite que o app funcione.
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Construtor que gera um serviço de inicialização por injeção de dependência. 
        /// Permite que o aplicativo inicialize com uma janela inicial associada a MainWindow.
        /// </summary>
        public App()
        {
            // cria um serviço de Dependecy Injection
            IServiceCollection services = new ServiceCollection();

            // adiciona as classes iniciais para a aplicação e associa a MainWindow com a HomeWindow
            services.AddSingleton<NavigationStore>();
            services.AddSingleton<MainViewModel>();

            services.AddSingleton<INavigationService>(s => CreateHomeNavigationService(s));
            services.AddTransient<HomeViewModel>(s => new HomeViewModel(s.GetRequiredService<NavigationStore>()));

            services.AddSingleton<MainWindow>(s => new MainWindow
            {
                DataContext = s.GetRequiredService<MainViewModel>()
            });

            _serviceProvider = services.BuildServiceProvider();
        }

        /// <summary>
        /// Método executado no início da aplicação.
        /// Navega o sistema para a HomeWindow e faz o display da MainWindow.
        /// </summary>
        /// <param name="e">Parâmetro que pode ser enviado para o App ao ser inicializado</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            INavigationService initialNavigationService = _serviceProvider.GetRequiredService<INavigationService>();
            initialNavigationService.Navigate();
            
            MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            MainWindow.Show();  
        }

        /// <summary>
        /// Cria o serviço de navegação inicial do sistema para que ele possa ir até a HomeWindow
        /// Recebe o serviço de dependecy injection como parâmetro
        /// </summary>
        /// <param name="serviceProvider">Recebe o provedor do serviço de Dependency Injection</param>
        /// <returns>Retorna uma referência para o serviço criado do tipo INavigationService</returns>
        private INavigationService CreateHomeNavigationService(IServiceProvider serviceProvider)
        {
            return new NavigationService<HomeViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<HomeViewModel>());
        }

        /// <summary>
        /// Conecta o aplicativo com o Banco de Dados
        /// </summary>
        string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
    }
}
