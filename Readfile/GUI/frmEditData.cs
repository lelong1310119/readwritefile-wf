using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmEditData : Form
    {
        private string id;
        public frmEditData(string id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public bool checkNumber(string input)
        {
            int result;
            return int.TryParse(input, out result);
        }

        public bool IsStringDouble(string input)
        {
            double result;
            return double.TryParse(input, out result);
        }

        private void txtLaneCode_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLaneCode.Text.Trim()))
            {
                e.Cancel = false;
                txtLaneCode.Text = "0";
                errorProvider1.SetError(txtLaneCode, null);
            }
            else if (!checkNumber(txtLaneCode.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLaneCode, "Vui lòng nhập LaneCode đúng kiểu số nguyên");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtLaneCode, null);
            }
        }

        private void txtFromKP_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFromKP.Text.Trim()))
            {
                e.Cancel = false;
                txtFromKP.Text = "0";
                errorProvider1.SetError(txtFromKP, null);
            }
            else if (!IsStringDouble(txtFromKP.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFromKP, "Vui lòng nhập FromKP đúng kiểu số thực");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFromKP, null);
            }
        }

        private void txtToKP_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtToKP.Text.Trim()))
            {
                e.Cancel = false;
                txtToKP.Text = "0";
                errorProvider1.SetError(txtToKP, null);
            }
            else if (!IsStringDouble(txtToKP.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtToKP, "Vui lòng nhập ToKP đúng kiểu số thực");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtToKP, null);
            }
        }

        private void txtLength_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLength.Text.Trim()))
            {
                e.Cancel = false;
                txtLength.Text = "0";
                errorProvider1.SetError(txtLength, null);
            }
            else if (!IsStringDouble(txtLength.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLength, "Vui lòng nhập Length đúng kiểu số thực");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtLength, null);
            }
        }

        private void txtPav_Type_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPav_Type.Text.Trim()))
            {
                e.Cancel = false;
                txtPav_Type.Text = "0";
                errorProvider1.SetError(txtPav_Type, null);
            }
            else if (!checkNumber(txtPav_Type.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPav_Type, "Vui lòng nhập PavType đúng kiểu số nguyên");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtPav_Type, null);
            }
        }

        private void txtIRI_Validating(object sender, CancelEventArgs e)
        {

        }

        private void txtTV_Validating(object sender, CancelEventArgs e)
        {

        }

        private void txtCrackFatigueL_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCrackFatigueL.Text.Trim()))
            {
                e.Cancel = false;
                txtCrackFatigueL.Text = "0";
                errorProvider1.SetError(txtCrackFatigueL, null);
            }
            else if (!IsStringDouble(txtCrackFatigueL.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCrackFatigueL, "Vui lòng nhập CrackFatigueL đúng kiểu số thực");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCrackFatigueL, null);
            }
        }

        private void txtCrackFatigueM_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCrackFatigueM.Text.Trim()))
            {
                e.Cancel = false;
                txtCrackFatigueM.Text = "0";
                errorProvider1.SetError(txtCrackFatigueM, null);
            }
            else if (!IsStringDouble(txtCrackFatigueM.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCrackFatigueM, "Vui lòng nhập CrackFatigueM đúng kiểu số thực");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCrackFatigueM, null);
            }
        }

        private void txtCrackFatigueH_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCrackFatigueH.Text.Trim()))
            {
                e.Cancel = false;
                txtCrackFatigueH.Text = "0";
                errorProvider1.SetError(txtCrackFatigueH, null);
            }
            else if (!IsStringDouble(txtCrackFatigueH.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCrackFatigueH, "Vui lòng nhập CrackFatigueH đúng kiểu số thực");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCrackFatigueH, null);
            }
        }

        private void txtCrackTransverseL_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCrackTransverseL.Text.Trim()))
            {
                e.Cancel = false;
                txtCrackTransverseL.Text = "0";
                errorProvider1.SetError(txtCrackTransverseL, null);
            }
            else if (!IsStringDouble(txtCrackTransverseL.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCrackTransverseL, "Vui lòng nhập CrackTransverseL đúng kiểu số thực");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCrackTransverseL, null);
            }
        }

        private void txtCrackTransverseM_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCrackTransverseM.Text.Trim()))
            {
                e.Cancel = false;
                txtCrackTransverseM.Text = "0";
                errorProvider1.SetError(txtCrackTransverseM, null);
            }
            else if (!IsStringDouble(txtCrackTransverseM.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCrackTransverseM, "Vui lòng nhập CrackTransverseM đúng kiểu số thực");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCrackTransverseM, null);
            }
        }

        private void txtCrackTransverseH_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCrackTransverseH.Text.Trim()))
            {
                e.Cancel = false;
                txtCrackTransverseH.Text = "0";
                errorProvider1.SetError(txtCrackTransverseH, null);
            }
            else if (!IsStringDouble(txtCrackTransverseH.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCrackTransverseH, "Vui lòng nhập CrackTransverseH đúng kiểu số thực");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCrackTransverseH, null);
            }
        }

        private void txtCrackMiscellanenousL_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCrackMiscellanenousL.Text.Trim()))
            {
                e.Cancel = false;
                txtCrackMiscellanenousL.Text = "0";
                errorProvider1.SetError(txtCrackMiscellanenousL, null);
            }
            else if (!IsStringDouble(txtCrackMiscellanenousL.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCrackMiscellanenousL, "Vui lòng nhập CrackMiscellanenousL đúng kiểu số thực");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCrackMiscellanenousL, null);
            }
        }

        private void txtCrackMiscellanenousM_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCrackMiscellanenousM.Text.Trim()))
            {
                e.Cancel = false;
                txtCrackMiscellanenousM.Text = "0";
                errorProvider1.SetError(txtCrackMiscellanenousM, null);
            }
            else if (!IsStringDouble(txtCrackMiscellanenousM.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCrackMiscellanenousM, "Vui lòng nhập CrackMiscellanenousM đúng kiểu số thực");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCrackMiscellanenousM, null);
            }
        }

        private void txtCrackMiscellanenousH_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCrackMiscellanenousH.Text.Trim()))
            {
                e.Cancel = false;
                txtCrackMiscellanenousH.Text = "0";
                errorProvider1.SetError(txtCrackMiscellanenousH, null);
            }
            else if (!IsStringDouble(txtCrackMiscellanenousH.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCrackMiscellanenousH, "Vui lòng nhập CrackMiscellanenousH đúng kiểu số thực");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCrackMiscellanenousH, null);
            }
        }

        private void txtRutDepth_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtRutDepth.Text.Trim()))
            {
                e.Cancel = false;
                txtRutDepth.Text = "0";
                errorProvider1.SetError(txtRutDepth, null);
            }
            else if (!IsStringDouble(txtRutDepth.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtRutDepth, "Vui lòng nhập RutDepth đúng kiểu số thực");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtRutDepth, null);
            }
        }

        private void txtDate_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDate.Text.Trim()))
            {
                e.Cancel = false;
                txtDate.Text = "";
                errorProvider1.SetError(txtDate, null);
            }
            else if (!DateTime.TryParseExact(txtDate.Text.Trim(), "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDate, "Vui lòng nhập ngày tháng đúng định dạng");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtDate, null);
            }
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            using (var context = new readfileEntities1())
            {
                try
                {
                    pav_condition record = context.pav_condition.FirstOrDefault(r => r.sectionId == id);

                    if (record != null)
                    {
                        record.sectionId2 = txtSectionID2.Text.Trim();
                        record.sectionId3 = txtSectionID3.Text.Trim();
                        record.roadId = txtRoadID.Text.Trim();
                        record.direction = txtDirection.Text.Trim();
                        record.laneCode = int.Parse(txtLaneCode.Text.Trim());
                        record.fromKP = float.Parse(txtFromKP.Text.Trim());
                        record.toKP = float.Parse(txtToKP.Text.Trim());
                        record.length = float.Parse(txtLength.Text.Trim());
                        record.date = txtDate.Text.Trim();
                        record.pavType = int.Parse(txtPav_Type.Text.Trim());
                        record.structure = txtStructure.Text.Trim();
                        record.crackFatigueL = float.Parse(txtCrackFatigueL.Text.Trim());
                        record.crackFatigueM = float.Parse(txtCrackFatigueM.Text.Trim());
                        record.crackFatigueH = float.Parse(txtCrackFatigueH.Text.Trim());
                        record.crackMiscellanenousL = float.Parse(txtCrackMiscellanenousL.Text.Trim());
                        record.crackMiscellanenousM = float.Parse(txtCrackMiscellanenousM.Text.Trim());
                        record.crackMiscellanenousH = float.Parse(txtCrackMiscellanenousH.Text.Trim());
                        record.crackTransverseL = float.Parse(txtCrackTransverseL.Text.Trim());
                        record.crackTransverseM = float.Parse(txtCrackTransverseM.Text.Trim());
                        record.crackTransverseH = float.Parse(txtCrackTransverseH.Text.Trim());
                        record.rutDepth = float.Parse(txtRutDepth.Text.Trim());
                        record.TV = int.Parse(txtTV.Text.Trim());
                        record.IRI = int.Parse(txtIRI.Text.Trim());
                        record.BIC = txtBIC.Text.Trim();
                        record.EIC = txtEIC.Text.Trim();
                        record.status = 1;
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

        private void frmEditData_Load(object sender, EventArgs e)
        {
            using (var context = new readfileEntities1())
            {
                pav_condition result = context.pav_condition.FirstOrDefault(p => p.sectionId == id);

                if (result != null)
                {
                    txtSectionID.Text = result.sectionId;
                    txtSectionID2.Text = result.sectionId2;
                    txtSectionID3.Text = result.sectionId3;
                    txtRoadID.Text = result.roadId;
                    txtDirection.Text = result.direction;
                    txtLaneCode.Text = result.laneCode.ToString();
                    txtLength.Text = result.length.ToString();
                    txtFromKP.Text = result.fromKP.ToString();
                    txtToKP.Text = result.toKP.ToString();
                    txtStructure.Text = result.structure;
                    txtPav_Type.Text = result.pavType.ToString();
                    txtDate.Text = result.date;
                    txtCrackFatigueH.Text = result.crackFatigueH.ToString();
                    txtCrackFatigueL.Text = result.crackFatigueL.ToString();
                    txtCrackFatigueM.Text = result.crackFatigueM.ToString();
                    txtCrackMiscellanenousH.Text = result.crackMiscellanenousH.ToString();
                    txtCrackMiscellanenousL.Text = result.crackMiscellanenousL.ToString();
                    txtCrackMiscellanenousM.Text = result.crackMiscellanenousM.ToString();
                    txtIRI.Text = result.IRI.ToString();
                    txtTV.Text = result.TV.ToString();
                    txtRutDepth.Text = result.rutDepth.ToString();
                    txtCrackTransverseH.Text = result.crackTransverseH.ToString();
                    txtCrackTransverseL.Text = result.crackTransverseL.ToString();
                    txtCrackTransverseM.Text = result.crackTransverseM.ToString();
                    txtEIC.Text = result.EIC;
                    txtBIC.Text = result.BIC;
                }
            }
        }
    }
}
