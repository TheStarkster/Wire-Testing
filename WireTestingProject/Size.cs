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
    public partial class Size : Form
    {
        int update = 0;
        int ID = 0;
        public Size()
        {
            InitializeComponent();
            BindGridview();
        }
        public void BindGridview()
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var objSize = from u in db.tb_Size
                          where u.Enable == 1 
                            select new
                            {
                                u.ID,
                                _Size = u.Size,
                                u.CreatedDate,
                                u.Status
                            };
            gridControl1.DataSource = objSize.ToList();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            if (!String.IsNullOrWhiteSpace(txtSize.Text) && txtSize.Text != "")
            {
                if (update != 0)
                {
                    var obj_Degree = db.tb_Size.FirstOrDefault(u => u.Size == txtSize.Text && u.ID != ID && u.Enable == 1);
                    if (obj_Degree == null)
                    {
                        Update(ID);
                        update = 0;
                    }
                    else
                    {
                        MessageBox.Show("This Size Name Already Exists!");
                        Clear();
                    }
                }
                else
                {
                    var obj_Degree = db.tb_Size.FirstOrDefault(u => u.Size == txtSize.Text && u.Enable == 1);
                    if (obj_Degree == null)
                    {
                        Submit();
                    }
                    else
                    {
                        MessageBox.Show("This Size Name Already Exists!");
                        Clear();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Enter Size Name!");
                Clear();
            }
            Clear();
            BindGridview();
        }
        public void Submit()
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            tb_Size objsize = new tb_Size();
            objsize.Size = txtSize.Text;
            objsize.CreatedDate = DateTime.Now.Date;
            objsize.CreateTime = DateTime.Now;
            objsize.Status = "Active";
            objsize.Enable = 1;
            db.tb_Size.Add(objsize);
            db.SaveChanges();
        }
        public void Update(int ID)
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var updateData = db.tb_Size.FirstOrDefault(u => u.ID == ID && u.Enable == 1);
            updateData.Size = txtSize.Text;
            updateData.CreatedDate = DateTime.Now.Date;
            db.SaveChanges();
        }
    
        public void Clear()
        {
            txtSize.Text = "";
            ID = 0;
            update = 0;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Clear();
        }
        private void repositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int Size_Id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID"));
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            if (db.tb_Size.FirstOrDefault(u => u.ID == Size_Id).Status == "Active")
            {
                txtSize.Text = db.tb_Size.FirstOrDefault(u => u.ID == Size_Id && u.Enable == 1).Size;
                ID = Size_Id;
                update++;
            }
            else
            {
                MessageBox.Show("Cannot Edit Inactive Data!");
            }
            
        }

        private void repositoryItemButtonDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int Size_Id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID"));
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var DeleteData = db.tb_Size.FirstOrDefault(u => u.ID == Size_Id && u.Enable == 1);
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

        private void repositoryItemButtonEdit1_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int ID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID"));
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var obj = db.tb_Size.FirstOrDefault(u => u.ID == ID && u.Enable == 1);
            obj.Enable = 0;
            db.SaveChanges();
            BindGridview();
        }
    }
}
