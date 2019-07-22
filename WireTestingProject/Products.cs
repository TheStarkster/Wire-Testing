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
    
    public partial class Products : Form
    {
        int ID = 0;
        int update = 0;
        public static string ComboBox1;
        public static string ComboBox2;
        public static string ComboBox3;
        public static string ComboBox6;
        public static bool SetDataFromDashboard;
        public Products()
        {
            InitializeComponent();
            BindComboBox();
            BindGridview();

            Dictionary<string, string> GradeDict = new Dictionary<string, string>();
            GradeDict.Add("0", "---Select Grade---");
            GradeDict.Add("1", "1/Fine");
            GradeDict.Add("2", "2/Medium");
            GradeDict.Add("3", "1/Thick");
            comboBox6.DataSource = GradeDict.ToList();
            comboBox6.ValueMember = "Key";
            comboBox6.DisplayMember = "Value";
        }
        public string GetGrade(int ID)
        {
            string grade = "";
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var Grade = db.tb_Product.FirstOrDefault(u => u.ID == ID && u.Enable == 1).Grade;
            if (Grade == 1)
            {
                grade = "1/Fine";
            }
            else if (Grade == 2)
            {
                grade = "2/Medium";
            }
            else if (Grade == 3)
            {
                grade = "3/Thick";
            }
            return grade;
        }
        public void BindComboBox()
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var obj1 = from u in db.tb_Wirename
                       orderby u.ID descending
                       where u.Status == "Active" && u.Enable == 1
                       select new
                       {
                           u.Wirename,
                           u.ID
                       };
            Dictionary<string,string> Role = new Dictionary<string,string>();
            Role.Add("0","---Select Wire Name---");
            foreach (var elem in obj1.ToList())
            {
                Role.Add(elem.ID.ToString(), elem.Wirename);
            }
            comboBox1.DataSource = Role.ToList();
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";

            var obj2 = from u in db.tb_Size
                       orderby u.ID descending
                       where u.Status == "Active" && u.Enable == 1
                       select new
                       {
                           u.ID,
                           u.Size,
                       };
            Dictionary<string, string> Role_1 = new Dictionary<string, string>();
            Role_1.Add("0", "---Select Size---");
            foreach (var elem in obj2.ToList())
            {
                Role_1.Add(elem.ID.ToString(), elem.Size);
            }
            comboBox3.DataSource = Role_1.ToList();
            comboBox3.DisplayMember = "Value";
            comboBox3.ValueMember = "Key";

            var obj3 = from u in db.tb_Degree
                       orderby u.ID descending
                       where u.Status == "Active" && u.Enable == 1
                       select new
                       {
                           u.ID,
                           u.Type,
                       };

            Dictionary<string, string> Role_2 = new Dictionary<string, string>();
            Role_2.Add("0", "---Select Degree---");
            foreach (var elem in obj3.ToList())
            {
                Role_2.Add(elem.ID.ToString(), elem.Type);
            }
            comboBox2.DataSource = Role_2.ToList();
            comboBox2.DisplayMember = "Value";
            comboBox2.ValueMember = "Key";
        }
        public void Clear()
        {
            comboBox1.SelectedValue = "0";
            comboBox2.SelectedValue = "0";
            comboBox3.SelectedValue = "0";
        }
        public void Delete(int ID)
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var DeleteData = db.tb_Product.FirstOrDefault(u => u.ID == ID);
            DeleteData.Status = "InActive";
            db.SaveChanges();
        }
        public void BindGridview()
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var objProduct = from u in db.tb_Product
                             orderby u.ID descending
                             where u.Enable == 1
                          select new
                          {
                              u.ID,
                              u.tb_Wirename.Wirename,
                              u.tb_Size.Size,
                              u.tb_Degree.Type,
                              u.CreatedDate,
                              u.Status,
                          };
            var objProduct1 = from u in objProduct.ToList()
                          select new
                          {
                              u.ID,
                              u.Wirename,
                              u.Size,
                              u.Type,
                              u.CreatedDate,
                              u.Status,
                              Grade = GetGrade(u.ID)
                          };
            gridControl1.DataSource = objProduct1.ToList();
        }
        public void SetData(int ID)
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var Setdata = db.tb_Product.FirstOrDefault(u => u.ID == ID && u.Enable == 1);
            comboBox1.SelectedValue = Setdata.WirenameID;
            comboBox2.SelectedValue = Setdata.DegreeID;
            comboBox3.SelectedValue = Setdata.SizeID;
            comboBox6.SelectedValue = Setdata.Grade;
        }
        public void Submit()
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            tb_Product objProduct = new tb_Product();
            objProduct.WirenameID = Convert.ToInt32(comboBox1.SelectedValue);
            objProduct.SizeID = Convert.ToInt32(comboBox3.SelectedValue);
            objProduct.DegreeID = Convert.ToInt32(comboBox2.SelectedValue);
            objProduct.Grade = Convert.ToInt32(comboBox6.SelectedValue);
            objProduct.CreatedDate = DateTime.Now.Date;
            objProduct.Enable = 1;
            objProduct.Status = "Active";
            db.tb_Product.Add(objProduct);
            db.SaveChanges();
        }
        public void Update(int ID)
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var updateData = db.tb_Product.FirstOrDefault(u => u.ID == ID && u.Enable == 1);
            updateData.WirenameID = Convert.ToInt32(comboBox1.SelectedValue);
            updateData.SizeID = Convert.ToInt32(comboBox3.SelectedValue);
            updateData.DegreeID = Convert.ToInt32(comboBox2.SelectedValue);
            updateData.Grade = Convert.ToInt32(comboBox6.SelectedValue);
            updateData.CreatedDate = DateTime.Now.Date;
            db.SaveChanges();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            int WirenameID = Convert.ToInt32(comboBox1.SelectedValue);
            int SizeID = Convert.ToInt32(comboBox3.SelectedValue);
            int DegreeID = Convert.ToInt32(comboBox2.SelectedValue);
            int Grade = Convert.ToInt32(comboBox6.SelectedValue);
            if (WirenameID > 0 || SizeID > 0 || DegreeID > 0)
            {
                if (update != 0)
                {
                    var obj = db.tb_Product.FirstOrDefault(u => u.tb_Wirename.ID == WirenameID && u.tb_Degree.ID == DegreeID && u.tb_Size.ID == SizeID && u.ID != ID && u.Grade == Grade && u.Enable == 1);
                    if (obj == null)
                    {
                        Update(ID);
                        update = 0;
                    }
                }
                else
                {
                    var obj_Degree = db.tb_Product.FirstOrDefault(u => u.tb_Wirename.ID == WirenameID && u.tb_Degree.ID == DegreeID && u.tb_Size.ID == SizeID && u.ID != ID && u.Grade == Grade && u.Enable == 1);
                    if (obj_Degree == null)
                    {
                        Submit();
                    }
                    else
                    {
                        MessageBox.Show("This Wire Name Already Exists!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Fill All The Inputs!");
            }
            Clear();
            BindGridview();
        }
        
        private void repositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            
            
                int Product_ID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID"));
                WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
                if (db.tb_Product.FirstOrDefault(u => u.ID == Product_ID && u.Enable == 1).Status == "Active")
                {
                    comboBox1.SelectedValue = db.tb_Product.FirstOrDefault(u => u.ID == Product_ID && u.Enable == 1).WirenameID.ToString();
                    comboBox2.SelectedValue = db.tb_Product.FirstOrDefault(u => u.ID == Product_ID && u.Enable == 1).DegreeID.ToString();
                    comboBox3.SelectedValue = db.tb_Product.FirstOrDefault(u => u.ID == Product_ID && u.Enable == 1).SizeID.ToString();
                    comboBox6.SelectedValue = db.tb_Product.FirstOrDefault(u => u.ID == Product_ID && u.Enable == 1).Grade.ToString();
                    ID = Product_ID;
                    update++;
                }
                else
                {
                    MessageBox.Show("Cannot Edit Inactive Data!");
                }
            
        }

        private void repositoryItemButtonAction_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int product_ID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID"));
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var DeleteData = db.tb_Product.FirstOrDefault(u => u.ID == product_ID && u.Enable == 1);
            if (DeleteData.Status == "InActive")
            {
                DeleteData.Status = "Active";
            }
            else
            {
                DeleteData.Status = "InActive";
            }
            db.SaveChanges();
            BindGridview();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Products_Load(object sender, EventArgs e)
        {
            if (SetDataFromDashboard == true)
            {
                int Product_ID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID"));
                WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
                if (db.tb_Product.FirstOrDefault(u => u.ID == Product_ID && u.Enable == 1).Status == "Active")
                {
                    comboBox1.SelectedValue = ComboBox1;
                    comboBox2.SelectedValue = ComboBox2;//db.tb_Product.FirstOrDefault(u => u.ID == Product_ID).DegreeID.ToString();
                    comboBox3.SelectedValue = ComboBox3;//db.tb_Product.FirstOrDefault(u => u.ID == Product_ID).SizeID.ToString();
                    comboBox6.SelectedValue = ComboBox6;//db.tb_Product.FirstOrDefault(u => u.ID == Product_ID).Grade.ToString();
                    ID = Product_ID;
                    update++;
                }
                else
                {
                    MessageBox.Show("Cannot Edit Inactive Data!");
                }
            }
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int ID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID"));
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var obj = db.tb_Product.FirstOrDefault(u => u.ID == ID && u.Enable == 1);
            obj.Enable = 0;
            db.SaveChanges();
            BindGridview();
        }
    }
}
