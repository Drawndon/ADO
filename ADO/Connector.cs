using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace ADO
{
	internal class Connector
	{
		string connection_string;
		SqlConnection connection;
		public Connector(string connection_string)
		{
			Console.WriteLine(connection_string);
			this.connection_string = connection_string;
			connection = new SqlConnection(connection_string);
		}
		public void Select(string cmd)
		{
			connection.Open();
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
			connection.Close();
		}

		public object Scalar(string cmd)
		{
			object result = null;
			connection.Open();

			SqlCommand command = new SqlCommand(cmd, connection);
			result = command.ExecuteScalar(); //Выполнение скалярного запроса. 

			connection.Close();
			return result;
		}
	}
}
