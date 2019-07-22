using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WireLibrary;

namespace WireTestingProject
{
    public partial class Parameter_Naming : Form
    {
        int ID = 0;
        int Delete = 0;
        //int g_autofocused_ID = 0;
        public Parameter_Naming()
        {
      
            InitializeComponent();
            BindWirename();
            BindParamTypes();
            BindCatagoryTypes();
            BindUnit();
            BindGrid();
            //txtFormulaBar.Enabled = false;
            //Bind Visiblity combo Box...
            Dictionary<string, string> VisibleDict = new Dictionary<string, string>();
            VisibleDict.Add("0", "---Select Visiblity---");
            VisibleDict.Add("1", "Yes");
            VisibleDict.Add("2", "No");
            comboBox5.DataSource = VisibleDict.ToList();
            comboBox5.DisplayMember = "Value";
            comboBox5.ValueMember = "Key";

            
        }
        public void BindWirename()
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var objWirenameDetail = from u in db.tb_Wirename
                                    orderby u.ID descending
                                    where u.Status == "Active" && u.Enable == 1
                                    select new
                                    {
                                        u.Wirename,
                                        u.ID,
                                    };
            Dictionary<string, string> WireNameDict = new Dictionary<string, string>();
            WireNameDict.Add("0","---Select Wire Name---");
            foreach (var elem in objWirenameDetail.ToList())
            {
                WireNameDict.Add(elem.ID.ToString(), elem.Wirename);
            }
            comboBox1.DataSource = WireNameDict.ToList();
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
        }
        public void BindUnit()
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var objParamDetail = from u in db.tb_Unit
                                 orderby u.ID descending
                                 select new
                                 {
                                     u.Unit,
                                     u.ID,
                                 };
            Dictionary<string, string> UnitDict = new Dictionary<string, string>();
            UnitDict.Add("0", "---Select Unit---");
            foreach (var elem in objParamDetail.ToList())
            {
                UnitDict.Add(elem.ID.ToString(), elem.Unit);
            }
            comboBox4.DataSource = UnitDict.ToList();
            comboBox4.DisplayMember = "Value";
            comboBox4.ValueMember = "Key";
         
        }
        public void BindParamTypes()
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var objParamDetail = from u in db.tb_ParamTypes
                                 orderby u.ID descending
                                 select new
                                 {
                                     u.ParamName,
                                     u.ID,
                                 };
            Dictionary<string, string> ParamNameDict = new Dictionary<string, string>();
            ParamNameDict.Add("0", "---Select Parameter Name---");
            foreach (var elem in objParamDetail.ToList())
            {
                ParamNameDict.Add(elem.ID.ToString(), elem.ParamName);
            }
            comboBox2.DataSource = ParamNameDict.ToList();
            comboBox2.DisplayMember = "Value";
            comboBox2.ValueMember = "Key";
        }

        public void BindCatagoryTypes()
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var objParamDetail = from u in db.tb_Catagory
                                 orderby u.ID descending
                                 select new
                                 {
                                     u.CatagoryName,
                                     u.ID,
                                 };
            Dictionary<string, string> CatagoryDict = new Dictionary<string, string>();
            CatagoryDict.Add("0", "---Select Parameter Name---");
            foreach (var elem in objParamDetail.ToList())
            {
                CatagoryDict.Add(elem.ID.ToString(), elem.CatagoryName);
            }
            comboBox3.DataSource = CatagoryDict.ToList();
            comboBox3.DisplayMember = "Value";
            comboBox3.ValueMember = "Key";
        }
       
        public void BindGrid()
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var objWireParam = from u in db.tb_ParamName
                               orderby u.ID descending
                               where u.Enable == 1
                               select new
                               {
                                   Parameter_ID = u.ID,
                                   Wire_Name = u.tb_Wirename.Wirename,
                                   Parameter_Name = u.Name,
                                   Parameter_Type = u.tb_ParamTypes.ParamName,
                                   Catagory = u.tb_Catagory.CatagoryName,
                                   u.tb_Unit.Unit,
                                   ShowParam = u.ShowParam == 1 ? "Yes" : "No",
                                   u.Status,
                               };
            var objWireParam1 = from u in objWireParam.ToList()
                               select new
                               {
                                   u.Parameter_ID,
                                   u.Wire_Name,
                                   u.Parameter_Name,
                                   u.Parameter_Type,
                                   u.Catagory,
                                   u.Unit,
                                   u.ShowParam,
                                   u.Status,
                               };
            gridParamType.DataSource = objWireParam1.ToList();
        }
        public void Submit()
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            int TypeID = Convert.ToInt32(comboBox2.SelectedValue);
            int WireID = Convert.ToInt32(comboBox1.SelectedValue);
            int CatagoryID = Convert.ToInt32(comboBox3.SelectedValue);
            int UnitID = Convert.ToInt32(comboBox4.SelectedValue);
            int Visibilty = Convert.ToInt32(comboBox5.SelectedValue);
            var obj = db.tb_ParamName.FirstOrDefault(u => u.Type == TypeID && u.WireID == WireID && u.CatagoryID == CatagoryID && u.Name == txtParamName.Text && u.UnitID == UnitID && u.Enable == 1);
            if (obj == null)
            {
                tb_ParamName objParamName = new tb_ParamName();
                objParamName.Name = txtParamName.Text;
                objParamName.Type = Convert.ToInt32(comboBox2.SelectedValue);
                objParamName.WireID = Convert.ToInt32(comboBox1.SelectedValue);
                objParamName.CatagoryID = Convert.ToInt32(comboBox3.SelectedValue);
                objParamName.UnitID = Convert.ToInt32(comboBox4.SelectedValue);
                objParamName.ShowParam = Visibilty;
                objParamName.Enable = 1;
                objParamName.Status = "Active";
                objParamName.CreatedDate = DateTime.Now.Date;
                db.tb_ParamName.Add(objParamName);
                db.SaveChanges();
            }
            else {
                MessageBox.Show("This Parameter Already Exists!");
            }
        }
        public void Update(int ID)
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            tb_ParamName objParamName = db.tb_ParamName.FirstOrDefault(u => u.ID == ID && u.Enable == 1);
            objParamName.Name = txtParamName.Text;
            objParamName.Type = Convert.ToInt32(comboBox2.SelectedValue);
            objParamName.WireID = Convert.ToInt32(comboBox1.SelectedValue);
            objParamName.CatagoryID = Convert.ToInt32(comboBox3.SelectedValue);
            objParamName.UnitID = Convert.ToInt32(comboBox4.SelectedValue);
            objParamName.ShowParam = Convert.ToInt32(comboBox5.SelectedValue);
            
            db.SaveChanges();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            WireTestSoftwareDBEntities db= new WireTestSoftwareDBEntities();
            int WirenameID = Convert.ToInt32(comboBox1.SelectedValue);
            int ParamType = Convert.ToInt32(comboBox2.SelectedValue);
            int CatagoryID = Convert.ToInt32(comboBox3.SelectedValue);
            int UnitID = Convert.ToInt32(comboBox4.SelectedValue);
            int Visibilty = Convert.ToInt32(comboBox5.SelectedValue);
            
            if (WirenameID > 0 && ParamType > 0 && CatagoryID > 0 && txtParamName.Text != "")
            {

                if (ID == 0 && Delete == 0)
                {
                    var obj = db.tb_ParamName.FirstOrDefault(u => u.WireID == WirenameID && u.tb_ParamTypes.ID == ParamType && u.CatagoryID == CatagoryID && u.Name == txtParamName.Text && u.UnitID == UnitID && u.Enable == 1);
                    if (obj == null)
                    {
                        Submit();
                    }
                    else
                    {
                        MessageBox.Show("This Parameter Name Already Exists!");
                    }
                }
                else
                {
                    var obj = db.tb_ParamName.FirstOrDefault(u => u.WireID == WirenameID && u.tb_ParamTypes.ID == ParamType && u.CatagoryID == CatagoryID && u.Name == txtParamName.Text && u.UnitID == Visibilty && ID != ID && u.Enable == 1);
                    if (obj == null)
                    {
                        Update(ID);
                        ID = 0;
                    }
                    else
                    {
                        MessageBox.Show("Cannot Update Same Value!");
                    }
                   
                }
                BindGrid();
                Clear();
            }
            else
            {
                MessageBox.Show("Please Enter Values!");
            }
        }
        public void Clear()
        {
            comboBox1.SelectedValue = "0";
            comboBox2.SelectedValue = "0";
            comboBox3.SelectedValue = "0";
            comboBox4.SelectedValue = "0";
            txtParamName.Text = "";
        }
        private void gridParamType_Click(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Btn_Edit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int ParamID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Parameter_ID"));
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            if (db.tb_ParamName.FirstOrDefault(u => u.ID == ParamID && u.Enable == 1).Status == "Active")
            {
                    var objGetVal = db.tb_ParamValue.FirstOrDefault(u=>u.ParamNameID == ParamID);
                    if (objGetVal != null)
                    {
                        comboBox2.Enabled = false;
                    }
                    else
                    {
                        comboBox2.Enabled = true;
                    }
                    comboBox1.SelectedValue = db.tb_ParamName.FirstOrDefault(u => u.ID == ParamID && u.Enable == 1).WireID.ToString();
                    comboBox2.SelectedValue = db.tb_ParamName.FirstOrDefault(u => u.ID == ParamID && u.Enable == 1).Type.ToString();
                    comboBox3.SelectedValue = db.tb_ParamName.FirstOrDefault(u => u.ID == ParamID && u.Enable == 1).CatagoryID.ToString();
                    txtParamName.Text = db.tb_ParamName.FirstOrDefault(u => u.ID == ParamID && u.Enable == 1).Name;
                    comboBox4.SelectedValue = db.tb_ParamName.FirstOrDefault(u => u.ID == ParamID && u.Enable == 1).UnitID.ToString();
                    comboBox5.SelectedValue = db.tb_ParamName.FirstOrDefault(u => u.ID == ParamID && u.Enable == 1).ShowParam.ToString();

                    ID = ParamID;
            }
            else
            {
                MessageBox.Show("Cannot Edit Inactive Data!");
            }
        }

        private void Btn_Delete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Delete = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Parameter_ID"));
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var DeleteData = db.tb_ParamName.FirstOrDefault(u => u.ID == Delete && u.Enable == 1);
            if (DeleteData.Status == "InActive")
            {
                DeleteData.Status = "Active";
            }
            else
            {
                DeleteData.Status = "InActive";
            }
            db.SaveChanges();
            BindGrid();
        }

        private void BtnType_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int ID_List = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Parameter_ID"));
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            string paraName = db.tb_ParamName.FirstOrDefault(u => u.ID == ID_List && u.Enable == 1).tb_ParamTypes.ParamName;
            if (paraName == "List")
            {
                Parameter_List_Naming plm = new Parameter_List_Naming(ID_List);
                plm.ShowDialog();
            }
            else
            {
                MessageBox.Show("its not List Type!");
            }
        }

        private void Parameter_Naming_Load(object sender, EventArgs e)
        {

        }

        private void repositoryItemButtonEdit3_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int Product_ID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Parameter_ID"));
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var obj = db.tb_ParamName.FirstOrDefault(u => u.ID == Product_ID && u.Enable == 1);
            obj.Enable = 0;
            db.SaveChanges();
            BindGrid();
        }
        bool CreateFormula = false;
        string Formula = "";
        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (CreateFormula == true)
            {
                string ParameterName = Convert.ToString(gridView1.GetFocusedRowCellValue("Parameter_Name"));
                int ParameterID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Parameter_ID"));
                Formula += "ID_"+Convert.ToString(ParameterID);
                //CreateFormulaString(ParameterName);
            }
        }
        //public void CreateFormulaString(string line)
        //{
        //    string InsideTxt = txtFormulaBar.Text;
        //    InsideTxt += " "+line+" ";
        //    txtFormulaBar.Text = InsideTxt;
        //}
        public void SubmitFormulaFor(int ID)
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var obj = db.tb_ParamName.FirstOrDefault(u => u.ID == ID);
            obj.Formula = Formula;
            db.SaveChanges();
        }
        int FormulaForID = 0;
        private void BtnFormula_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            CreateFormula = true;
            FormulaForID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Parameter_ID"));
            //paramNameforFormula.Text = (string)gridView1.GetFocusedRowCellValue("Parameter_Name");
            //txtFormulaBar.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SubmitFormulaFor(FormulaForID);
        }
        public void RemoveLastChar()
        {
            string s = txtParamName.Text;

            if (s.Length > 1)
            {
                s = s.Substring(0, s.Length - 1);
            }
            else
            {
                s = "0";
            }

            txtParamName.Text = s;
        }
        private void txtFormulaBar_KeyPress(object sender, KeyPressEventArgs e)
        {
            string CharKey = e.KeyChar.ToString();
            if (CharKey == "+" || CharKey == "-" || CharKey == "\b" || CharKey == "*" || CharKey == "/" || CharKey == "1" || CharKey == "2" || CharKey == "3" || CharKey == "4" || CharKey == "5" || CharKey == "6" || CharKey == "7" || CharKey == "8" || CharKey == "9" || CharKey == "0")
            {
                Formula += "," + CharKey + ",";
            }
            else
            {
                MessageBox.Show("Enter Arithmetical Operation Only!");
                RemoveLastChar();
            }
        }

        private void txtFormulaBar_KeyDown(object sender, KeyEventArgs e)
        {
    
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Formula = "";
            FormulaForID = 0;
            //txtFormulaBar.Text = "";
        }

        private void txtFormulaBar_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
