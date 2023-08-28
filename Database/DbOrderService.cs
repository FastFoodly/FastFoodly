using FastFoodly.Models;

namespace FastFoodly
{
	/// <summary>
	/// Fornece serviços para interagir com o banco de dados relacionados aos pedidos.
	/// </summary>
	public class DbOrderService
	{
		/// Atributo para guardar a string de conexão com o banco de dados
		private string _connectionString;

		/// <summary>
		/// Construtor padrão que inicializa a string de conexão com o banco de dados.
		/// </summary>
		public DbOrderService()
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
		/// Insere um pedido no banco de dados.
		/// </summary>
		/// <param name="order">O objeto Order a ser inserido no banco de dados.</param>
		/// <returns>O ID do pedido inserido.</returns>
		public int InsertOrder(Order order)
		{
			try
			{
				var conn = OpenConnection();
				SqlCommand command = new SqlCommand($"INSERT INTO pedidos VALUES('{order.ProductIds}', {order.TotalPrice * 100}, '{order.Observations}')", conn);
				command.ExecuteReader();

				//buscar id do pedido
				int OrderId = GetLastOrder();

				return OrderId;
			}
			catch (Exception ex)
			{
				//Captura a exceção se a conexão com o banco de dados falhar e lança uma mensagem com detalhes do erro
				Console.WriteLine($"Erro ao comunicar com banco. \n\nMessage: {ex.Message} \n\nTarget Site: {ex.TargetSite} \n\nStack Trace: {ex.StackTrace}");
				return 0;
			}
		}
		/// <summary>
		/// Obtém o ID do último pedido inserido no banco de dados.
		/// </summary>
		/// <returns>O ID do último pedido.</returns>
		public int GetLastOrder()
		{
			try
			{
				var conn = OpenConnection();

				//buscar id do último pedido
				SqlCommand getIdCommand = new SqlCommand("SELECT MAX(idPedido) FROM pedidos", conn);
				int OrderId = 0;

				SqlDataReader reader = getIdCommand.ExecuteReader();
				if (reader.HasRows)
				{
					while (reader.Read())
					{
						// Salva o id do pedido
						OrderId = !reader.IsDBNull(0) ? (int)reader.GetDecimal(0) : 0;
					}
				}
				return OrderId;
			}
			catch (Exception ex)
			{
				//Captura a exceção se a conexão com o banco de dados falhar e lança uma mensagem com detalhes do erro
				Console.WriteLine($"Erro ao comunicar com banco. \n\nMessage: {ex.Message} \n\nTarget Site: {ex.TargetSite} \n\nStack Trace: {ex.StackTrace}");
				return 0;
			}
		}
	}
}