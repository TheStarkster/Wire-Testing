using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class Create_Report_List_Value : Form
    {
        int Paramter_Name_ID = 0;
        int P_Id = 0;
        int Rand_Numb_ = 0;
        int Report_Id_Update = 0;
        int isUpdate = 0;
        bool isTrueFalse = false;
        DataTable Dt_List = new DataTable();
        public Create_Report_List_Value(int ID,int Product_ID,int Rand_Numb,int ReportID_update)
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            InitializeComponent();
            Paramter_Name_ID = ID;
            P_Id = Product_ID;
            Report_Id_Update = ReportID_update;
            Rand_Numb_ = Rand_Numb;
            isTrueFalse = db.tb_ParamListValues.FirstOrDefault(u => u.ParamNameID == Paramter_Name_ID).tb_ParamList.tb_ParamTypes.ParamName == "True False" ? true : false;
            if (isTrueFalse == true)
            {
                List_Parameter_Values.Visible = false;
                gridColumn1.Visible = false;
            }
            BindList();

            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView1.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridView1.OptionsSelection.EnableAppearanceHideSelection = false;
            gridView1.OptionsSelection.MultiSelect = true;
        }
        public void BindList()
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            //tb_ParamValue objParamValues = new tb_ParamValue();
            Dt_List = new DataTable();
            Dt_List.Columns.Add("ID");
            Dt_List.Columns.Add("Self_ID");
            Dt_List.Columns.Add("Self_ID_From_ParamList");
            Dt_List.Columns.Add("Name");
            Dt_List.Columns.Add("Val");
            Dt_List.Columns.Add("Type");
            Dt_List.Columns.Add("Tolerance");
            Dt_List.Columns.Add("Result");

            var obj_1 = from u in db.tb_ValuesOfReport_Lists
                        where u.ValRepID == Paramter_Name_ID && u.ReportID == Report_Id_Update
                        select new
                        {
                            u.ParamNameID,
                            self_ID = u.ID,
                            u.tb_ParamList.Name,
                            u.tb_ParamList.tb_ParamTypes.ParamName,
                            u.Val1,
                            u.Tolerance,
                            Result = db.tb_ParamListValues.FirstOrDefault(x => x.ParamNameID == u.ID).Val == "true" ? "Pass" : "Fail"
                        };
            if (obj_1.Count() > 0)
            {
                isUpdate = 1;
                foreach (var elem in obj_1.ToList())
                {
                    Dt_List.Rows.Add(new object[]{
                        elem.ParamNameID,elem.self_ID,"",elem.Name,elem.Val1,elem.ParamName,elem.Tolerance,elem.Result
                    });
                }
            }
            else {

                var obj = from u in db.tb_ParamList
                          where u.ParamNameID == Paramter_Name_ID
                          select new
                          {
                              u.ID,
                              u.ParamNameID,
                              self_Id = u.ID,
                              u.Name,
                              u.tb_ParamTypes.ParamName,
                              Result = db.tb_ParamListValues.FirstOrDefault(x => x.ParamListID == u.ID).Val == "true" ? "Fail" : "Pass"
                          };

                foreach (var elem in obj.ToList())
                {
                    Dt_List.Rows.Add(new object[]{
                        elem.ParamNameID,"",elem.ID,elem.Name,"",elem.ParamName,"",elem.Result
                    });
                }
                
            }
            gridControl1.DataSource = Dt_List;
            
        }
        public void Submit()
        {
            int Count = 0;
            //Editing Data Table According to True False Selected...
            foreach (DataRow elem in Dt_List.Rows)
            {
                if (elem["Type"].ToString() == "True False")
                {
                    elem["Val"] = gridView1.IsRowSelected(Count) == true ? "true" : "false";
                }
                Count++;
            }
            CreateReport.List_Values = Dt_List;
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            Submit();
            MessageBox.Show("Record Added Succesesfully!");
        }
        private void Create_Report_List_Value_Load(object sender, EventArgs e)
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            BindList();
            if (isUpdate == 0)
            {
                label4.Text = db.tb_ParamName.FirstOrDefault(u => u.ID == Paramter_Name_ID).Name;
            }
            else
            {
                label4.Text = db.tb_ValuesOfReports.FirstOrDefault(u => u.ID == Paramter_Name_ID).tb_ParamName.Name;
            }
        }

        private void gridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName == "Val")
            {
                double price = 0;
                if (!Double.TryParse(e.Value as String, out price))
                {
                    e.Valid = false;
                    e.ErrorText = "Only numeric values are accepted!";
                }
            }
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                (gridControl1.FocusedView as ColumnView).FocusedRowHandle++;
                e.Handled = true;
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            GridView grd = (GridView)sender;
            var a = grd.GetDataRow(e.ControllerRow);
            int ParamListID = Convert.ToInt32(a["Self_ID_From_ParamList"]);
            int ParamNameID = Convert.ToInt32(a["ID"]);
            var obj = db.tb_ParamListValues.FirstOrDefault(u => u.ParamListID == ParamListID && u.ParamNameID == ParamNameID);
            if (gridView1.IsRowSelected(e.ControllerRow))
            {
                if (obj.Val == "true")
                {
                    gridView1.SetRowCellValue(e.ControllerRow, "Result", "Pass");
                }
                else
                {
                    gridView1.SetRowCellValue(e.ControllerRow, "Result", "Fail");
                }
            }
            else
            {
                if (obj.Val == "true")
                {
                    gridView1.SetRowCellValue(e.ControllerRow, "Result", "Fail");
                }
                else
                {
                    gridView1.SetRowCellValue(e.ControllerRow, "Result", "Pass");
                }
            }
        }
    }
}
