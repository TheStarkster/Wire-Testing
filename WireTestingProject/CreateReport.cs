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
    public partial class CreateReport : Form
    {
        int isUpdate = 0;
        static int Rand_Numb = 0;
        int Report_ID = 0;
        DataTable Dt_OtherValues;
        public static DataTable List_Values = new DataTable();
        public int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }
        public CreateReport(int Report_ID)
        {
            InitializeComponent();
            DisableGrids();
            BindProduct(); 
            Rand_Numb = GenerateRandomNo();
            if (Report_ID != 0)
            {
                SettingValuesToUpdate(Report_ID);
                isUpdate = 1;
            }

            //Create Data Table for Other Values...
            Dt_OtherValues = new DataTable();
            Dt_OtherValues.Columns.Add("Cover");
            Dt_OtherValues.Columns.Add("Customer");
            Dt_OtherValues.Columns.Add("OtherSpec");
            Dt_OtherValues.Columns.Add("InvNo");
            Dt_OtherValues.Columns.Add("InvDate");
            Dt_OtherValues.Columns.Add("Qty");
            Dt_OtherValues.Columns.Add("Winding");
            Dt_OtherValues.Columns.Add("Surface");
            Dt_OtherValues.Columns.Add("Edge");
            Dt_OtherValues.Columns.Add("Clean");
            Dt_OtherValues.Columns.Add("Polythene");
            Dt_OtherValues.Columns.Add("Box");
            Dt_OtherValues.Columns.Add("Wrapper");
            Dt_OtherValues.Columns.Add("Trace");
            Dt_OtherValues.Columns.Add("Weight");
            Dt_OtherValues.Columns.Add("Size");
            Dt_OtherValues.Columns.Add("Special");

            DataRow dr = Dt_OtherValues.NewRow();
            Dt_OtherValues.Rows.Add(dr);
        }
        string IsPassedOrNot = "";
        private void CreateReport_Load(object sender, EventArgs e)
        {
        }
        public void LocatePanel()
        {
            string[] Locations = { "4, 3", "314, 4", "624, 3", "4, 292", "314, 292", "627, 292" };
            //CountingGrids in Need...
            int count = -1;
            if (panel_Max.Visible == true)
            {
                count++;
                string[] points = Locations[count].Split(',');
                Point newPoints = new Point(int.Parse(points[0]), int.Parse(points[1]));
                panel_Max.Location = newPoints;

                gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
                gridView1.OptionsSelection.EnableAppearanceFocusedRow = false;
                gridView1.OptionsSelection.EnableAppearanceHideSelection = false;
                gridView1.OptionsSelection.MultiSelect = true;
            }
            if (panel_Min.Visible == true)
            {
                count++;
                string[] points = Locations[count].Split(',');
                Point newPoints = new Point(int.Parse(points[0]), int.Parse(points[1]));
                panel_Min.Location = newPoints;

                gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
                gridView2.OptionsSelection.EnableAppearanceFocusedRow = false;
                gridView2.OptionsSelection.EnableAppearanceHideSelection = false;
                gridView2.OptionsSelection.MultiSelect = true;
            }
            if (panel_List.Visible == true)
            {
                count++;
                string[] points = Locations[count].Split(',');
                Point newPoints = new Point(int.Parse(points[0]), int.Parse(points[1]));
                panel_List.Location = newPoints;

                gridView5.OptionsSelection.EnableAppearanceFocusedCell = false;
                gridView5.OptionsSelection.EnableAppearanceFocusedRow = false;
                gridView5.OptionsSelection.EnableAppearanceHideSelection = false;
                gridView5.OptionsSelection.MultiSelect = true;
            }
            if (panel_Btw.Visible == true)
            {
                count++;
                string[] points = Locations[count].Split(',');
                Point newPoints = new Point(int.Parse(points[0]), int.Parse(points[1]));
                panel_Btw.Location = newPoints;

                gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
                gridView3.OptionsSelection.EnableAppearanceFocusedRow = false;
                gridView3.OptionsSelection.EnableAppearanceHideSelection = false;
                gridView3.OptionsSelection.MultiSelect = true;
            }
            if (panel_Same_Value.Visible == true)
            {
                count++;
                string[] points = Locations[count].Split(',');
                Point newPoints = new Point(int.Parse(points[0]), int.Parse(points[1]));
                panel_Same_Value.Location = newPoints;

                gridView6.OptionsSelection.EnableAppearanceFocusedCell = false;
                gridView6.OptionsSelection.EnableAppearanceFocusedRow = false;
                gridView6.OptionsSelection.EnableAppearanceHideSelection = false;
                gridView6.OptionsSelection.MultiSelect = true;
            }
            if (panel_True_False.Visible == true)
            {
                count++;
                string[] points = Locations[count].Split(',');
                Point newPoints = new Point(int.Parse(points[0]), int.Parse(points[1]));
                panel_True_False.Location = newPoints;

                gridView4.OptionsSelection.EnableAppearanceFocusedCell = false;
                gridView4.OptionsSelection.EnableAppearanceFocusedRow = false;
                gridView4.OptionsSelection.EnableAppearanceHideSelection = false;
                gridView4.OptionsSelection.MultiSelect = true;
            }
        }
        public void GridsWeNeed(int ID)
        {
            DisableGrids();
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            int wire_ID = (int)db.tb_Product.FirstOrDefault(u => u.ID == ID).WirenameID;
            var ParamsType = db.tb_ParamName.Where(u => u.WireID == wire_ID);
            if (ParamsType != null)
            {
                foreach (var elem in ParamsType.Distinct().ToList())
                {
                    if (elem.tb_ParamTypes.ParamName == "Max")
                    {
                        panel_Max.Visible = true;
                    }
                    else if (elem.tb_ParamTypes.ParamName == "Min")
                    {
                        panel_Min.Visible = true;
                    }
                    else if (elem.tb_ParamTypes.ParamName == "Between")
                    {
                        panel_Btw.Visible = true;
                    }
                    else if (elem.tb_ParamTypes.ParamName == "True False")
                    {
                        panel_True_False.Visible = true;
                    }
                    else if (elem.tb_ParamTypes.ParamName == "List")
                    {
                        panel_List.Visible = true;
                    }
                    else if (elem.tb_ParamTypes.ParamName == "Same Value")
                    {
                        panel_Same_Value.Visible = true;
                    }
                }
            }
            else
            {
                MessageBox.Show("No Parameters Has Been Set For This Wire Name!");
            }
            LocatePanel();
            BindIntoGrids(wire_ID);
        }
        public void DisableGrids()
        {
            panel_Max.Visible = false;
            panel_Min.Visible = false;
            panel_Btw.Visible = false;
            panel_True_False.Visible = false;
            panel_List.Visible = false;
            panel_Same_Value.Visible = false;
        }
        public void BindProduct()
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var obj = from u in db.tb_Product
                      where u.Status == "Active"
                      orderby u.ID descending
                      select new
                      {
                          u.ID,
                          Product_Full_Name = u.tb_Wirename.Wirename + " (Size: " + u.tb_Size.Size + ", Degree:" + u.tb_Degree.Type + ")",
                      };
            comboBox1.DisplayMember = "Product_Full_Name";
            comboBox1.ValueMember = "ID";
            comboBox1.DataSource = obj.ToList();
        }

        DataTable Dt_Max;
        DataTable Dt_Min;
        DataTable Dt_btw;
        DataTable Dt_List;
        DataTable Dt_SameValue;
        DataTable Dt_TrueFalse;
        public void BindIntoGrids(int ID)
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
           
            if (panel_Max.Visible == true)
            {
                var obj1 = from u in db.tb_ParamName
                           where u.tb_ParamTypes.ParamName == "Max" && u.WireID == ID && u.Formula == null
                           select new
                           {
                               u.ID,
                               u.WireID,
                               u.Name,
                           };
                Dt_Max = new DataTable();
                Dt_Max.Columns.Add("Param_Max_Name");
                Dt_Max.Columns.Add("Param_Max_Value");
                Dt_Max.Columns.Add("Tolerance");
                Dt_Max.Columns.Add("Result");
                foreach (var elem in obj1.ToList())
                {
                    Dt_Max.Rows.Add(new object[]{
                        elem.Name,"","",""
                    });
                }
                grdMax.DataSource = Dt_Max;
            }
            if (panel_Min.Visible == true)
            {
                var obj2 = from u in db.tb_ParamName
                           where u.tb_ParamTypes.ParamName == "Min" && u.WireID == ID && u.Formula == null
                           select new
                           {
                               u.ID,
                               u.WireID,
                               u.Name
                           };
                Dt_Min = new DataTable();
                Dt_Min.Columns.Add("Param_Min_Name");
                Dt_Min.Columns.Add("Param_Min_Value");
                Dt_Min.Columns.Add("Tolerance");
                Dt_Min.Columns.Add("Result");

                foreach (var elem in obj2.ToList())
                {
                    Dt_Min.Rows.Add(new object[]{
                        elem.Name,"",""
                    });
                }
                grdMin.DataSource = Dt_Min;
            }
            if (panel_Btw.Visible == true)
            {
                var obj3 = from u in db.tb_ParamName
                           where u.tb_ParamTypes.ParamName == "Between" && u.WireID == ID && u.Formula == null
                           select new
                           {
                               u.ID,
                               u.WireID,
                               u.Name
                           };
                Dt_btw = new DataTable();
                Dt_btw.Columns.Add("Param_Btw_Name");
                Dt_btw.Columns.Add("Param_Btw_Value_To");
                Dt_btw.Columns.Add("Tolerance");
                Dt_btw.Columns.Add("Result");
                foreach (var elem in obj3.ToList())
                {
                    Dt_btw.Rows.Add(new object[]{
                        elem.Name,"",""
                    });
                }
                grdBtw.DataSource = Dt_btw;
            }
            if (panel_True_False.Visible == true)
            {
                var obj4 = from u in db.tb_ParamName
                           where u.tb_ParamTypes.ParamName == "True False" && u.WireID == ID && u.Formula == null
                           select new
                           {
                               u.ID,
                               u.WireID,
                               u.Name,
                               Result = db.tb_ParamValue.FirstOrDefault(x => x.ParamNameID == u.ID).Val1 == "true" ? "Fail" : "Pass",
                           };
                grdTrueFalse.DataSource = obj4.ToList();
                Dt_TrueFalse = new DataTable();
                Dt_TrueFalse.Columns.Add("Param_TrueFalse_Name");
                Dt_TrueFalse.Columns.Add("Param_TrueFalse_Value");
                Dt_TrueFalse.Columns.Add("Result");
                foreach (var elem in obj4.ToList())
                {
                    Dt_TrueFalse.Rows.Add(new object[]{
                        elem.Name,"",elem.Result
                    });
                }
                grdTrueFalse.DataSource = Dt_TrueFalse;
            }
            if (panel_List.Visible == true)
            {
                var obj5 = from u in db.tb_ParamName
                           where u.tb_ParamTypes.ParamName == "List" && u.WireID == ID && u.Formula == null
                           select new
                           {
                               u.ID,
                               u.WireID,
                               u.Name
                           };
                grdList.DataSource = obj5.ToList();
                Dt_List = new DataTable();
                Dt_List.Columns.Add("Param_List_Name");
                Dt_List.Columns.Add("Param_List_Value");
                Dt_List.Columns.Add("ID").ColumnMapping = MappingType.Hidden;
                foreach (var elem in obj5.ToList())
                {
                    Dt_List.Rows.Add(new object[]{
                        elem.Name,"",elem.ID
                    });
                }
                grdList.DataSource = Dt_List;
            }
            if (panel_Same_Value.Visible == true)
            {
                var obj6 = from u in db.tb_ParamName
                           where u.tb_ParamTypes.ParamName == "Same Value" && u.WireID == ID && u.Formula == null
                           select new
                           {
                               u.ID,
                               u.WireID,
                               u.Name
                           };
                grdSameVal.DataSource = obj6.ToList();
                Dt_SameValue = new DataTable();
                Dt_SameValue.Columns.Add("Param_SameVal_Name");
                Dt_SameValue.Columns.Add("Param_SameVal_Value");
                Dt_SameValue.Columns.Add("Result");
                Dt_SameValue.Columns.Add("Tolerance");
                foreach (var elem in obj6.ToList())
                {
                    Dt_SameValue.Rows.Add(new object[]{
                        elem.Name,"",""
                    });
                }
                grdSameVal.DataSource = Dt_SameValue;
            }
        }
  
        public decimal Calculate(decimal val1, string operation, decimal val2)
        {
            decimal res = 0;
            if (operation == "+")
            {
                res = val1 + val2;
            }
            else if (operation == "-")
            {
                res = val1 - val2;
            }
            else if (operation == "*")
            {
                res = val1 * val2;
            }
            else if (operation == "/") {
                res = val1 / val2;
            }
            return res;
        }
        public void Submit()
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            int ProductID = Convert.ToInt32(comboBox1.SelectedValue);
            
            tb_Reports obj = new tb_Reports();
            obj.ReportName = txtReportName.Text;
            obj.Status = "Active";
            obj.Enable = 1;
            obj.Cover = textBox2.Text;
            obj.Customer = textBox4.Text;
            obj.OtherSpec = textBox7.Text;
            obj.InvNo = textBox8.Text;
            obj.InvDate = Convert.ToDateTime(dateTimePicker2.Text);
            obj.CreatedDate = Convert.ToDateTime(dateTimePicker1.Text);
            obj.Qty = textBox10.Text;
            obj.ProductID = ProductID;
            foreach (DataRow elem in Dt_OtherValues.Rows)
            {
                obj.Winding = elem["Winding"].ToString();
                obj.Surface = elem["Surface"].ToString();
                obj.Edge = elem["Edge"].ToString();
                obj.Clean = elem["Clean"].ToString();
                obj.Polythene = elem["Polythene"].ToString();
                obj.Box = elem["Box"].ToString();
                obj.Wrapper = elem["Wrapper"].ToString();
                obj.Trace = elem["Trace"].ToString();
                obj.Weight = elem["Weight"].ToString();
                obj.Size = elem["Size"].ToString();
            }
            Report_ID = obj.ID;
            tb_ValuesOfReports objParamVal = new tb_ValuesOfReports();
            if (panel_Max.Visible == true)
            {
                decimal res_Max = 1;
                foreach (DataRow elem in Dt_Max.Rows)
                {
                    string ParamName = (string)elem["Param_Max_Name"];
                    objParamVal.ParamNameID = db.tb_ParamName.FirstOrDefault(u => u.Name == ParamName).ID;
                    //if (db.tb_ParamName.FirstOrDefault(u => u.Name == ParamName).Formula == null)
                    //{
                    objParamVal.ProductID = ProductID;
                    objParamVal.Val1 = (string)elem["Param_Max_Value"];
                    objParamVal.Tolerance = Convert.ToString(elem["Tolerance"]);
                    objParamVal.Status = "Active";
                    if (elem["Result"].ToString() == "Pass")
                    {
                        IsPassedOrNot = "Passed";
                    }
                    else
                    {
                        IsPassedOrNot = "Fail";
                    }
                    obj.tb_ValuesOfReports.Add(objParamVal);
                    //}
                    //else
                    //{
                    //    string Formula = db.tb_ParamName.FirstOrDefault(u => u.Name == ParamName).Formula;
                    //    string[] parts = Formula.Split(',');
                    //    string Operation;
                    //    tb_ParamValue obj_1 = null;
                    //    foreach (var elemParts in parts)
                    //    {
                    //        string[] ID_Num = elemParts.Split(',');
                    //        if (ID_Num[0] == "ID")
                    //        {
                    //            int ID = Convert.ToInt32(ID_Num[1]);
                    //            obj_1 = db.tb_ParamValue.FirstOrDefault(u => u.ID == ID);
                    //        }
                    //        else if (ID_Num[0] != "ID")
                    //        {
                    //            Operation = ID_Num[0];
                    //            if (Operation == "+")
                    //            {
                    //                res_Max += Convert.ToDecimal(obj_1.Val1) - 1;
                    //            }
                    //            else if (Operation == "-")
                    //            {
                    //                res_Max -= Convert.ToDecimal(obj_1.Val1) + 1;
                    //            }
                    //            else if (Operation == "*")
                    //            {
                    //                res_Max *= Convert.ToDecimal(obj_1.Val1);
                    //            }
                    //            else if (Operation == "/")
                    //            {
                    //                res_Max = Convert.ToDecimal(obj_1.Val1) / res_Max;
                    //            }
                    //        }
                    //    }
                    //}
                }
            }
            if (panel_Min.Visible == true)
            {
                foreach (DataRow elem in Dt_Min.Rows)
                {
                    tb_ValuesOfReports objParamVal_Min = new tb_ValuesOfReports();
                    string ParamName = (string)elem["Param_Min_Name"];
                    objParamVal_Min.ParamNameID = db.tb_ParamName.FirstOrDefault(u => u.Name == ParamName).ID;
                    objParamVal_Min.ProductID = ProductID;
                    objParamVal_Min.Val1 = (string)elem["Param_Min_Value"];
                    objParamVal_Min.Tolerance = Convert.ToString(elem["Tolerance"]);
                    objParamVal_Min.Status = "Active";
                    if (elem["Result"].ToString() == "Pass")
                    {
                        IsPassedOrNot = "Passed";
                    }
                    else
                    {
                        IsPassedOrNot = "Fail";
                    }
                    obj.tb_ValuesOfReports.Add(objParamVal_Min);
                }
            }
            if (panel_Btw.Visible == true)
            {
                foreach (DataRow elem in Dt_btw.Rows)
                {
                    tb_ValuesOfReports objParamVal_btw = new tb_ValuesOfReports();
                    string ParamName = Convert.ToString(elem["Param_Btw_Name"]);
                    objParamVal_btw.ParamNameID = db.tb_ParamName.FirstOrDefault(u => u.Name == ParamName).ID;
                    objParamVal_btw.ProductID = ProductID;
                    objParamVal_btw.Val1 = Convert.ToString(elem["Param_Btw_Value_To"]);
                    objParamVal_btw.Tolerance = Convert.ToString(elem["Tolerance"]);
                    objParamVal_btw.Status = "Active";
                    if (elem["Result"].ToString() == "Pass")
                    {
                        IsPassedOrNot = "Passed";
                    }
                    else
                    {
                        IsPassedOrNot = "Fail";
                    }
                    obj.tb_ValuesOfReports.Add(objParamVal_btw);
                }
            }
            if (panel_True_False.Visible == true)
            {
                int Rows = gridView4.RowCount;
                for (int i = 0; i < Rows; i++)
                {
                    tb_ValuesOfReports objParamVal_TF = new tb_ValuesOfReports();
                    if (gridView4.IsRowSelected(i))
                    {
                        objParamVal_TF.Val1 = "true";
                        string name = Convert.ToString(Dt_TrueFalse.Rows[i][0]);
                        objParamVal_TF.ParamNameID = db.tb_ParamName.FirstOrDefault(u => u.Name == name).ID;
                        objParamVal_TF.ProductID = ProductID;
                        objParamVal_TF.Status = "Active";
                        IsPassedOrNot = "Passed";
                    }
                    else
                    {
                        objParamVal_TF.Val1 = "false";
                        string name = Convert.ToString(Dt_TrueFalse.Rows[i][0]);
                        objParamVal_TF.ParamNameID = db.tb_ParamName.FirstOrDefault(u => u.Name == name).ID;
                        objParamVal_TF.ProductID = ProductID;
                        objParamVal_TF.Status = "Active";
                        IsPassedOrNot = "Fail";
                    }
                    obj.tb_ValuesOfReports.Add(objParamVal_TF);
                }
            }
            if (panel_List.Visible == true)
            {
                foreach (DataRow elem in Dt_List.Rows)
                {
                    string ParamName = (string)elem["Param_List_Name"];
                    objParamVal.ParamNameID = db.tb_ParamName.FirstOrDefault(u => u.Name == ParamName).ID;
                    objParamVal.ProductID = ProductID;
                    objParamVal.Val1 = Convert.ToString(elem["Param_List_Value"]);
                    objParamVal.Status = "Active";
                   
                    obj.tb_ValuesOfReports.Add(objParamVal);

                    foreach (DataRow el in List_Values.Rows)
                    {
                        if (Convert.ToString(el["Self_ID"]) != "")
                        {
                            int self_Id = Convert.ToInt32(el["Self_ID"]);
                            var objj = db.tb_ValuesOfReport_Lists.FirstOrDefault(u => u.ID == self_Id);
                            objj.Val1 = Convert.ToString(el["Val"]);
                            objj.Tolerance = Convert.ToString(el["Tolerance"]);

                    
                        }
                        else
                        {
                            tb_ValuesOfReport_Lists objParamList = new tb_ValuesOfReport_Lists();
                            objParamList.Val1 = Convert.ToString(el["Val"]);
                            //int Param_List_ID = db.tb_ParamList.FirstOrDefault(u => u.ParamNameID == Paramter_Name_ID).ID;
                            objParamList.ParamListID = Convert.ToInt32(el["Self_ID_From_ParamList"]);
                            objParamList.CreatedDate = DateTime.Today;
                            objParamList.Tolerance = Convert.ToString(el["Tolerance"]);
                            objParamList.Rand_Numb = 0;
                            objParamList.Status = "Active";
                            obj.tb_ValuesOfReport_Lists.Add(objParamList);
                            objParamVal.tb_ValuesOfReport_Lists.Add(objParamList);
                            if (el["Result"].ToString() == "Pass")
                            {
                                IsPassedOrNot = "Passed";
                            }
                            else
                            {
                                IsPassedOrNot = "Fail";
                            }
                        }
                    }
                }
            }
            if (panel_Same_Value.Visible == true)
            {
                foreach (DataRow elem in Dt_SameValue.Rows)
                {
                    tb_ValuesOfReports objParamVal_Same_Val = new tb_ValuesOfReports();
                    string ParamName = (string)elem["Param_SameVal_Name"];
                    objParamVal_Same_Val.ParamNameID = db.tb_ParamName.FirstOrDefault(u => u.Name == ParamName).ID;
                    objParamVal_Same_Val.ProductID = ProductID;
                    objParamVal_Same_Val.Val1 = Convert.ToString(elem["Param_SameVal_Value"]);
                    objParamVal_Same_Val.Tolerance = Convert.ToString(elem["Tolerance"]);
                    objParamVal_Same_Val.Status = "Active";
                    if (elem["Result"].ToString() == "Pass")
                    {
                        IsPassedOrNot = "Passed";
                    }
                    else
                    {
                        IsPassedOrNot = "Fail";
                    }
                    obj.tb_ValuesOfReports.Add(objParamVal_Same_Val);
                }
            }
            //Updating Report ID in Values Of Report List Table using the refrence of random number...
            //var obj_valuesOfReport = db.tb_ValuesOfReport_Lists.Where(u => u.Rand_Numb == Rand_Numb);
            //foreach (var elem in obj_valuesOfReport.ToList())
            //{
            //    elem.ReportID = Report_ID;
            //}
            obj.Result = IsPassedOrNot;
            db.tb_Reports.Add(obj);
            db.SaveChanges();
        }
        int Report_ID_Update = 0;
        public void SettingValuesToUpdate(int ID)
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var obj_Data = db.tb_ValuesOfReports.FirstOrDefault(u => u.tb_Reports.ID == ID);
            txtReportName.Text = obj_Data.tb_Reports.ReportName;
            comboBox1.SelectedValue = Convert.ToInt32(obj_Data.ProductID);
            Report_ID_Update = ID;
            //Enabling Grids we need...
            int ProductID = Convert.ToInt32(db.tb_ValuesOfReports.FirstOrDefault(u => u.ReportID == ID).ProductID);

            var ParamType = db.tb_ValuesOfReports.Where(u=>u.ProductID == ProductID);
            if (ParamType != null)
            {
                foreach (var elem in ParamType.Distinct().ToList())
                {
                    if (elem.tb_ParamName.tb_ParamTypes.ParamName == "Max")
                    {
                        panel_Max.Visible = true;
                    }
                    else if (elem.tb_ParamName.tb_ParamTypes.ParamName == "Min")
                    {
                        panel_Min.Visible = true;
                    }
                    else if (elem.tb_ParamName.tb_ParamTypes.ParamName == "Between")
                    {
                        panel_Btw.Visible = true;
                    }
                    else if (elem.tb_ParamName.tb_ParamTypes.ParamName == "True False")
                    {
                        panel_True_False.Visible = true;
                    }
                    else if (elem.tb_ParamName.tb_ParamTypes.ParamName == "List")
                    {
                        panel_List.Visible = true;
                    }
                    else if (elem.tb_ParamName.tb_ParamTypes.ParamName == "Same Value")
                    {
                        panel_Same_Value.Visible = true;
                    }
                }
            }
            LocatePanel();
            
            //Binding Data Tables...
            var obj_Max_Values = from u in db.tb_ValuesOfReports
                                 where u.ReportID == ID && u.tb_ParamName.tb_ParamTypes.ParamName == "Max" && u.tb_ParamName.Status == "Active"
                                 select new
                                 {
                                     u.ID,
                                     u.tb_ParamName.Name,
                                     u.Val1,
                                     u.Tolerance
                                 };
            Dt_Max = new DataTable();
            Dt_Max.Columns.Add("Param_Max_Name");
            Dt_Max.Columns.Add("Param_Max_Value");
            Dt_Max.Columns.Add("ID");
            Dt_Max.Columns.Add("Tolerance");
            foreach (var elem in obj_Max_Values.ToList())
            {
                Dt_Max.Rows.Add(elem.Name, elem.Val1,elem.ID,elem.Tolerance);
            }
            grdMax.DataSource = Dt_Max;

            var obj_Min_Values = from u in db.tb_ValuesOfReports
                                 where u.ReportID == ID && u.tb_ParamName.tb_ParamTypes.ParamName == "Min" && u.tb_ParamName.Status == "Active"
                                 select new
                                 {
                                     u.ID,
                                     u.tb_ParamName.Name,
                                     u.Val1,
                                     u.Tolerance
                                 };
            Dt_Min = new DataTable();
            Dt_Min.Columns.Add("Param_Min_Name");
            Dt_Min.Columns.Add("Param_Min_Value");
            Dt_Min.Columns.Add("ID");
            Dt_Min.Columns.Add("Tolerance");
            foreach (var elem in obj_Min_Values.ToList())
            {
                Dt_Min.Rows.Add(elem.Name, elem.Val1,elem.ID,elem.Tolerance);
            }
            grdMin.DataSource = Dt_Min;

            var obj_btw_Values = from u in db.tb_ValuesOfReports
                                 where u.ReportID == ID && u.tb_ParamName.tb_ParamTypes.ParamName == "Between" && u.tb_ParamName.Status == "Active"
                                 select new
                                 {
                                     u.ID,
                                     u.tb_ParamName.Name,
                                     u.Val1,
                                     u.Tolerance
                                 };
            Dt_btw = new DataTable();
            Dt_btw.Columns.Add("Param_Btw_Name");
            Dt_btw.Columns.Add("Param_Btw_Value_To");
            Dt_btw.Columns.Add("ID");
            Dt_btw.Columns.Add("Tolerance");
            foreach (var elem in obj_btw_Values.ToList())
            {
                Dt_btw.Rows.Add(elem.Name, elem.Val1, elem.ID,elem.Tolerance);
            }
            grdBtw.DataSource = Dt_btw;

            var obj_List_Values = from u in db.tb_ValuesOfReports
                                 where u.ReportID == ID && u.tb_ParamName.tb_ParamTypes.ParamName == "List" && u.tb_ParamName.Status == "Active"
                                 select new
                                 {
                                     u.ID,
                                     u.tb_ParamName.Name,
                                     u.Val1
                                 };
            Dt_List = new DataTable();
            Dt_List.Columns.Add("Param_List_Name");
            Dt_List.Columns.Add("Param_List_Value");
            Dt_List.Columns.Add("ID");
            foreach (var elem in obj_List_Values.ToList())
            {
                Dt_List.Rows.Add(elem.Name, elem.Val1, elem.ID);
            }
            grdList.DataSource = Dt_List;

            var obj_SameValue_Values = from u in db.tb_ValuesOfReports
                                       where u.ReportID == ID && u.tb_ParamName.tb_ParamTypes.ParamName == "Same Value" && u.tb_ParamName.Status == "Active"
                                       select new
                                       {
                                           u.ID,
                                           u.tb_ParamName.Name,
                                           u.Val1
                                       };
            Dt_SameValue = new DataTable();
            Dt_SameValue.Columns.Add("Param_SameVal_Name");
            Dt_SameValue.Columns.Add("Param_SameVal_Value");
            Dt_SameValue.Columns.Add("ID");
            foreach (var elem in obj_SameValue_Values.ToList())
            {
                Dt_SameValue.Rows.Add(elem.Name, elem.Val1, elem.ID);
            }
            grdSameVal.DataSource = Dt_SameValue;

            var obj_TrueFalse_Values = from u in db.tb_ValuesOfReports
                                       where u.ReportID == ID && u.tb_ParamName.tb_ParamTypes.ParamName == "True False" && u.tb_ParamName.Status == "Active"
                                       select new
                                       {
                                           u.ID,
                                           u.tb_ParamName.Name,
                                           u.Val1
                                       };
            Dt_TrueFalse = new DataTable();
            Dt_TrueFalse.Columns.Add("Param_TrueFalse_Name");
            Dt_TrueFalse.Columns.Add("Param_TrueFalse_Value");
            Dt_TrueFalse.Columns.Add("ID");
            foreach (var elem in obj_TrueFalse_Values.ToList())
            {
                Dt_TrueFalse.Rows.Add(elem.Name, elem.Val1, elem.ID);
            }
            grdTrueFalse.DataSource = Dt_TrueFalse;
        }
        public void data_Update()
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            if (panel_Max.Visible == true)
            {
                foreach (DataRow elem in Dt_Max.Rows)
                {
                        int ID = Convert.ToInt32(elem["ID"]);
                        var objMax_update = db.tb_ValuesOfReports.FirstOrDefault(u=>u.ID == ID);
                        objMax_update.Val1 = (string)elem["Param_Max_Value"];
                        objMax_update.Val1 = Convert.ToString(elem["Tolerance"]);
                }
            }
            if (panel_Min.Visible == true)
            {
                foreach (DataRow elem in Dt_Min.Rows)
                {
                        int ID = Convert.ToInt32(elem["ID"]);
                        var objMax_update = db.tb_ValuesOfReports.FirstOrDefault(u => u.ID == ID);
                        objMax_update.Val1 = (string)elem["Param_Min_Value"];
                        objMax_update.Tolerance = Convert.ToString(elem["Tolerance"]);
                }
            }
            if (panel_Btw.Visible == true)
            {
                foreach (DataRow elem in Dt_btw.Rows)
                {
                        int ID = Convert.ToInt32(elem["ID"]);
                        var objMax_update = db.tb_ValuesOfReports.FirstOrDefault(u => u.ID == ID);
                        objMax_update.Val1 = (string)elem["Param_Btw_Value_To"];
                        objMax_update.Tolerance = Convert.ToString(elem["Tolerance"]);
                }
            }
            int Row_Count = 0;
            if (panel_True_False.Visible == true)
            {
                foreach(DataRow elem in Dt_TrueFalse.Rows)
                {
                    int param_ID = Convert.ToInt32(elem["ID"]);
                    var obj = db.tb_ValuesOfReports.FirstOrDefault(u => u.ID == param_ID);
                    obj.Val1 = gridView4.IsRowSelected(Row_Count) == true? "true":"false";
                    Row_Count++;
                }
            }
            if (panel_List.Visible == true)
            {
                foreach (DataRow elem in List_Values.Rows)
                {
                    int ID = Convert.ToInt32(elem["Self_ID"]);
                    var obj = db.tb_ValuesOfReport_Lists.FirstOrDefault(u => u.ID == ID);
                    obj.Val1 = Convert.ToString(elem["Val"]);
                }
            }
            if (panel_Same_Value.Visible == true)
            {
                foreach (DataRow elem in Dt_SameValue.Rows)
                {
                    int param_ID = Convert.ToInt32(elem["ID"]);
                    var objParamVal_Same_ValUpdate = db.tb_ValuesOfReports.FirstOrDefault(u => u.ID == param_ID);
                    objParamVal_Same_ValUpdate.Val1 = Convert.ToString(elem["Param_SameVal_Value"]);
                    objParamVal_Same_ValUpdate.Tolerance = Convert.ToString(elem["Tolerance"]);
                }
            }
            db.SaveChanges();
        }
        private void button1_Click_1(object sender, EventArgs e)//Button Ok..
        {
            int ID = Convert.ToInt32(comboBox1.SelectedValue);
            GridsWeNeed(ID);
        }
        private void button2_Click_3(object sender, EventArgs e)//Submit Button Click...
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();

            if (isUpdate == 0)
            {
                int ID = Convert.ToInt32(comboBox1.SelectedValue);
                Submit();
                MessageBox.Show("Reocord Added Successfully!");
            }
            else
            {
                data_Update();
                MessageBox.Show("Reocord Updated Successfully!");
            }
            
        }
        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int ID = 0;
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            if (isUpdate == 0)
            {
                ID = Convert.ToInt32(gridView5.GetFocusedRowCellValue("ID"));
            }
            else {
                ID = Convert.ToInt32(gridView5.GetFocusedRowCellValue("ID"));
              }
                int P_ID = Convert.ToInt32(ID);
                int Product_ID = Convert.ToInt32(comboBox1.SelectedValue);
                Create_Report_List_Value CRLV = new Create_Report_List_Value(P_ID, Product_ID, Rand_Numb,Report_ID_Update);
                CRLV.ShowDialog();
        }

        private void gridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName == "Param_Max_Value")
            {
                double price = 0;
                if (!Double.TryParse(e.Value as String, out price))
                {
                    e.Valid = false;
                    e.ErrorText = "Only numeric values are accepted!";
                }
            }
        }

        private void gridView2_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName == "Param_Min_Value")
            {
                double price = 0;
                if (!Double.TryParse(e.Value as String, out price))
                {
                    e.Valid = false;
                    e.ErrorText = "Only numeric values are accepted!";
                }
            }
        }

        private void gridView3_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName == "Param_Btw_Value_To")
            {
                double price = 0;
                if (!Double.TryParse(e.Value as String, out price))
                {
                    e.Valid = false;
                    e.ErrorText = "Only numeric values are accepted!";
                }
            }
        }

        private void gridView5_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName == "Param_List_Value")
            {
                double price = 0;
                if (!Double.TryParse(e.Value as String, out price))
                {
                    e.Valid = false;
                    e.ErrorText = "Only numeric values are accepted!";
                }
            }
        }

        private void gridView6_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName == "Param_SameVal_Value")
            {
                double price = 0;
                if (!Double.TryParse(e.Value as String, out price))
                {
                    e.Valid = false;
                    e.ErrorText = "Only numeric values are accepted!";
                }
            }
        }

        private void grdMin_Click_1(object sender, EventArgs e)
        {

        }
        private void grdSameVal_Click(object sender, EventArgs e)
        {

        }
        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            GridView grd = (GridView)sender;
            decimal val = Convert.ToDecimal(grd.EditingValue);

            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            string ParamName = (string)gridView1.GetFocusedRowCellValue("Param_Max_Name");
            int ProductID = (int)comboBox1.SelectedValue;
            var obj = db.tb_ParamValue.FirstOrDefault(u => u.ProductID == ProductID && u.tb_ParamName.Name == ParamName);
                //MessageBox.Show("Pass");
                foreach (DataRow elem in Dt_Max.Rows)
                {
                    if (val <= Convert.ToDecimal(obj.Val1))
                    {
                        elem["Result"] = "Pass";
                    }
                    else
                    {
                        elem["Result"] = "Fail";
                    }
                }
           
        }
        private void grdMin_KeyDown(object sender, KeyEventArgs e)
        {
            GridView grd = (GridView)sender;
            decimal val = Convert.ToDecimal(grd.EditingValue);

            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            string ParamName = (string)gridView1.GetFocusedRowCellValue("Param_Min_Name");
            int ProductID = (int)comboBox1.SelectedValue;
            var obj = db.tb_ParamValue.FirstOrDefault(u => u.ProductID == ProductID && u.tb_ParamName.Name == ParamName);
            //MessageBox.Show("Pass");
            foreach (DataRow elem in Dt_Min.Rows)
            {
                if (val >= Convert.ToDecimal(obj.Val1))
                {
                    elem["Result"] = "Pass";
                }
                else
                {
                    elem["Result"] = "Fail";
                }
            }
        }
        private void grdBtw_KeyDown(object sender, KeyEventArgs e)
        {
            GridView grd = (GridView)sender;
            decimal val = Convert.ToDecimal(grd.EditingValue);

            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            string ParamName = (string)gridView1.GetFocusedRowCellValue("Param_Btw_Name");
            int ProductID = (int)comboBox1.SelectedValue;
            var obj = db.tb_ParamValue.FirstOrDefault(u => u.ProductID == ProductID && u.tb_ParamName.Name == ParamName);
            //MessageBox.Show("Pass");
            foreach (DataRow elem in Dt_btw.Rows)
            {
                if (val <= Convert.ToDecimal(obj.Val1) && val >=Convert.ToDecimal(obj.Val2))
                {
                    elem["Result"] = "Pass";
                }
                else
                {
                    elem["Result"] = "Fail";
                }
            }
        }
        private void grdList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                (grdList.FocusedView as ColumnView).FocusedRowHandle++;
                e.Handled = true;
            }
        }
        private void grdSameVal_KeyDown(object sender, KeyEventArgs e)
        {
            GridView grd = (GridView)sender;
            decimal val = Convert.ToDecimal(grd.EditingValue);

            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            string ParamName = (string)gridView1.GetFocusedRowCellValue("Param_SameVal_Value");
            int ProductID = (int)comboBox1.SelectedValue;
            var obj = db.tb_ParamValue.FirstOrDefault(u => u.ProductID == ProductID && u.tb_ParamName.Name == ParamName);
            foreach (DataRow elem in Dt_SameValue.Rows)
            {
                if (val == Convert.ToDecimal(obj.Val1))
                {
                    elem["Result"] = "Pass";
                }
                else
                {
                    elem["Result"] = "Fail";
                }
            }
        }
        private void grdBtw_Click(object sender, EventArgs e)
        {

        }
        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void gridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }
        private void gridView4_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void gridView4_KeyDown(object sender, KeyEventArgs e)
        {
          
        }
        private void gridView4_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {   
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            GridView grd = (GridView)sender;
            var a = grd.GetDataRow(e.ControllerRow);
            int ProductID = (int)comboBox1.SelectedValue;
            string ParamName = a["Param_TrueFalse_Name"].ToString();
            var obj = db.tb_ParamValue.FirstOrDefault(u => u.ProductID == ProductID && u.tb_ParamName.Name == ParamName);
          
                if (gridView4.IsRowSelected(e.ControllerRow))
                {
                    if (obj.Val1 == "true")
                    {
                        gridView4.SetRowCellValue(e.ControllerRow,"Result","Pass");
                    }
                    else
                    {
                        gridView4.SetRowCellValue(e.ControllerRow, "Result", "Fail");
                    }
                }
                else
                {
                    if (obj.Val1 == "true")
                    {
                        gridView4.SetRowCellValue(e.ControllerRow, "Result", "Fail");
                    }
                    else
                    {
                        gridView4.SetRowCellValue(e.ControllerRow, "Result", "Pass");
                    }
                }
            }
        private void button4_Click(object sender, EventArgs e)
        {
            
            if (button4.BackColor == Color.FromArgb(0x2E, 0xCC, 0x71))
            {
                button4.BackColor = Color.Gainsboro;
                button4.ForeColor = Color.Black;
                foreach (DataRow Col in Dt_OtherValues.Rows)
                {
                    Col["Winding"] = "";
                }
            }
            else
            {
                button4.BackColor = Color.FromArgb(0x2E, 0xCC, 0x71);
                button4.ForeColor = Color.White;
                Dt_OtherValues.Rows[0]["Winding"] = "OK";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            if (button3.BackColor == Color.FromArgb(0x2E, 0xCC, 0x71))
            {
                button3.BackColor = Color.Gainsboro;
                button3.ForeColor = Color.Black;
                foreach (DataRow Col in Dt_OtherValues.Rows)
                {
                    Col["Surface"] = "";
                }
            }
            else
            {
                button3.BackColor = Color.FromArgb(0x2E, 0xCC, 0x71);
                button3.ForeColor = Color.White;
                Dt_OtherValues.Rows[0]["Surface"] = "OK";
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            
            if (button13.BackColor == Color.FromArgb(0x2E, 0xCC, 0x71))
            {
                button13.BackColor = Color.Gainsboro;
                button13.ForeColor = Color.Black;
                foreach (DataRow Col in Dt_OtherValues.Rows)
                {
                    Col["Edge"] = "";
                }
            }
            else
            {
                button13.BackColor = Color.FromArgb(0x2E, 0xCC, 0x71);
                button13.ForeColor = Color.White;
                Dt_OtherValues.Rows[0]["Edge"] = "OK";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            if (button5.BackColor == Color.FromArgb(0x2E, 0xCC, 0x71))
            {
                button5.BackColor = Color.Gainsboro;
                button5.ForeColor = Color.Black;
                foreach (DataRow Col in Dt_OtherValues.Rows)
                {
                    Col["Clean"] = "";
                }
            }
            else
            {
                button5.BackColor = Color.FromArgb(0x2E, 0xCC, 0x71);
                button5.ForeColor = Color.White;
                Dt_OtherValues.Rows[0]["Clean"] = "OK";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            
            if (button8.BackColor == Color.FromArgb(0x2E, 0xCC, 0x71))
            {
                button8.BackColor = Color.Gainsboro;
                button8.ForeColor = Color.Black;
                foreach (DataRow Col in Dt_OtherValues.Rows)
                {
                    Col["Polythene"] = "";
                }
            }
            else
            {
                button8.BackColor = Color.FromArgb(0x2E, 0xCC, 0x71);
                button8.ForeColor = Color.White;
                Dt_OtherValues.Rows[0]["Polythene"] = "OK";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            if (button7.BackColor == Color.FromArgb(0x2E, 0xCC, 0x71))
            {
                button7.BackColor = Color.Gainsboro;
                button7.ForeColor = Color.Black;
                foreach (DataRow Col in Dt_OtherValues.Rows)
                {
                    Col["Box"] = "";
                }
            }
            else
            {
                button7.BackColor = Color.FromArgb(0x2E, 0xCC, 0x71);
                button7.ForeColor = Color.White;
                Dt_OtherValues.Rows[0]["Box"] = "OK";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            if (button6.BackColor == Color.FromArgb(0x2E, 0xCC, 0x71))
            {
                button6.BackColor = Color.Gainsboro;
                button6.ForeColor = Color.Black;
                foreach (DataRow Col in Dt_OtherValues.Rows)
                {
                    Col["Wrapper"] = "";
                }
            }
            else
            {
                button6.BackColor = Color.FromArgb(0x2E, 0xCC, 0x71);
                button6.ForeColor = Color.White;
                Dt_OtherValues.Rows[0]["Wrapper"] = "OK";
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            
            if (button11.BackColor == Color.FromArgb(0x2E, 0xCC, 0x71))
            {
                button11.BackColor = Color.Gainsboro;
                button11.ForeColor = Color.Black;
                foreach (DataRow Col in Dt_OtherValues.Rows)
                {
                    Col["Trace"] = "";
                }
            }
            else
            {
                button11.BackColor = Color.FromArgb(0x2E, 0xCC, 0x71);
                button11.ForeColor = Color.White;
                Dt_OtherValues.Rows[0]["Trace"] = "OK";
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            
            if (button10.BackColor == Color.FromArgb(0x2E, 0xCC, 0x71))
            {
                button10.BackColor = Color.Gainsboro;
                button10.ForeColor = Color.Black;
                foreach (DataRow Col in Dt_OtherValues.Rows)
                {
                    Col["Weight"] = "";
                }
            }
            else
            {
                button10.BackColor = Color.FromArgb(0x2E, 0xCC, 0x71);
                button10.ForeColor = Color.White;
                Dt_OtherValues.Rows[0]["Weight"] = "OK";
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            
            if (button9.BackColor == Color.FromArgb(0x2E, 0xCC, 0x71))
            {
                button9.BackColor = Color.Gainsboro;
                button9.ForeColor = Color.Black;
                foreach (DataRow Col in Dt_OtherValues.Rows)
                {
                    Col["Size"] = "";
                }
            }
            else
            {
                button9.BackColor = Color.FromArgb(0x2E, 0xCC, 0x71);
                button9.ForeColor = Color.White;
                Dt_OtherValues.Rows[0]["Size"] = "OK";
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
           
            if (button12.BackColor == Color.FromArgb(0x2E, 0xCC, 0x71))
            {
                button12.BackColor = Color.Gainsboro;
                button12.ForeColor = Color.Black;
            }
            else
            {
                button12.BackColor = Color.FromArgb(0x2E, 0xCC, 0x71);
                button12.ForeColor = Color.White;
                Dt_OtherValues.Rows[0]["Special"] = "OK";
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

    }
}
