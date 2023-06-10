using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using ExcelDataReader;
using OfficeOpenXml;


namespace ReadWrireFile
{
    public class HandleFile
    {

        public static void readExcel(List<Staff> staffs, string path)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                var package = new ExcelPackage(new FileInfo(path));

                ExcelWorksheet workSheet = package.Workbook.Worksheets[0];
                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    try
                    {
                        int j = 1;
                        string id = workSheet.Cells[i, j++].Value.ToString();
                        string name = workSheet.Cells[i, j++].Value.ToString();                   
                        string birthdayTemp = workSheet.Cells[i, j++].Value.ToString();
                        DateTime birthday;
                        DateTime.TryParseExact(birthdayTemp, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthday);
                        string address = workSheet.Cells[i, j++].Value.ToString();
                        string phone = workSheet.Cells[i, j++].Value.ToString();
                        string gender = workSheet.Cells[i, j++].Value.ToString();
                        Staff staff = new Staff(id, name, birthday, address, phone, gender);
                        staffs.Add(staff);

                    }
                    catch (Exception exe)
                    {
                        MessageBox.Show(exe.Message);
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        public static void exportExcel(List<Staff> staffs)
        {
            {
                string filePath = "";
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "Excel | *.xlsx | Excel 2003 | *.xls";

                if (dialog.ShowDialog() == DialogResult.OK) ;
                {
                    filePath = dialog.FileName;
                }

                if (string.IsNullOrEmpty(filePath))
                {
                    MessageBox.Show("Đường dẫn báo cáo không hợp lệ");
                    return;
                }
                try
                {
                    using (ExcelPackage p = new ExcelPackage())
                    {
                        p.Workbook.Properties.Author = "Long Lê Đăng";
                        p.Workbook.Properties.Title = "Báo cáo thống kê";
                        p.Workbook.Worksheets.Add("Test sheet");
                        ExcelWorksheet ws = p.Workbook.Worksheets[0];
                        ws.Name = "Test sheet";

                        string[] arrColumnHeader = {
                                                "ID",
                                                "Name",
                                                "Birthday",
                                                "Address",
                                                "Phone",
                                                "Gender"
                        };

                        var countColHeader = arrColumnHeader.Count();

                        ws.Cells[1, 1].Value = "Thống kê thông tin nhân viên";
                        ws.Cells[1, 1, 1, countColHeader].Merge = true;
                        ws.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;

                        int colIndex = 1;
                        int rowIndex = 2;

                        foreach (var item in arrColumnHeader)
                        {
                            var cell = ws.Cells[rowIndex, colIndex];
                            cell.Value = item;
                            colIndex++;
                        }

                        foreach (var item in staffs)
                        {
                            colIndex = 1;
                            rowIndex++;
                            ws.Cells[rowIndex, colIndex++].Value = item.ID;
                            ws.Cells[rowIndex, colIndex++].Value = item.Name;
                            ws.Cells[rowIndex, colIndex++].Value = item.Birth.ToShortDateString();
                            ws.Cells[rowIndex, colIndex++].Value = item.Adress;
                            ws.Cells[rowIndex, colIndex++].Value = item.Phone;
                            ws.Cells[rowIndex, colIndex++].Value = item.Gender;
                        }

                        Byte[] bin = p.GetAsByteArray();
                        File.WriteAllBytes(filePath, bin);
                    }
                    MessageBox.Show("Xuất excel thành công!");
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
        }
        public static void readFile(List<Staff> staffs, string path)
        {
            try
            {
                StreamReader reader = new StreamReader(path, Encoding.UTF8);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] arr = line.Split(';');
                    Staff staff = new Staff(arr[0], arr[1], DateTime.ParseExact(arr[2], "M/d/yyyy", CultureInfo.InvariantCulture), arr[3], arr[4], arr[5]);
                    staffs.Add(staff);
                }
                reader.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public static void writeFile(Staff staff, string path)
        {
            try
            {
                StreamWriter writer = new StreamWriter(path, true, Encoding.UTF8);
                string line = staff.ToString();
                writer.WriteLine(line); 
                writer.Close(); 
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
