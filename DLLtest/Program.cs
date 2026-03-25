using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using DBTools;

namespace DLLtest
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Connector connector = new Connector
				(
				ConfigurationManager.ConnectionStrings["Movies_PV_521"].ConnectionString
				);
			connector.Select("SELECT * FROM Directors");


			Connector academy_connector = new Connector
				(
				ConfigurationManager.ConnectionStrings["PV_521_Import"].ConnectionString
				);
			academy_connector.Select("SELECT * FROM Disciplines");
		}
	}
}
