using FastFoodly.Models;

namespace FastFoodly
{
	/// <summary>
	/// Fornece serviços para interagir com o banco de dados relacionados ao carrinho
	/// </summary>
	public class DbCartService
	{
		/// Atributo para guardar a string de conexão com o banco de dados
		private string _connectionString;

		/// <summary>
		/// Construtor padrão que inicializa a string de conexão com o banco de dados.
		/// </summary>
		public DbCartService()
		{
			_connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
		}

		/// <summary>
		/// Método que abre uma conexão com o banco de dados.
		/// </summary>
		/// <returns>Instância aberta de SqlConnection.</returns>
		public SqlConnection OpenConnection()
		{
			SqlConnection connection = new SqlConnection(_connectionString);
			connection.Open();
			return connection;
		}

		/// <summary>
		/// Método que insere um item no carrinho de compras no banco de dados.
		/// </summary>
		/// <param name="item">O item a ser inserido.</param>
		/// <returns>Uma mensagem de sucesso ou falha.</returns>
		public string InsertItem(CartItem item)
		{
			try
			{
				var conn = OpenConnection();
				SqlCommand command = new SqlCommand($"INSERT INTO carrinho VALUES({item.ProductId}, '{item.Name}', {(int)(item.Price * 100)}, {item.Quantity}, '{item.Observations}', '{item.ImagePath.AbsolutePath}')", conn);

				command.ExecuteReader();
				return "Success";
			}
			catch (Exception ex)
			{
				//Captura a exceção se a conexão com o banco de dados falhar e lança uma mensagem com detalhes do erro
				Console.WriteLine($"Erro ao comunicar com banco. \n\nMessage: {ex.Message} \n\nTarget Site: {ex.TargetSite} \n\nStack Trace: {ex.StackTrace}");
				return "Failed to add item to cart";
			}
		}

		/// <summary>
		/// Lista todos os itens no carrinho de compras a partir do banco de dados.
		/// </summary>
		/// <returns>Uma coleção de objetos CartItem.</returns>
		public ObservableCollection<CartItem> ListAllItems()
		{
			try
			{
				var conn = OpenConnection();
				ObservableCollection<CartItem> cart = new ObservableCollection<CartItem>();
				SqlCommand command = new SqlCommand("SELECT * FROM Carrinho", conn);

				SqlDataReader reader = command.ExecuteReader();
				if (reader.HasRows)
				{
					while (reader.Read())
					{
						// Cria o objeto cartItem e salva suas variaveis
						var cartItem = new CartItem()
						{
							ItemId = (int)reader.GetDecimal(0),
							ProductId = (int)reader.GetDecimal(1),
							Name = reader.GetString(2),
							Price = reader.GetDecimal(3) / 100,
							Quantity = (int)reader.GetDecimal(4),
							Observations = reader.GetString(5)
						};
						string ImagePath = !reader.IsDBNull(6) && !string.IsNullOrEmpty(reader.GetString(6)) ? reader.GetString(6) : Path.GetFullPath(@"Assets/Images/no-image.jpg");
						cartItem.ImagePath = new Uri(ImagePath);
						cart.Add(cartItem);
					}
				}
				return cart;
			}
			catch (Exception ex)
			{
				//Captura a exceção se a conexão com o banco de dados falhar e lança uma mensagem com detalhes do erro
				Console.WriteLine($"Erro ao comunicar com banco. \n\nMessage: {ex.Message} \n\nTarget Site: {ex.TargetSite} \n\nStack Trace: {ex.StackTrace}");
				throw;
			}
		}

		/// <summary>
		/// Exclui um item do carrinho de compras no banco de dados.
		/// </summary>
		/// <param name="itemId">O ID do item a ser excluído.</param>
		/// <returns>Uma mensagem de sucesso ou falha.</returns>
		public string DeleteItem(int itemId)
		{
			try
			{
				var conn = OpenConnection();
				SqlCommand command = new SqlCommand($"DELETE FROM carrinho WHERE idItem={itemId}", conn);

				command.ExecuteReader();
				return "Success";
			}
			catch (Exception ex)
			{
				//Captura a exceção se a conexão com o banco de dados falhar e lança uma mensagem com detalhes do erro
				Console.WriteLine($"Erro ao comunicar com banco. \n\nMessage: {ex.Message} \n\nTarget Site: {ex.TargetSite} \n\nStack Trace: {ex.StackTrace}");
				return $"Failed to delete item {itemId} from cart";
			}
		}

		/// <summary>
		/// Exclui todos os itens do carrinho de compras no banco de dados.
		/// </summary>
		/// <returns>Uma mensagem de sucesso ou falha.</returns>
		public string DeleteAllItems()
		{
			try
			{
				var conn = OpenConnection();
				SqlCommand command = new SqlCommand("DELETE FROM carrinho", conn);

				command.ExecuteReader();
				return "Success";
			}
			catch (Exception ex)
			{
				//Captura a exceção se a conexão com o banco de dados falhar e lança uma mensagem com detalhes do erro
				Console.WriteLine($"Erro ao comunicar com banco. \n\nMessage: {ex.Message} \n\nTarget Site: {ex.TargetSite} \n\nStack Trace: {ex.StackTrace}");
				return "Failed to delete items from cart";
			}
		}
	}
}