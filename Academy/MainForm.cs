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
		Query[] queries =
		{
			new Query
				(
				"last_name, first_name, middle_name, group_name,direction_name",
				"Students,Groups,Directions",
				"[group]=group_id AND direction=direction_id"
				),
			new Query
				(
				"*",
				"Groups,Directions",
				"direction=direction_id"
				),
			new Query("*", "Directions"),
			new Query("*", "Disciplines"),
			new Query("*", "Teachers"),
		};
		string[] status_messages =
			{
			"Количество студентов",
			"Количество групп", 
			"Количество направлений",
			"Количество дисциплин",
			"Количество преподавателей" 
		};
		DataGridView[] tables;
		DBTools.Connector connector;
		public MainForm()
		{
			InitializeComponent();
			tables = new DataGridView[] { dgvStudents, dgvGroups, dgvDirections, dgvDisciplines, dgvTeachers };
			connector = new DBTools.Connector(ConfigurationManager.ConnectionStrings["PV_521_Import"].ConnectionString);

			//dgvDirections.DataSource = connector.Select("*", "Directions");
			//toolStripStatusLabel.Text = $"Количество направлений обучения: {dgvDirections.Rows.Count - 1}"; //Этот способ работает быстро
			//Этот способ нагружает базу//toolStripStatusLabel.Text = $"Количество направлений обучения: {connector.Scalar("SELECT COUNT(*) FROM Directions")}";
			tabControl_SelectedIndexChanged(tabControl, null);
		}

		private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			int i = tabControl.SelectedIndex;
			//tables[i].DataSource = connector.Select($"SELECT * FROM {tabControl.SelectedTab.Text}");
			tables[i].DataSource = connector.Select(queries[i].ToString());
			toolStripStatusLabel.Text = $"{status_messages[i]}: {tables[i].RowCount - 1}";
		}
	}
}
