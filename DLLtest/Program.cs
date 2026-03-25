using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DBTools;

namespace DLLtest
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Connector connector = new Connector
				(
				"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Movies_PV_521;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
				);
			connector.Select("SELECT * FROM Directors");
		}
	}
}
