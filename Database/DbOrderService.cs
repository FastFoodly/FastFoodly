using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using FastFoodly.Models;

namespace FastFoodly
{
	public class DbOrderService
	{
		private string _connectionString;

		public DbOrderService()
		{
			_connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
		}

		public SqlConnection OpenConnection()
		{
			SqlConnection connection = new SqlConnection(_connectionString);
			connection.Open();
			return connection;
		}

		public int InsertOrder(Order order)
		{
			try
			{
				var conn = OpenConnection();
				SqlCommand command = new SqlCommand($"INSERT INTO pedidos VALUES('{order.ProductIds}', {(int)(order.TotalPrice * 100)}, '{order.Observations}')", conn);
				command.ExecuteReader();

				//buscar id do pedido
				// SqlCommand getIdCommand = new SqlCommand("SELECT MAX(idPedido) FROM pedidos", conn);
				int OrderId = GetLastOrder();

				return OrderId;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Erro ao comunicar com banco. \n\nMessage: {ex.Message} \n\nTarget Site: {ex.TargetSite} \n\nStack Trace: {ex.StackTrace}");
				return 0;
			}
		}

		public int GetLastOrder()
		{
			try
			{
				var conn = OpenConnection();

				//buscar id do pedido
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
				Console.WriteLine($"Erro ao comunicar com banco. \n\nMessage: {ex.Message} \n\nTarget Site: {ex.TargetSite} \n\nStack Trace: {ex.StackTrace}");
				return 0;
			}
		}
	}
}