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
using Microsoft.Reporting.WinForms;

namespace WireTestingProject
{
    public partial class Wire_Test_Report : Form
    {
        string IsPassed = "Passed";
        public Wire_Test_Report(int ID)
        {
            InitializeComponent();
            BindReport(ID);
        }

        private void Wire_Test_Report_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }
        int ValRep_ID = 0;
        public string GetUniqueNameFromParamList(int ID)
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            //Got the Major Data from Report ID becuase its common in all rows...
            var obj = db.tb_ValuesOfReport_Lists.FirstOrDefault(u => u.ReportID == ID);
            var _ID = obj.ID;
            var obj_1 = ValRep_ID == 0 ? db.tb_ValuesOfReport_Lists.FirstOrDefault(u => u.ID == _ID && u.ReportID == ID) : db.tb_ValuesOfReport_Lists.FirstOrDefault(u => u.ID > ValRep_ID && u.ReportID == ID);
            ValRep_ID = obj_1 == null ? 0 : obj_1.ID;
            string ret;
            if (obj_1 == null)
            {
                ret = "";
            }
            else
            {
                ret = obj_1.tb_ParamList.Name;
            }
            return ret;
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
        public string GetFormatList(string ListParamType, int ID)
        {
            string format = "";
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            if (ListParamType == "Max")
            {
                int ParamListID = (int)db.tb_ValuesOfReport_Lists.FirstOrDefault(u => u.ID == ID).ParamListID;
                format = Convert.ToDecimal(db.tb_ValuesOfReport_Lists.FirstOrDefault(u => u.ID == ID).Val1) <= Convert.ToDecimal(db.tb_ParamListValues.FirstOrDefault(n => n.ParamListID == ParamListID).Val) ? "Light" : "ExtraBold";
                isPassedOrNot(format);
            }
            else if (ListParamType == "Min")
            {
                int ParamListID = (int)db.tb_ValuesOfReport_Lists.FirstOrDefault(u => u.ID == ID).ParamListID;
                format = Convert.ToDecimal(db.tb_ValuesOfReport_Lists.FirstOrDefault(u => u.ID == ID).Val1) >= Convert.ToDecimal(db.tb_ParamListValues.FirstOrDefault(n => n.ParamListID == ParamListID).Val) ? "Light" : "ExtraBold";
                isPassedOrNot(format);
            }
            else if (ListParamType == "Between")
            {
                int ParamListID = (int)db.tb_ValuesOfReport_Lists.FirstOrDefault(u => u.ID == ID).ParamListID;
                format = Convert.ToDecimal(db.tb_ValuesOfReport_Lists.FirstOrDefault(u => u.ID == ID).Val1) >= Convert.ToDecimal(db.tb_ParamListValues.FirstOrDefault(n => n.ParamListID == ParamListID).Val) && Convert.ToDecimal(db.tb_ValuesOfReport_Lists.FirstOrDefault(u => u.ID == ID).Val1) <= Convert.ToDecimal(db.tb_ParamListValues.FirstOrDefault(n => n.ParamListID == ParamListID).Val2) ? "Light" : "ExtraBold";
                isPassedOrNot(format);
            }
            else if (ListParamType == "Same Value")
            {
                int ParamListID = (int)db.tb_ValuesOfReport_Lists.FirstOrDefault(u => u.ID == ID).ParamListID;
                format = Convert.ToDecimal(db.tb_ValuesOfReport_Lists.FirstOrDefault(u => u.ID == ID).Val1) == Convert.ToDecimal(db.tb_ParamListValues.FirstOrDefault(n => n.ParamListID == ParamListID).Val) ? "Light" : "ExtraBold";
                isPassedOrNot(format);
            }
            else if (ListParamType == "Same Value")
            {
                format = db.tb_ValuesOfReport_Lists.FirstOrDefault(u => u.ID == ID).Val1 == "true" ? "Light" : "ExtraBold";
                isPassedOrNot(format);
            }
            return format;
        }
        public void isPassedOrNot(string name)
        {
            if (name == "Light")
            {
                IsPassed = "Passed";
            }
            else
            {
                IsPassed = "Fail";
            }
        }
        public string GetFormat(string ListParamType, int ID)
        {
            string format = "";
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            if (ListParamType == "Max")
            {
                int ParamNameID = (int)db.tb_ValuesOfReports.FirstOrDefault(u => u.ID == ID).ParamNameID;
                format = Convert.ToDecimal(db.tb_ValuesOfReports.FirstOrDefault(u => u.ID == ID).Val1) <= Convert.ToDecimal(db.tb_ParamValue.FirstOrDefault(n => n.ParamNameID == ParamNameID).Val1) ? "Light" : "ExtraBold";
            }
            else if (ListParamType == "Min")
            {
                int ParamNameID = (int)db.tb_ValuesOfReports.FirstOrDefault(u => u.ID == ID).ParamNameID;
                format = Convert.ToDecimal(db.tb_ValuesOfReports.FirstOrDefault(u => u.ID == ID).Val1) >= Convert.ToDecimal(db.tb_ParamValue.FirstOrDefault(n => n.ParamNameID == ParamNameID).Val1) ? "Light" : "ExtraBold";
            }
            else if (ListParamType == "Between")
            {
                int ParamNameID = (int)db.tb_ValuesOfReports.FirstOrDefault(u => u.ID == ID).ParamNameID;
                format = Convert.ToDecimal(db.tb_ValuesOfReports.FirstOrDefault(u => u.ID == ID).Val1) >= Convert.ToDecimal(db.tb_ParamValue.FirstOrDefault(n => n.ParamNameID == ParamNameID).Val1) && Convert.ToDecimal(db.tb_ValuesOfReports.FirstOrDefault(u => u.ID == ID).Val1) <= Convert.ToDecimal(db.tb_ParamValue.FirstOrDefault(n => n.ParamNameID == ParamNameID).Val2) ? "Light" : "ExtraBold";
            }
            else if (ListParamType == "Same Value")
            {
                int ParamNameID = (int)db.tb_ValuesOfReports.FirstOrDefault(u => u.ID == ID).ParamNameID;
                format = Convert.ToDecimal(db.tb_ValuesOfReports.FirstOrDefault(u => u.ID == ID).Val1) == Convert.ToDecimal(db.tb_ParamValue.FirstOrDefault(n => n.ParamNameID == ParamNameID).Val2) ? "Light" : "ExtraBold";
            }
            else if (ListParamType == "True False")
            {
                format = db.tb_ValuesOfReports.FirstOrDefault(u => u.ID == ID).Val1 == "true" ? "Light" : "ExtraBold";
            }
            return format;
        }
        public void BindReport(int ID) 
        {
            ReportDataSource itemDataSource1;
            ReportDataSet ds = new ReportDataSet();
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var obj_Caliber = from u in db.tb_ValuesOfReports
                                 where u.ReportID == ID
                                 select new
                                 {
                                     u.ID,
                                     ReportID = db.tb_ValuesOfReports.FirstOrDefault(n => n.ID == u.ID).ReportID,
                                     CatagoryName = db.tb_ValuesOfReports.FirstOrDefault(n => n.ID == u.ID && n.tb_ParamName.ShowParam != 2).tb_ParamName.tb_Catagory.CatagoryName,
                                     ParamName = "."+db.tb_ValuesOfReports.FirstOrDefault(n => n.ID == u.ID && n.tb_ParamName.ShowParam != 2).tb_ParamName.Name + "   " + db.tb_ValuesOfReports.FirstOrDefault(n => n.ID == u.ID && n.tb_ParamName.ShowParam != 2).tb_ParamName.tb_Unit.Unit,
                                     ParamType = db.tb_ValuesOfReports.FirstOrDefault(n => n.ID == u.ID && n.tb_ParamName.ShowParam != 2).tb_ParamName.tb_ParamTypes.ParamName == "True False" ? "Pass/Not Pass" : db.tb_ValuesOfReports.FirstOrDefault(n => n.ID == u.ID && n.tb_ParamName.ShowParam != 2).tb_ParamName.tb_ParamTypes.ParamName,
                                     Param_Expected_Val1 = db.tb_ParamValue.FirstOrDefault(n => n.ParamNameID == u.ParamNameID && u.ProductID == db.tb_ValuesOfReports.FirstOrDefault(v => v.ID == u.ID).ProductID && u.tb_ParamName.ShowParam != 2).Val1 == "true"?"Pass":"Not Pass",
                                     Param_Expected_Val2 = db.tb_ParamValue.FirstOrDefault(n => n.ParamNameID == u.ParamNameID && u.ProductID == db.tb_ValuesOfReports.FirstOrDefault(v => v.ID == u.ID).ProductID && n.tb_ParamName.ShowParam != 2).Val2,
                                     Param_Expected_Tolerance = db.tb_ParamValue.FirstOrDefault(n => n.ParamNameID == u.ParamNameID && u.ProductID == db.tb_ValuesOfReports.FirstOrDefault(v => v.ID == u.ID).ProductID && n.tb_ParamName.ShowParam != 2).Tolerance,
                                     Param_Res_Val = db.tb_ValuesOfReports.FirstOrDefault(n => n.ID == u.ID && n.tb_ParamName.ShowParam != 2).Val1,
                                     Param_Res_Tolerance = u.Tolerance,
                                     //Param_Unit = db.tb_ValuesOfReports.FirstOrDefault(n => n.ID == ID && n.tb_ParamName.ShowParam != 2).tb_ParamName.tb_Unit.Unit,
                                 };
            string Cover = db.tb_ValuesOfReports.FirstOrDefault(u => u.ReportID == ID).tb_Reports.Cover;
            string Box = db.tb_ValuesOfReports.FirstOrDefault(u => u.ReportID == ID).tb_Reports.Box;
            string Clean = db.tb_ValuesOfReports.FirstOrDefault(u => u.ReportID == ID).tb_Reports.Clean;
            string Customer = db.tb_ValuesOfReports.FirstOrDefault(u => u.ReportID == ID).tb_Reports.Customer;
            string Edge = db.tb_ValuesOfReports.FirstOrDefault(u => u.ReportID == ID).tb_Reports.Edge;
            string InvDate = Convert.ToDateTime(db.tb_ValuesOfReports.FirstOrDefault(u => u.ReportID == ID).tb_Reports.InvDate).ToString("dd/MM/yyyy");
            string InvNo = db.tb_ValuesOfReports.FirstOrDefault(u => u.ReportID == ID).tb_Reports.InvNo;
            string OtherSpec = db.tb_ValuesOfReports.FirstOrDefault(u => u.ReportID == ID).tb_Reports.OtherSpec;
            string Polythene = db.tb_ValuesOfReports.FirstOrDefault(u => u.ReportID == ID).tb_Reports.Polythene;
            string Qty = db.tb_ValuesOfReports.FirstOrDefault(u => u.ReportID == ID).tb_Reports.Qty;
            string Size = db.tb_ValuesOfReports.FirstOrDefault(u => u.ReportID == ID).tb_Reports.Size;
            string Surface = db.tb_ValuesOfReports.FirstOrDefault(u => u.ReportID == ID).tb_Reports.Surface;
            string Trace = db.tb_ValuesOfReports.FirstOrDefault(u => u.ReportID == ID).tb_Reports.Trace;
            string Weight = db.tb_ValuesOfReports.FirstOrDefault(u => u.ReportID == ID).tb_Reports.Weight;
            string Winding = db.tb_ValuesOfReports.FirstOrDefault(u => u.ReportID == ID).tb_Reports.Winding;
            string Wrapper = db.tb_ValuesOfReports.FirstOrDefault(u => u.ReportID == ID).tb_Reports.Wrapper;

            var obj_Caliber1 = from u in obj_Caliber.ToList()
                                  select new
                                  {
                                      u.ID,
                                      u.ReportID,
                                      u.CatagoryName,
                                      u.ParamName,
                                      u.ParamType,
                                      u.Param_Expected_Val1,
                                      u.Param_Expected_Val2,
                                      u.Param_Expected_Tolerance,
                                      u.Param_Res_Val,
                                      u.Param_Res_Tolerance,
                                      Format = GetFormat(u.ParamType,u.ID),
                                  };
            var obj_Caliber1_List = from u in db.tb_ValuesOfReport_Lists
                                    where u.ReportID == ID 
                                    select new
                                    {
                                        u.tb_ParamList.tb_ParamTypes.ParamName,
                                        u.ID,
                                        List_Param_Name = u.tb_ParamList.Name + "   " + u.tb_ParamList.tb_Unit.Unit,
                                        List_Param_Name_Expect_Val = db.tb_ParamListValues.FirstOrDefault(n => n.ParamListID == u.ParamListID).Val == "true" ? "Pass" : "Not Pass",
                                        List_Param_Name_Expect_Val2 = db.tb_ParamListValues.FirstOrDefault(n => n.ParamListID == u.ParamListID).Val2,
                                        List_Param_Res_Val = u.Val1,
                                        List_Param_Type = u.tb_ParamList.tb_ParamTypes.ParamName == "True False" ? "Pass/Not Pass" : u.tb_ParamList.tb_ParamTypes.ParamName,
                                        Main_Param_Type = u.tb_ValuesOfReports.tb_ParamName.tb_ParamTypes.ParamName,
                                        Main_Param_Name = u.tb_ValuesOfReports.tb_ParamName.Name,
                                        List_Param_Tolerance = u.Tolerance,
                                        List_Param_Tolerance_Expected = db.tb_ParamListValues.FirstOrDefault(n=>n.ParamListID == u.ParamListID).Tolerance,
                                        //Convert.ToDecimal(u.Val1) <= Convert.ToDecimal(db.tb_ParamListValues.FirstOrDefault(n => n.ParamListID == u.ParamListID).Val) ? "ExtraBold" : "Light",
                                        //Param_list_Unit = u.tb_ParamList.tb_Unit.Unit,

                                    };
            var obj_Caliber1_List1 = from u in obj_Caliber1_List.ToList()
                                    select new
                                    {
                                        u.List_Param_Name,
                                        u.List_Param_Name_Expect_Val,
                                        u.List_Param_Name_Expect_Val2,
                                        u.List_Param_Res_Val,
                                        u.List_Param_Type,
                                        u.Main_Param_Type,
                                        u.Main_Param_Name,
                                        Format = GetFormatList(u.ParamName, u.ID),
                                        u.List_Param_Tolerance,
                                        u.List_Param_Tolerance_Expected,
                                        //Convert.ToDecimal(u.Val1) <= Convert.ToDecimal(db.tb_ParamListValues.FirstOrDefault(n => n.ParamListID == u.ParamListID).Val) ? "ExtraBold" : "Light",
                                        //Param_list_Unit = u.tb_ParamList.tb_Unit.Unit,
                                    };

            //Creating Datatable to append both objects in a single table....

            DataTable Dt_ReportData = new DataTable();

            Dt_ReportData.Columns.Add("ValuesOfReportID");
            Dt_ReportData.Columns.Add("ReportID");
            Dt_ReportData.Columns.Add("CatagoryName");
            Dt_ReportData.Columns.Add("ParamName");
            Dt_ReportData.Columns.Add("ParamType");
            Dt_ReportData.Columns.Add("List_ParamType");
            Dt_ReportData.Columns.Add("Param_Expected_Val1");
            Dt_ReportData.Columns.Add("Param_Expected_Val2");
            Dt_ReportData.Columns.Add("Param_Result_Val");
            Dt_ReportData.Columns.Add("Param_Tolerance_Expected_Val");
            Dt_ReportData.Columns.Add("Param_Tolerance_Result_Val");
            Dt_ReportData.Columns.Add("Bold_Format");
            Dt_ReportData.Columns.Add("Bytes");
            Dt_ReportData.Columns.Add("Cover");//New Dt...
            Dt_ReportData.Columns.Add("Customer");
            Dt_ReportData.Columns.Add("OtherSpec");
            Dt_ReportData.Columns.Add("InvNo");
            Dt_ReportData.Columns.Add("InvDate");
            Dt_ReportData.Columns.Add("Qty");
            Dt_ReportData.Columns.Add("Winding");
            Dt_ReportData.Columns.Add("Surface");
            Dt_ReportData.Columns.Add("Edge");
            Dt_ReportData.Columns.Add("Clean");
            Dt_ReportData.Columns.Add("Polythene");
            Dt_ReportData.Columns.Add("Box");
            Dt_ReportData.Columns.Add("Wrapper");
            Dt_ReportData.Columns.Add("Trace");
            Dt_ReportData.Columns.Add("Weight");
            Dt_ReportData.Columns.Add("Size");
            Dt_ReportData.Columns.Add("Special");

            byte[] bytes = null;

            foreach (var elem in obj_Caliber1.ToList())
            {
                if (elem.ParamType == "List")
                    {
                        foreach (var el in obj_Caliber1_List1.ToList())
                        {
                            Dt_ReportData.Rows.Add(elem.ID,elem.ReportID, elem.CatagoryName,el.List_Param_Name,"List",el.List_Param_Type,el.List_Param_Name_Expect_Val,el.List_Param_Name_Expect_Val2,el.List_Param_Res_Val,el.List_Param_Tolerance_Expected,el.List_Param_Tolerance,el.Format,bytes,Cover,Customer,OtherSpec,InvNo,InvDate,Qty,Winding,Surface,Edge,Clean,Polythene,Box,Wrapper,Trace,Weight,Size);
                        }
                    }
                Dt_ReportData.Rows.Add(elem.ID,elem.ReportID,elem.CatagoryName,elem.ParamName,elem.ParamType,"",elem.Param_Expected_Val1,elem.Param_Expected_Val2,elem.Param_Res_Val,elem.Param_Expected_Tolerance,elem.Param_Res_Val,elem.Format,"",Cover,Customer,OtherSpec,InvNo,InvDate,Qty,Winding,Surface,Edge,Clean,Polythene,Box,Wrapper,Trace,Weight,Size);
            }
            DataTable Dt_List = new DataTable();
            if (obj_Caliber1 != null)
            {
                Dt_List.Columns.Add("Name");
                Dt_List.Columns.Add("Type");
                Dt_List.Columns.Add("Exp_Value");
                Dt_List.Columns.Add("Res_Value");
                foreach (DataRow elem in Dt_ReportData.Rows)
                {
                    if (elem["Param_Result_Val"].ToString() == "true")
                    {
                        bytes = System.IO.File.ReadAllBytes("TickMark.png");
                    }
                    else if (elem["Param_Result_Val"].ToString() == "false")
                    {
                        bytes = System.IO.File.ReadAllBytes("CrossMark.png");
                    }
                    else
                    {
                        bytes = null;
                    }
                    ds.Caliber_Report.Rows.Add(elem["ReportID"], elem["ParamName"], elem["Param_Expected_Val1"], elem["Param_Expected_Val2"], elem["Param_Tolerance_Expected_Val"], elem["Param_Result_Val"].ToString() == "true" || elem["Param_Result_Val"].ToString() == "false" ? "" : elem["Param_Result_Val"], elem["Param_Tolerance_Result_Val"], elem["CatagoryName"], elem["ParamType"], elem["List_ParamType"], elem["Bold_Format"], bytes, elem["Cover"], elem["Customer"], elem["OtherSpec"], elem["InvNo"], elem["InvDate"], elem["Qty"], elem["Winding"], elem["Surface"], elem["Clean"], elem["Edge"], elem["Polythene"], elem["Box"], elem["Wrapper"], elem["Trace"], elem["Weight"], elem["Size"]);
                }
            }
            //Binding Misc_Data...
            var obj_Misc_Data = from u in db.tb_ValuesOfReports
                                where u.ReportID == ID
                                select new
                                {
                                    u.ID,
                                    productID = u.tb_Product.ID,
                                    u.tb_Reports.ReportName,
                                    u.tb_Reports.CreatedDate,
                                    Product = u.tb_Product.tb_Wirename.Wirename + " [Size : " + u.tb_Product.tb_Size.Size + ", Degree : " + u.tb_Product.tb_Degree.Type + "]",
                                    Result = u.tb_Reports.Result
                                };

            var obj_Misc_Data1 = from u in obj_Misc_Data.ToList()
                                 select new
                                 {
                                     u.ID,
                                     u.productID,
                                     u.ReportName,
                                     Date = Convert.ToDateTime(u.CreatedDate).ToString("dd-MM-yyyy"),
                                     u.Product,
                                     Grade = GetGrade(u.productID),
                                 };
            string Result = db.tb_ValuesOfReports.FirstOrDefault(u => u.tb_Reports.ID == ID).tb_Reports.Result;
            if (obj_Misc_Data1 != null)
            {
                foreach (var elem in obj_Misc_Data1.ToList())
                {
                    ds.Misc_Data.Rows.Add(elem.ID, IsPassed, elem.ReportName, elem.Date, elem.Product,"", Result);
                }
            }

            this.reportViewer1.LocalReport.DataSources.Clear();
            //itemDataSource1 = new ReportDataSource("ElectricalDataSet", ds.Tables["Electrical_Report"]);
            //this.reportViewer1.LocalReport.DataSources.Add(itemDataSource1);
            itemDataSource1 = new ReportDataSource("CaliberDataSet", ds.Tables["Caliber_Report"]);
            this.reportViewer1.LocalReport.DataSources.Add(itemDataSource1);
            //itemDataSource1 = new ReportDataSource("ThermalDataSet", ds.Tables["Thermal_Report"]);
            //this.reportViewer1.LocalReport.DataSources.Add(itemDataSource1);
            //itemDataSource1 = new ReportDataSource("ChemicalDataSet", ds.Tables["Chemical_Report"]);
            //this.reportViewer1.LocalReport.DataSources.Add(itemDataSource1);
            //itemDataSource1 = new ReportDataSource("MechanicalDataSet", ds.Tables["Mechanical_Report"]);
            //this.reportViewer1.LocalReport.DataSources.Add(itemDataSource1);
            itemDataSource1 = new ReportDataSource("Misc_DataSet", ds.Tables["Misc_Data"]);
            this.reportViewer1.LocalReport.DataSources.Add(itemDataSource1);
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
