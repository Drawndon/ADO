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

		public void Select(string fields, string tables, string condition = "")
		{
			string cmd = $"SELECT {fields} FROM {tables}";
			if (condition != "") cmd += $" WHERE {condition}";
			cmd += ";";
			Select(cmd);
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

		public string GetPrimaryKeyName(string table)
		{
			connection.Open();
			string cmd = $"SELECT * FROM {table}";
			SqlCommand command = new SqlCommand(cmd, connection);
			SqlDataReader reader = command.ExecuteReader();
			string pkName = reader.GetName(0);
			reader.Close();
			connection.Close();
			return pkName;
		}


		public int GetMaxPrimaryKey(string table)
		{
			string cmd = $"SELECT * FROM {table}";
			SqlCommand command = new SqlCommand(cmd, connection);
			connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			string pk_name = reader.GetName(0);
			reader.Close();
			connection.Close();
			return (int)Scalar($"SELECT MAX({pk_name}) FROM {table}");
		}

		public int GetNextPrimaryKey(string table)
		{
			return GetMaxPrimaryKey(table) + 1;
		}
		
		public void Insert(string cmd)
		{
			SqlCommand command = new SqlCommand (cmd, connection);
			connection.Open();
			try
			{
				command.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.GetType());
				Console.WriteLine(ex.Message);
				if (ex.GetType() == typeof(SqlException) && ex.Message.Contains(" _id"))
				{
					Console.WriteLine("Good");
				}
			}
			connection.Close();
		}
	}
}
