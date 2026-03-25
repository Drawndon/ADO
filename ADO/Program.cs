using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			Connector connector = new Connector(connection_string);

			Console.WriteLine(connector.GetPrimaryKeyColumnName("Directors"));
			Console.WriteLine(connector.GetPrimaryKeyColumnName("Movies"));

			//connector.Insert($@"INSERT Directors (director_id, first_name, last_name) VALUES ({connector.GetNextPrimaryKey("Directors")}, N'Guy', N'Richie');");
			connector.Insert
				(
				"Directors",
				"director_id,first_name,last_name",
				$"{connector.GetNextPrimaryKey("Directors")}, John, Singleton"
				);

			Console.WriteLine($"PK макс:\t{connector.GetMaxPrimaryKey("Directors")}");

			/*Из таблицы Directors
			string cmd = "SELECT * FROM Directors";
			Из таблицы Movies
			string cmd = "SELECT * FROM Movies";*/

			//string cmd = "SELECT movie_id, title, release_date, first_name, last_name FROM Movies, Directors WHERE director = director_id ";

			//connector.Select(cmd);
			connector.Select("*", "Directors");
			Console.WriteLine($"Количество записей: {connector.Scalar("SELECT COUNT(*) FROM Directors")}");

			connector.Select
				(
				"title, release_date, first_name, last_name",
				"Movies, Directors",
				"director = director_id"
				);
			Console.WriteLine($"Количество записей: {connector.Scalar("SELECT COUNT(*) FROM Movies")}");

			//command.CommandText = "SELECT COUNT(*) FROM Movies";
			//command.ExecuteScalar() возвращает одно значение
			//Console.WriteLine($"Количество записей:\t{command.ExecuteScalar()}");

			//connection.Close();
		}

	}
}
