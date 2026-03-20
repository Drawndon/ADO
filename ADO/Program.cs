using System;

using System.Data.SqlClient;

namespace ADO
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string connection_string = "Data Source=(localdb)\\MSSQLLocalDB;" +
														"Initial Catalog=Movies_PV_521;Integrated Security=True;Connect Timeout=30;Encrypt=False;" +
														"TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
			Console.WriteLine(connection_string);
			SqlConnection connection = new SqlConnection(connection_string);
			connection.Open();
			Separate(connection_string, connection);

			connection.Close();
		}

		private static void Separate(string connection_string, SqlConnection connection)
		{
			string cmd = "SELECT movie_id, title, release_date, first_name, last_name FROM Movies, Directors WHERE director = director_id ";

			SqlCommand command = new SqlCommand(cmd, connection);

			SqlDataReader reader = command.ExecuteReader();
			//Выводим заголовки из таблицы
			for (int i = 0; i < reader.FieldCount; i++)
				Console.Write(reader.GetName(i) + "\t");
			Console.WriteLine();
			//Выводим содержимое таблицы
			while (reader.Read())
			{
				for (int i = 0; i < reader.FieldCount; i++)
					Console.Write($"{reader[i]}\t\t");
				Console.WriteLine();
			}
			reader.Close();

			command.CommandText = "SELECT COUNT(*) FROM Movies";
			//command.ExecuteScalar() возвращает одно значение
			Console.WriteLine($"Количество записей:\t{command.ExecuteScalar()}");
		}
	}
}
