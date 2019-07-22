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
    public partial class Degree : Form
    {
        int update = 0;
        int ID = 0;
        public Degree()
        {
            InitializeComponent();
            BindGridview();
        }
        public void BindGridview()
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var objDegree = from u in db.tb_Degree
                            orderby u.ID descending
                            where u.Enable == 1
                               select new
                               {
                                   u.ID,
                                   u.Type,
                                   u.CreatedDate,
                                   u.Status
                               };
            gridControl1.DataSource = objDegree.ToList();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            if (!String.IsNullOrWhiteSpace(txtDegree.Text) && txtDegree.Text != "")
            {
                if (update != 0)
                {
                    var obj_Degree = db.tb_Degree.FirstOrDefault(u => u.Type == txtDegree.Text && u.ID != ID && u.Enable == 1);
                    if (obj_Degree == null)
                    {
                        Update(ID);
                        update = 0;
                    }
                    else
                    {
                        MessageBox.Show("This Degree Name Already Exists!");
                        Clear();
                    }
                }
                else
                {
                    var obj_Degree = db.tb_Degree.FirstOrDefault(u => u.Type == txtDegree.Text && u.Enable == 1);
                    if (obj_Degree == null)
                    {
                        Submit();
                    }
                    else
                    {
                        MessageBox.Show("This Degree Name Already Exists!");
                        Clear();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Enter Degree Name!");
                Clear();
            }
            Clear();
            BindGridview();
        }
        public void Submit()
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            tb_Degree objDegree = new tb_Degree();
            objDegree.Type = txtDegree.Text;
            objDegree.CreatedDate = DateTime.Now.Date;
            objDegree.CreateTime = DateTime.Now;
            objDegree.Status = "Active";
            objDegree.Enable = 1;
            objDegree.Enable = 1;
            db.tb_Degree.Add(objDegree);
            db.SaveChanges();
        }
        public void Delete(int ID)
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var DeleteData = db.tb_Degree.FirstOrDefault(u => u.ID == ID);
            DeleteData.Status = "InActive";
            db.SaveChanges();
        }
        public void Update(int ID)
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var updateData = db.tb_Degree.FirstOrDefault(u => u.ID == ID);
            updateData.Type = txtDegree.Text;
            updateData.CreatedDate = DateTime.Now.Date;
            db.SaveChanges();
        }
        public void SetData(int ID)
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var Setdata = db.tb_Degree.FirstOrDefault(u => u.ID == ID);
            txtDegree.Text = Setdata.Type;
        }
        public void Clear()
        {
            txtDegree.Text = "";
            ID = 0;
            update = 0;
        }

        private void repositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int Deg_Id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID"));
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            if (db.tb_Degree.FirstOrDefault(u => u.ID == Deg_Id && u.Enable == 1).Status == "Active")
            {
                txtDegree.Text = db.tb_Degree.FirstOrDefault(u => u.ID == Deg_Id && u.Enable == 1).Type;
                ID = Deg_Id;
                update++;
            }
            else
            {
                MessageBox.Show("Cannot Edit Inactive Data!");
            }
           
        }

        private void repositoryItemButtonAction_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int Deg_Id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID"));
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var DeleteData = db.tb_Degree.FirstOrDefault(u => u.ID == Deg_Id && u.Enable == 1);
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

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int ID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID"));
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var obj = db.tb_Degree.FirstOrDefault(u => u.ID == ID && u.Enable == 1);
            obj.Enable = 0;
            db.SaveChanges();
            BindGridview();
        }
    
    }
}
