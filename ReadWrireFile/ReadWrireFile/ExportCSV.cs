using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadWrireFile
{
    public partial class ExportCSV : Form
    {
        public ExportCSV()
        {
            InitializeComponent();
        }

        private void btnReadData_Click(object sender, EventArgs e)
        {
            try
            {
                string tableName = textBox1.Text.ToString();
                string query = $"select * from {tableName}";
                DataTable dt = DataConnection.GetInstance().GetRecords(query);
                dgvData.DataSource = dt;
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);    
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "CSV file|*.csv";
                saveFileDialog1.Title = "Export to CSV";
                if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                string header = "";
                foreach (DataGridViewColumn column in dgvData.Columns)
                {
                    header += column.HeaderText + ",";
                }
                header = header.TrimEnd(',');
                sw.WriteLine(header);
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    string data = "";
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null)
                        {
                            data += cell.Value.ToString() + ",";
                        }
                        else
                        {
                            data += ",";
                        }
                    }
                    data = data.TrimEnd(',');
                    sw.WriteLine(data);
                }

                sw.Close();
                MessageBox.Show("Exported successfully to " + saveFileDialog1.FileName, "Export to CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (Exception ex)
            {
                MessageBox.Show("The table does not exist, please enter the correct table name", "Message");
            }
        }
    }
}
