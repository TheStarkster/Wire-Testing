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
    public partial class MasterPage : Form
    {
        public MasterPage(int Access)
        {
            InitializeComponent();
            BindGridview();
            BindReportGridView();
            if (Access == 0)
            {
                mastersToolStripMenuItem.Visible = false;
                paramtersToolStripMenuItem.Visible = false;
            }
        }
        public void BindGridview()
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var objSize = from u in db.tb_Product
                          orderby u.ID descending
                          where u.Enable == 1
                          select new
                          {
                              u.ID,
                              u.tb_Wirename.Wirename,
                              u.tb_Size.Size,
                              u.tb_Degree.Type,
                              u.CreatedDate,
                              u.Status
                          };
            var objSize1 = from u in objSize.ToList()
                           orderby u.ID descending
                          select new
                          {
                              u.ID,
                              u.Wirename,
                              u.Size,
                              u.Type,
                              u.CreatedDate,
                              Grade = GetGrade(u.ID),
                              u.Status
                          };
            gridControl1.DataSource = objSize1.ToList();
        }
        public string GetGrade(int ID)
        {
            string grade = "";
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var Grade = db.tb_Product.FirstOrDefault(u => u.ID == ID).Grade;
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
        public void BindReportGridView()
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var objReport = from u in db.tb_Reports
                            orderby u.ID descending
                          where u.Status == "Active" && u.Enable == 1
                          select new
                          {
                              u.ID,
                              u.ReportName,
                              u.CreatedDate,
                              u.Status,
                              ProductName = u.tb_Product.tb_Wirename.Wirename + " | " + u.tb_Product.tb_Size.Size + " | " + u.tb_Product.tb_Degree.Type,
                              u.Result,
                          };
            gridControl2.DataSource = objReport.ToList();
        }
        private void sizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Size sz = new Size();
            sz.ShowDialog();
        }

        private void wireNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Wirename wn = new Wirename();
            wn.ShowDialog();
        }

        private void degreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Degree dg = new Degree();
            dg.ShowDialog();
        }
        public void Delete(int ID)
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var DeleteData = db.tb_Size.FirstOrDefault(u => u.ID == ID);
            DeleteData.Status = "InActive";
            db.SaveChanges();
        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Products pd = new Products();
            pd.FormClosing += new FormClosingEventHandler(this.FormClosing_click);
            pd.ShowDialog();
        }
        private void createReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateReport cr = new CreateReport(0);
            cr.FormClosing += new FormClosingEventHandler(this.FormClosing_click);
            cr.ShowDialog();
        }
        public void FormClosing_click(object sender, FormClosingEventArgs evArgs) {
            BindReportGridView();
            BindGridview();
        }
        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report_List rl = new Report_List();
            rl.ShowDialog();
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void parameterValusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Parameter_Value pv = new Parameter_Value();
            pv.ShowDialog();
        }

        private void parameterNamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Parameter_Naming pn = new Parameter_Naming();
            pn.ShowDialog();
        }

        private void Btn_Update_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int Product_ID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID"));
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            if (db.tb_Product.FirstOrDefault(u => u.ID == Product_ID).Status == "Active")
            {
                Products.ComboBox1 = db.tb_Product.FirstOrDefault(u => u.ID == Product_ID).WirenameID.ToString();
                Products.ComboBox2 = db.tb_Product.FirstOrDefault(u => u.ID == Product_ID).DegreeID.ToString();
                Products.ComboBox3 = db.tb_Product.FirstOrDefault(u => u.ID == Product_ID).SizeID.ToString();
                Products.ComboBox6 = db.tb_Product.FirstOrDefault(u => u.ID == Product_ID).Grade.ToString();
                Products.SetDataFromDashboard = true;
                Products pd = new Products();
                pd.FormClosing += new FormClosingEventHandler(this.FormClosing_click);
                pd.ShowDialog();
            }
            else
            {
                MessageBox.Show("Cannot Edit Inactive Data!");
            }
        }
        private void Btn_Delete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int Product_ID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID"));
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var obj = db.tb_Product.FirstOrDefault(u=>u.ID == Product_ID);
            obj.Enable = 0;
            db.SaveChanges();
            BindGridview();
        }

        private void BtnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int ID = Convert.ToInt32(gridView2.GetFocusedRowCellValue("ID"));
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var obj = db.tb_Reports.FirstOrDefault(u => u.ID == ID);
            obj.Enable = 0;
            db.SaveChanges();
            BindReportGridView();
        }

        private void mastersToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void BtnStatus_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int ID = Convert.ToInt32(gridView2.GetFocusedRowCellValue("ID"));
            Wire_Test_Report obj = new Wire_Test_Report(ID);
            obj.ShowDialog();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login lg = new Login();
            this.Close();
            lg.ShowDialog();
        }
    }
}
