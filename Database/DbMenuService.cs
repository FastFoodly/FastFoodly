using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using FastFoodly.Models;

namespace FastFoodly
{
    /// <summary>
    /// Fornece serviços para interagir com o banco de dados relacionados ao menu de produtos.
    /// </summary>
    public class DbMenuService
    {
        /// Atributo para guardar a string de conexão com o banco de dados
        private string _connectionString;

        /// <summary>
        /// Construtor padrão que inicializa a string de conexão com o banco de dados.
        /// </summary>
        public DbMenuService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        }

        /// <summary>
        /// Abre uma conexão com o banco de dados.
        /// </summary>
        /// <returns>Uma instância aberta de SqlConnection.</returns>
        public SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        /// <summary>
        /// Lista todos os itens do menu a partir do banco de dados.         
        /// </summary>
        /// <returns>Uma coleção de objetos Product que representam o menu.</returns>
        public ObservableCollection<Product> ListAllMenu()
        {
            try
            {
                var conn = OpenConnection();
                ObservableCollection<Product> cardapio = new ObservableCollection<Product>();
                SqlCommand command = new SqlCommand("SELECT * FROM Cardapio", conn);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // Cria o objeto Produto e salva suas variaveis
                        var produto = new Product()
                        {
                            ProductId = (int)reader.GetDecimal(0),
                            Name = reader.GetString(1),
                            Price = reader.GetDecimal(2) / 100,
                            Description = reader.GetString(3),
                            Extras = new ObservableCollection<string>(),
                            Category = reader.GetString(5)
                        };

                        // salva elementos na lista de ingredientes
                        var rawList = reader.GetString(4).Split(',');
                        for (int i = 0; i < rawList.Length; i++)
                        {
                            produto.Extras?.Add(rawList[i]);
                        }
                        string ImagePath = !reader.IsDBNull(6) && !string.IsNullOrEmpty(reader.GetString(6)) ? reader.GetString(6) : "Assets/Images/no-image.jpg";
                        produto.ImagePath = new Uri(Path.GetFullPath(@ImagePath));
                        cardapio.Add(produto);
                    }
                }
                return cardapio;
            }
            catch (Exception ex)
            {
                //Captura a exceção se a conexão com o banco de dados falhar e lança uma mensagem com detalhes do erro
                Console.WriteLine($"Erro ao comunicar com banco. \n\nMessage: {ex.Message} \n\nTarget Site: {ex.TargetSite} \n\nStack Trace: {ex.StackTrace}");
                throw;
            }
        }

        /// <summary>
        /// Lista os itens do menu de acordo com a categoria especificada.
        /// </summary>
        /// <param name="categoria">A categoria pela qual os produtos são filtrados.</param>
        /// <returns>Uma coleção de objetos Product que correspondem à categoria especificada.</returns>
        public ObservableCollection<Product> ListByCategory(string categoria)
        {
            try
            {
                ObservableCollection<Product> menuByCategory = new ObservableCollection<Product>();
                var conn = OpenConnection();
                SqlCommand command = new SqlCommand($"SELECT * FROM cardapio WHERE categoria='{categoria}'", conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // Cria o objeto Produto e salva suas variaveis
                        var produto = new Product()
                        {
                            ProductId = (int)reader.GetDecimal(0),
                            Name = reader.GetString(1),
                            Price = reader.GetDecimal(2) / 100,
                            Description = reader.GetString(3),
                            Extras = new ObservableCollection<string>(),
                            Category = reader.GetString(5)
                        };

                        // salva elementos na lista de ingredientes
                        var rawList = reader.GetString(4).Split(',');
                        for (int i = 0; i < rawList.Length; i++)
                        {
                            produto.Extras?.Add(rawList[i]);
                        }
                        string ImagePath = !reader.IsDBNull(6) && !string.IsNullOrEmpty(reader.GetString(6)) ? reader.GetString(6) : "Assets/Images/no-image.jpg";
                        produto.ImagePath = new Uri(Path.GetFullPath(@ImagePath));
                        menuByCategory.Add(produto);
                    }
                }
                return menuByCategory;
            }
            catch (Exception ex)
            {
                //Captura a exceção se a conexão com o banco de dados falhar e lança uma mensagem com detalhes do erro
                Console.WriteLine($"Erro ao comunicar com banco. \n\nMessage: {ex.Message} \n\nTarget Site: {ex.TargetSite} \n\nStack Trace: {ex.StackTrace}");
                throw;
            }
        }

        /// <summary>
        ///  Lista os itens do menu que correspondem a uma pesquisa de texto.
        /// </summary>
        /// <param name="searchString">O texto a ser pesquisado.</param>
        /// <returns>Uma coleção de objetos Product que correspondem à pesquisa.</returns>
        public ObservableCollection<Product> ListBySearch(string searchString)
        {
            try
            {
                ObservableCollection<Product> menuBySearch = new ObservableCollection<Product>();
                var conn = OpenConnection();
                searchString = SanitizeInput(searchString);
                SqlCommand command = new SqlCommand($"SELECT * FROM cardapio WHERE descricao LIKE '%{searchString}%' OR nome LIKE '%{searchString}%'", conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // Cria o objeto Produto e salva suas variaveis
                        var produto = new Product()
                        {
                            ProductId = (int)reader.GetDecimal(0),
                            Name = reader.GetString(1),
                            Price = reader.GetDecimal(2) / 100,
                            Description = reader.GetString(3),
                            Extras = new ObservableCollection<string>(),
                            Category = reader.GetString(5)
                        };

                        // salva elementos na lista de ingredientes
                        var rawList = reader.GetString(4).Split(',');
                        for (int i = 0; i < rawList.Length; i++)
                        {
                            produto.Extras?.Add(rawList[i]);
                        }

                        string ImagePath = !reader.IsDBNull(6) && !string.IsNullOrEmpty(reader.GetString(6)) ? reader.GetString(6) : "Assets/Images/no-image.jpg";
                        produto.ImagePath = new Uri(Path.GetFullPath(@ImagePath));
                        menuBySearch.Add(produto);
                    }
                }
                return menuBySearch;
            }
            catch (Exception ex)
            {
                //Captura a exceção se a conexão com o banco de dados falhar e lança uma mensagem com detalhes do erro
                Console.WriteLine($"Erro ao comunicar com banco. \n\nMessage: {ex.Message} \n\nTarget Site: {ex.TargetSite} \n\nStack Trace: {ex.StackTrace}");
                throw;
            }
        }

        /// <summary>
        /// Obtém um produto pelo seu nome.
        /// </summary>
        /// <param name="name">O nome do produto a ser obtido.</param>
        /// <returns>Um objeto Product que corresponde ao nome especificado.</returns>
        public Product GetProductByName(string name)
        {
            try
            {
                // Cria o objeto Produto
                Product menuBySearch = new Product();
                var conn = OpenConnection();
                SqlCommand command = new SqlCommand($"SELECT * FROM cardapio WHERE nome='{name}'", conn);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        menuBySearch.ProductId = (int)reader.GetDecimal(0);
                        menuBySearch.Name = reader.GetString(1);
                        menuBySearch.Price = reader.GetDecimal(2) / 100;
                        menuBySearch.Description = reader.GetString(3);
                        menuBySearch.Extras = new ObservableCollection<string>();
                        menuBySearch.Category = reader.GetString(5);

                        string ImagePath = !reader.IsDBNull(6) && !string.IsNullOrEmpty(reader.GetString(6)) ? reader.GetString(6) : "Assets/Images/no-image.jpg";
                        menuBySearch.ImagePath = new Uri(Path.GetFullPath(@ImagePath));


                        //salva valor do Id do produto
                        object value = reader.GetValue(0);
                        if (value is decimal decimalValue)
                        {
                            int numericValue = (int)decimalValue; // Convert decimal to int
                            menuBySearch.ProductId = numericValue;
                        }

                        // salva elementos na lista de ingredientes
                        var rawList = reader.GetString(4).Split(',');
                        for (int i = 0; i < rawList.Length; i++)
                        {
                            menuBySearch.Extras?.Add(rawList[i]);
                        }
                    }

                }
                return menuBySearch;
            }
            catch (Exception ex)
            {
                //Captura a exceção se a conexão com o banco de dados falhar e lança uma mensagem com detalhes do erro
                Console.WriteLine($"Erro ao comunicar com banco. \n\nMessage: {ex.Message} \n\nTarget Site: {ex.TargetSite} \n\nStack Trace: {ex.StackTrace}");
                throw;
            }
        }

        /// <summary>
        /// Remove caracteres potencialmente perigosos de uma string.
        /// </summary>
        /// <param name="input">A string a ser sanitizada.</param>
        /// <returns>A string após a remoção dos caracteres perigosos.</returns>
        public string SanitizeInput(string input)
        {
            List<string> lixo = new() { "select", "drop", ";", "--", "'", "insert", "delete", "xp_" };
            string textoOK = input;

            foreach (var item in lixo)
            {
                textoOK = input.Replace(item, "");
            };

            return textoOK;
        }
    }
}