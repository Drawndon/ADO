using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Academy
{
	public partial class MainForm : Form
	{
		DBTools.Connector connector;
		public MainForm()
		{
			InitializeComponent();
			connector = new DBTools.Connector(ConfigurationManager.ConnectionStrings["PV_521_Import"].ConnectionString);
			dgvDirections.DataSource = connector.Select("*", "Directions");
		}
	}
}
