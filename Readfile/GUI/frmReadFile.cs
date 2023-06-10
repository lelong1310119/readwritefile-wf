using GUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using Z.BulkOperations.Internal.InformationSchema;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GUI
{
    public partial class frmReadFile : Form
    {
        private DataTable dataTable = new DataTable();
        private string pathFolder;
        private int page = 1;
        private int pageSize = 6;
        private int maxPage;
        private int col = 1;
        private int count;

        public frmReadFile()
        {
            InitializeComponent();
        }

        private void btn_select_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.Description = "Chọn thư mục lưu trữ";
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
            folderBrowserDialog1.ShowNewFolderButton = true;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string folderPath = folderBrowserDialog1.SelectedPath;
                textBox1.Text = folderPath;
                pathFolder = folderPath;
            }
            //using (var dialog = new CommonOpenFileDialog())
            //{
            //    dialog.IsFolderPicker = true;
            //    dialog.RestoreDirectory = true;
            //    if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            //    {
            //        textBox1.Text = dialog.FileName;
            //        pathFolder = dialog.FileName;
            //    }
            //}
        }

        public void LoadData()
        {
            dataTable.Clear();
            using (var context = new readfileEntities1())
            {
                List<pav_condition> result = context.pav_condition.OrderBy(x => x.sectionId).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                foreach (pav_condition pav in result)
                {
                    DataRow row = dataTable.NewRow();
                    row["Section_ID"] = pav.sectionId;
                    row["Section_ID2"] = pav.sectionId2;
                    row["Section_ID3"] = pav.sectionId3;
                    row["Road_ID"] = pav.roadId;
                    row["Direction"] = pav.direction;
                    row["Lane_Code"] = pav.laneCode;
                    row["From_KP"] = pav.fromKP;
                    row["To_KP"] = pav.toKP;
                    row["Length"] = pav.length;
                    row["Structure"] = pav.structure;
                    row["Pav_Type"] = pav.pavType;
                    row["Date"] = pav.date;
                    row["Crack_Fatigue_L"] = pav.crackFatigueL;
                    row["Crack_Fatigue_M"] = pav.crackFatigueM;
                    row["Crack_Fatigue_H"] = pav.crackFatigueH;
                    row["Crack_Transverse_L"] = pav.crackTransverseL;
                    row["Crack_Transverse_M"] = pav.crackTransverseM;
                    row["Crack_Transverse_H"] = pav.crackTransverseH;
                    row["Crack_Miscellanenous_L"] = pav.crackMiscellanenousL;
                    row["Crack_Miscellanenous_M"] = pav.crackMiscellanenousM;
                    row["Crack_Miscellanenous_H"] = pav.crackMiscellanenousH;
                    row["Rut_Depth"] = pav.rutDepth;
                    row["IRI"] = pav.IRI;
                    row["TV"] = pav.TV;
                    row["B_IC"] = pav.BIC;
                    row["E_IC"] = pav.EIC;
                    row["Status"] = pav.status;
                    dataTable.Rows.Add(row);
                }
            }
            dgvData.DataSource = dataTable;
            label2.Visible = false;
        }

        private void btn_readFile_Click(object sender, EventArgs e)
        {
            dgvData.LoadData();
            try
            {
                foreach (string filePath in Directory.GetFiles(pathFolder, "*.csv"))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string[] headers = reader.ReadLine().Split(',');
                        while (!reader.EndOfStream)
                        {
                            string[] rows = reader.ReadLine().Split(',');
                            DataRow dataRow = dataTable.NewRow();
                            for (int i = 0; i < headers.Length; i++)
                            {
                                dataRow[i] = rows[i];
                            }
                            dataRow[headers.Length] = 0;
                            dataTable.Rows.Add(dataRow);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (rdbBuklCopy.Checked)
            {
                try
                {
                    List<pav_condition> pav_Conditions = new List<pav_condition>();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        using (var context = new readfileEntities1())
                        {
                            var entity = context.pav_condition.Find(row[0].ToString());
                            if (entity != null)
                            {
                                continue;
                            }
                        }
                        pav_condition newPavCondition = new pav_condition
                        {
                            sectionId = row[0].ToString(),
                            sectionId2 = row[1].ToString(),
                            sectionId3 = row[2].ToString(),
                            roadId = row[3].ToString(),
                            direction = row[4].ToString(),
                            laneCode = int.TryParse(row[5].ToString(), out int result5) ? result5 : 0,
                            fromKP = float.TryParse(row[6].ToString(), out float result6) ? result6 : 0,
                            toKP = float.TryParse(row[7].ToString(), out float result7) ? result7 : 0,
                            length = float.TryParse(row[8].ToString(), out float result8) ? result8 : 0,
                            structure = row[9].ToString(),
                            pavType = int.TryParse(row[10].ToString(), out int result10) ? result10 : 0,
                            date = row[11].ToString(),
                            crackFatigueL = float.TryParse(row[12].ToString(), out float result12) ? result12 : 0,
                            crackFatigueM = float.TryParse(row[13].ToString(), out float result13) ? result13 : 0,
                            crackFatigueH = float.TryParse(row[14].ToString(), out float result14) ? result14 : 0,
                            crackTransverseL = float.TryParse(row[15].ToString(), out float result15) ? result15 : 0,
                            crackTransverseM = float.TryParse(row[16].ToString(), out float result16) ? result16 : 0,
                            crackTransverseH = float.TryParse(row[17].ToString(), out float result17) ? result17 : 0,
                            crackMiscellanenousL = float.TryParse(row[18].ToString(), out float result18) ? result18 : 0,
                            crackMiscellanenousM = float.TryParse(row[19].ToString(), out float result19) ? result19 : 0,
                            crackMiscellanenousH = float.TryParse(row[20].ToString(), out float result20) ? result20 : 0,
                            rutDepth = float.TryParse(row[21].ToString(), out float result21) ? result21 : 0,
                            IRI = int.TryParse(row[22].ToString(), out int result22) ? result22 : 0,
                            TV = int.TryParse(row[23].ToString(), out int result23) ? result23 : 0,
                            BIC = row[24].ToString(),
                            EIC = row[25].ToString(),
                            status = 0,
                        };
                        try
                        {
                            pav_Conditions.Add(newPavCondition);
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }
                    using (var dbContext = new readfileEntities1())
                    {
                        dbContext.BulkInsert(pav_Conditions);
                    }
                    MessageBox.Show("Success");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("bukl" + ex.Message);
                }
            }
            else
            {
                using (var context = new readfileEntities1())
                {
                    try
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            var entity = context.pav_condition.Find(row[0].ToString());
                            if (entity != null)
                            {
                                continue;
                            }
                            pav_condition newPavCondition = new pav_condition
                            {
                                sectionId = row[0].ToString(),
                                sectionId2 = row[1].ToString(),
                                sectionId3 = row[2].ToString(),
                                roadId = row[3].ToString(),
                                direction = row[4].ToString(),
                                laneCode = int.TryParse(row[5].ToString(), out int result5) ? result5 : 0,
                                fromKP = float.TryParse(row[6].ToString(), out float result6) ? result6 : 0,
                                toKP = float.TryParse(row[7].ToString(), out float result7) ? result7 : 0,
                                length = float.TryParse(row[8].ToString(), out float result8) ? result8 : 0,
                                structure = row[9].ToString(),
                                pavType = int.TryParse(row[10].ToString(), out int result10) ? result10 : 0,
                                date = row[11].ToString(),
                                crackFatigueL = float.TryParse(row[12].ToString(), out float result12) ? result12 : 0,
                                crackFatigueM = float.TryParse(row[13].ToString(), out float result13) ? result13 : 0,
                                crackFatigueH = float.TryParse(row[14].ToString(), out float result14) ? result14 : 0,
                                crackTransverseL = float.TryParse(row[15].ToString(), out float result15) ? result15 : 0,
                                crackTransverseM = float.TryParse(row[16].ToString(), out float result16) ? result16 : 0,
                                crackTransverseH = float.TryParse(row[17].ToString(), out float result17) ? result17 : 0,
                                crackMiscellanenousL = float.TryParse(row[18].ToString(), out float result18) ? result18 : 0,
                                crackMiscellanenousM = float.TryParse(row[19].ToString(), out float result19) ? result19 : 0,
                                crackMiscellanenousH = float.TryParse(row[20].ToString(), out float result20) ? result20 : 0,
                                rutDepth = float.TryParse(row[21].ToString(), out float result21) ? result21 : 0,
                                IRI = int.TryParse(row[22].ToString(), out int result22) ? result22 : 0,
                                TV = int.TryParse(row[23].ToString(), out int result23) ? result23 : 0,
                                BIC = row[24].ToString(),
                                EIC = row[25].ToString(),
                                status = 0,
                            };
                            try
                            {
                                context.pav_condition.Add(newPavCondition);
                            }
                            catch (Exception ex)
                            {
                                continue;
                            }
                        }
                        context.SaveChanges();
                        MessageBox.Show("Success");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            dataTable.Clear();
            resetPage();
            LoadData();
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i >= 0)
            {
                if (dgvData.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.ColumnIndex == dgvData.Columns["Edit"].Index)
                {
                    this.Hide();
                    frmEditData f = new frmEditData(dgvData.Rows[i].Cells["Column1"].Value.ToString());
                    f.ShowDialog();
                    this.Show();
                    LoadData();
                }
                if (dgvData.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.ColumnIndex == dgvData.Columns["Delete"].Index)
                {
                    DialogResult result = MessageBox.Show("Are You Sure to Delete this Record", "", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)
                    {
                        using (var dbContext = new readfileEntities1())
                        {
                            var recordToDelete = dbContext.pav_condition.Find(dgvData.Rows[i].Cells["Column1"].Value.ToString());
                            if (recordToDelete != null)
                            {
                                dbContext.pav_condition.Remove(recordToDelete);
                                dbContext.SaveChanges();
                            }
                        }
                        MessageBox.Show("Success");
                        resetPage();
                        if (page > maxPage) page = maxPage;
                        txtPage.Text = page.ToString();
                        LoadData();
                    }
                }
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            dataTable.Columns.Add("Section_ID");
            dataTable.Columns.Add("Section_ID2");
            dataTable.Columns.Add("Section_ID3");
            dataTable.Columns.Add("Road_ID");
            dataTable.Columns.Add("Direction");
            dataTable.Columns.Add("Lane_Code");
            dataTable.Columns.Add("From_KP");
            dataTable.Columns.Add("To_KP");
            dataTable.Columns.Add("Length");
            dataTable.Columns.Add("Structure");
            dataTable.Columns.Add("Pav_Type");
            dataTable.Columns.Add("Date");
            dataTable.Columns.Add("Crack_Fatigue_L");
            dataTable.Columns.Add("Crack_Fatigue_M");
            dataTable.Columns.Add("Crack_Fatigue_H");
            dataTable.Columns.Add("Crack_Transverse_L");
            dataTable.Columns.Add("Crack_Transverse_M");
            dataTable.Columns.Add("Crack_Transverse_H");
            dataTable.Columns.Add("Crack_Miscellanenous_L");
            dataTable.Columns.Add("Crack_Miscellanenous_M");
            dataTable.Columns.Add("Crack_Miscellanenous_H");
            dataTable.Columns.Add("Rut_Depth");
            dataTable.Columns.Add("IRI");
            dataTable.Columns.Add("TV");
            dataTable.Columns.Add("B_IC");
            dataTable.Columns.Add("E_IC");
            dataTable.Columns.Add("Status");
            rdbBuklCopy.Checked = true;
            label2.Visible = false;
            resetPage();
            txtPage.Text = "1";
            txtColumn.Text = "1";
            txtPageSize.Text = pageSize.ToString();
            LoadData();
            resetCol();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            page -= 1;
            txtPage.Text = page.ToString();
            btnNext.Enabled = true;
            if (page == 1) btnPrev.Enabled = false;
            LoadData();
        }

        public void resetPage()
        {
            using (var context = new readfileEntities1())
            {
                count = context.pav_condition.Count();
                maxPage = count % pageSize > 0 ? count / pageSize + 1 : count / pageSize;
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            page += 1;
            txtPage.Text = page.ToString();
            btnPrev.Enabled = true;
            if (page == maxPage) btnNext.Enabled = false;
            LoadData();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtPage.Text.Trim(), out int result) || int.Parse(txtPage.Text.Trim()) < 1 || int.Parse(txtPage.Text.Trim()) > maxPage) {
                label2.Text = $"Vui lòng nhập đúng số từ 1 đến {maxPage}";
                label2.Visible = true;
                txtPage.Text = page.ToString();
            } else
            {
                label2.Visible = false;
                page = int.Parse(txtPage.Text.Trim());
                LoadData();
            }
        }

        public void resetCol()
        {
            foreach (DataGridViewColumn column in dgvData.Columns)
            {
                column.Visible = false;
            }
            switch (col)
            {
                case 1:
                    dgvData.Columns["Column2"].Visible = true;
                    dgvData.Columns["Column3"].Visible = true;
                    dgvData.Columns["Column4"].Visible = true;
                    break;
                case 2:
                    dgvData.Columns["Column5"].Visible = true;
                    dgvData.Columns["Column6"].Visible = true;
                    dgvData.Columns["Column7"].Visible = true;
                    dgvData.Columns["Column8"].Visible = true;
                    break;
                case 3:
                    dgvData.Columns["Column9"].Visible = true;
                    dgvData.Columns["Column10"].Visible = true;
                    dgvData.Columns["Column11"].Visible = true;
                    dgvData.Columns["Column12"].Visible = true;
                    break;
                case 4:
                    dgvData.Columns["Column13"].Visible = true;
                    dgvData.Columns["Column14"].Visible = true;
                    dgvData.Columns["Column15"].Visible = true;
                    dgvData.Columns["Column16"].Visible = true;
                    break;
                case 5:
                    dgvData.Columns["Column17"].Visible = true;
                    dgvData.Columns["Column18"].Visible = true;
                    dgvData.Columns["Column19"].Visible = true;
                    break;
                case 6:
                    dgvData.Columns["Column20"].Visible = true;
                    dgvData.Columns["Column21"].Visible = true;
                    dgvData.Columns["Column22"].Visible = true;
                    break;
                case 7:
                    dgvData.Columns["Column23"].Visible = true;
                    dgvData.Columns["Column24"].Visible = true;
                    dgvData.Columns["Column25"].Visible = true;
                    dgvData.Columns["Column26"].Visible = true;
                    break;
                default:
                    break;
            }
            dgvData.Columns["Column1"].Visible = true;
            dgvData.Columns["Edit"].Visible = true;
            dgvData.Columns["Delete"].Visible = true;
        }
        private void btnPrevCollumn_Click(object sender, EventArgs e)
        {
            col -= 1;
            txtColumn.Text = col.ToString();
            btnNextColumn.Enabled = true;
            if (col == 1) btnPrevCollumn.Enabled = false;
            resetCol();
        }

        private void btnNextColumn_Click(object sender, EventArgs e)
        {
            col += 1;
            txtColumn.Text = col.ToString();
            btnPrevCollumn.Enabled = true;
            if (col == 7) btnNextColumn.Enabled = false;
            resetCol();
        }

        private void btnSetSize_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtPageSize.Text.Trim(), out int result) || int.Parse(txtPageSize.Text.Trim()) < 1 || int.Parse(txtPageSize.Text.Trim()) > count)
            {
                label2.Text = $"Vui lòng nhập đúng số từ 1 đến {count}";
                label2.Visible = true;
                txtPageSize.Text = pageSize.ToString();
            }
            else
            {
                page = 1;
                label2.Visible = false;
                pageSize = int.Parse(txtPageSize.Text.Trim());
                resetPage();
                LoadData();
            }
        }

        private void dgvData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dgvData.Columns[e.ColumnIndex].Name == "Column1")
            {
                string id = e.Value.ToString();
                using (var context = new readfileEntities1())
                {
                    pav_condition entity = context.pav_condition.Find(id);
                    if (entity != null)
                    {
                        if (entity.status == 1)
                        {
                            DataGridViewRow row = dgvData.Rows[e.RowIndex];
                            row.DefaultCellStyle.BackColor = Color.LightGreen;
                        } 
                    }
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
