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
    
    public partial class Parameter_List_Naming : Form
    {
        int UpdateId = 0;
        int ID_PARAMNAME = 0;
        public Parameter_List_Naming(int ID_Param_List)
        {
            InitializeComponent();
            ID_PARAMNAME = ID_Param_List;
            BindComoBox();

            Dictionary<string, string> VisibleDict = new Dictionary<string, string>();
            VisibleDict.Add("0", "---Select Group Type---");
            VisibleDict.Add("1", "Yes");
            VisibleDict.Add("2", "No");
        }
        public void BindComoBox()
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var obj = from u in db.tb_ParamTypes
                      select new
                      {
                          u.ID,
                          u.ParamName
                      };
            comboBox1.DisplayMember = "ParamName";
            comboBox1.ValueMember = "ID";
            comboBox1.DataSource = obj.ToList();
        }
        public void BindList()
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var obj = from u in db.tb_ParamList
                      where u.ParamNameID == ID_PARAMNAME && u.Enable == 1
                      select new
                      {
                          u.ID,
                          u.Name,
                          u.Status,
                          u.CreatedDate,
                          Type = u.tb_ParamTypes.ParamName
                      };
            gridControl1.DataSource = obj.ToList();
        }
        public void Update(int ID) {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var obj = db.tb_ParamList.FirstOrDefault(u => u.ID == ID && u.Enable == 1);
            if (obj != null)
            {
                obj.Name = txtListName.Text;
                obj.ParamTypeID = Convert.ToInt32(comboBox1.SelectedValue);
            }
            db.SaveChanges();
        }
        public void Submit()
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            tb_ParamList objList = new tb_ParamList();
            objList.Name = txtListName.Text;
            objList.ParamNameID = ID_PARAMNAME;
            objList.ParamTypeID = Convert.ToInt32(comboBox1.SelectedValue);
            objList.Status = "Active";
            objList.Enable = 1;
            objList.CreatedDate = DateTime.Today;
            db.tb_ParamList.Add(objList);
            db.SaveChanges();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtListName.Text) && txtListName.Text != "")
            {
                if (UpdateId != 0)
                {
                    WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
                    var obj = db.tb_ParamList.FirstOrDefault(u => u.Name == txtListName.Text && u.ID != UpdateId && u.Enable == 1);
                    if (obj == null)
                    {
                        Update(UpdateId);
                    }
                    else
                    {
                        MessageBox.Show("Please Check Your Input!");
                    }
                }
                else
                {
                    WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
                    int Combo_ID = Convert.ToInt32(comboBox1.SelectedValue);
                    var obj = db.tb_ParamList.FirstOrDefault(u => u.Name == txtListName.Text && u.ParamTypeID == Combo_ID && u.Enable == 1);
                    if (obj == null)
                    {
                        Submit();
                    }
                    else
                    {
                        MessageBox.Show("Please Check Your Input!");
                    }

                }
            }
            else
            {
                MessageBox.Show("Can't Save Empty!");
            }
            
            BindList();
            Clear();
        }
        public void Clear()
        {
            txtListName.Text = "";
            UpdateId = 0;
        }
        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void Parameter_List_Naming_Load(object sender, EventArgs e)
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            txtParamName.Text = db.tb_ParamName.FirstOrDefault(u => u.ID == ID_PARAMNAME && u.Enable == 1).Name;
            BindList();
        }

        private void Btn_Delete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int ID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID"));
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var obj = db.tb_ParamList.FirstOrDefault(u => u.ID == ID && u.Enable == 1);
            if (obj.Status == "InActive")
            {
                obj.Status = "Active";
            }
            else 
            {
                obj.Status = "InActive";
            }
            db.SaveChanges();
            BindList();
        }

        private void Btn_Edit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int ID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID"));
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var obj = db.tb_ParamList.FirstOrDefault(u => u.ID == ID && u.Enable == 1);
            if (obj.Status == "Active")
            {
                txtListName.Text = obj.Name;
                UpdateId = ID;
            }
            else {
                MessageBox.Show("Can't Update InActive Items!");
            }
        }

        private void repositoryItemButtonEdit1_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int Product_ID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID"));
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var obj = db.tb_ParamList.FirstOrDefault(u => u.ID == Product_ID && u.Enable == 1);
            obj.Enable = 0;
            db.SaveChanges();
        }
    }
}
