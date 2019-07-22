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
    public partial class Report_List : Form
    {
        public Report_List()
        {
            InitializeComponent();
            BindList();
        }
        public void BindList()
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();

            var obj = from u in db.tb_Reports
                      orderby u.ID descending
                      where u.Enable == 1
                      select new
                      {
                          u.ID,
                          u.CreatedDate,
                          u.ReportName,
                          Product = u.tb_Product.tb_Wirename.Wirename + " | " + u.tb_Product.tb_Size.Size + " | " + u.tb_Product.tb_Degree.Type,
                          u.Result,
                      };
            gridControl1.DataSource = obj.ToList();
        }

        private void BtnOpen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int ID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID"));
            Wire_Test_Report obj = new Wire_Test_Report(ID);
            obj.ShowDialog();
        }

        private void BtnEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int ID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID"));
            CreateReport obj = new CreateReport(ID);
            obj.ShowDialog();
        }
        private void BtnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int ID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID"));
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var obj = db.tb_Reports.FirstOrDefault(u => u.ID == ID);
            obj.Enable = 0;
            db.SaveChanges();
            BindList();
        }
    }
}
