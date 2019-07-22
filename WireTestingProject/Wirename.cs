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
    public partial class Wirename : Form
    {
        int update = 0;
        int ID = 0;
        public Wirename()
        {
            InitializeComponent();
            BindGridview();
        }
        public void BindGridview(){
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var objWireNames = from u in db.tb_Wirename
                               orderby u.ID descending
                               where u.Enable == 1
                               select new
                               {
                                   u.ID,
                                   u.Wirename,
                                   u.CreatedDate,
                                   u.Status
                               };
            gridControl1.DataSource = objWireNames.ToList();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            if (!String.IsNullOrWhiteSpace(txtWirename.Text) && txtWirename.Text != "")
            {
                if (update != 0)
                {
                    var obj_Degree = db.tb_Wirename.FirstOrDefault(u => u.Wirename == txtWirename.Text && u.ID != ID && u.Enable == 1);
                    if (obj_Degree == null)
                    {
                        Update(ID);
                        update = 0;
                    }
                    else {
                        MessageBox.Show("This Wire Name Already Exists!");
                        Clear();
                     }
                }
                else
                {
                    var obj_Degree = db.tb_Wirename.FirstOrDefault(u => u.Wirename == txtWirename.Text && u.Enable == 1);
                    if (obj_Degree == null)
                    {
                        Submit();
                    }
                    else
                    {
                        MessageBox.Show("This Wire Name Already Exists!");
                        Clear();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Enter Wire Name!");
                Clear();
            }
            Clear();
            BindGridview();
        }
        public void Submit() {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            tb_Wirename objWirename = new tb_Wirename();
            objWirename.Wirename = txtWirename.Text;
            objWirename.CreatedDate = DateTime.Now.Date;
            objWirename.CreateTime = DateTime.Now;
            objWirename.Enable = 1;
            objWirename.Status = "Active";
            db.tb_Wirename.Add(objWirename);
            db.SaveChanges();
        }
        public void Delete(int ID)
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var DeleteData = db.tb_Wirename.FirstOrDefault(u => u.ID == ID && u.Enable == 1);
            DeleteData.Status = "InActive";
            db.SaveChanges();
        }
        public void Update(int ID)
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var updateData = db.tb_Wirename.FirstOrDefault(u => u.ID == ID && u.Enable == 1);
            updateData.Wirename = txtWirename.Text;
            updateData.CreatedDate = DateTime.Now.Date;
            db.SaveChanges();
        }
        public void SetData(int ID)
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var Setdata = db.tb_Wirename.FirstOrDefault(u => u.ID == ID && u.Enable == 1);
            txtWirename.Text = Setdata.Wirename;
        }
        public void Clear()
        {
            txtWirename.Text = "";
            ID = 0;
            update = 0;
        }

        private void repositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int Wire_ID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID"));
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var DeleteData = db.tb_Wirename.FirstOrDefault(u => u.ID == Wire_ID && u.Enable == 1);
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

        private void repositoryItemButtonEdit_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int Wire_Id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID"));
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            if (db.tb_Wirename.FirstOrDefault(u => u.ID == Wire_Id && u.Enable == 1).Status == "Active")
            {
                txtWirename.Text = db.tb_Wirename.FirstOrDefault(u => u.ID == Wire_Id && u.Enable == 1).Wirename;
                ID = Wire_Id;
                update++;
            }
            else
            {
                MessageBox.Show("Cannot Edit Inactive Data!");
            }
        }

        private void repositoryItemButtonEdit1as_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int Wire_Id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID"));
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var obj = db.tb_Wirename.FirstOrDefault(u => u.ID == Wire_Id && u.Enable == 1);
            obj.Enable = 0;
            db.SaveChanges();
            BindGridview();
        }
       
    }
}
