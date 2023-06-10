using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadWrireFile
{
    public partial class frmReadFile : Form
    {
        List<Staff> staffs = new List<Staff>();
        public frmReadFile()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Staff staff = new Staff(id.Text, name.Text, birth.Value, address.Text, phone.Text, gender.SelectedItem.ToString());
                staffs.Add(staff);
                // HandleFile.writeFile(staff, Application.StartupPath + "//CSV.csv");
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = staffs;
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HandleFile.readExcel(staffs, Application.StartupPath + "\\test.xlsx");
            dataGridView1.DataSource = staffs;    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HandleFile.exportExcel(staffs);
        }
    }
}
