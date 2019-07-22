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
    public partial class Parameter_Value : Form
    {
        public Parameter_Value()
        {
            InitializeComponent();
            DisableGrids();
            BindProduct();
        }
        public void GridsWeNeed(int ID)
        {
            DisableGrids();
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            int wire_ID = (int)db.tb_Product.FirstOrDefault(u => u.ID == ID && u.Enable == 1).WirenameID;
            var ParamsType = db.tb_ParamName.Where(u => u.WireID == wire_ID && u.Enable == 1);
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
            //BindIntoGrids(wire_ID);
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
                      orderby u.ID descending
                      where u.Status == "Active" && u.Enable == 1
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
        int isUpdate = 0;
        public void LocatePanel()
        {
            string[] Locations = { "4, 3", "314, 4", "624, 3", "4, 292", "314, 292", "627, 292"};
            //CountingGrids in Need...
            int count = -1;
            if (panel_Max.Visible == true)
            {
                count++;
                string[] points = Locations[count].Split(',');
                Point newPoints = new Point(int.Parse(points[0]), int.Parse(points[1]));
                panel_Max.Location = newPoints;
            }
            if(panel_Min.Visible == true)
            {
                count++;
                string[] points = Locations[count].Split(',');
                Point newPoints = new Point(int.Parse(points[0]), int.Parse(points[1]));
                panel_Min.Location = newPoints;
            }
            if (panel_List.Visible == true)
            {
                count++;
                string[] points = Locations[count].Split(',');
                Point newPoints = new Point(int.Parse(points[0]), int.Parse(points[1]));
                panel_List.Location = newPoints;
            }
            if (panel_Btw.Visible == true)
            {
                count++;
                string[] points = Locations[count].Split(',');
                Point newPoints = new Point(int.Parse(points[0]), int.Parse(points[1]));
                panel_Btw.Location = newPoints;
            }
            if (panel_Same_Value.Visible == true)
            {
                count++;
                string[] points = Locations[count].Split(',');
                Point newPoints = new Point(int.Parse(points[0]), int.Parse(points[1]));
                panel_Same_Value.Location = newPoints;
            }
            if (panel_True_False.Visible == true)
            {
                count++;
                string[] points = Locations[count].Split(',');
                Point newPoints = new Point(int.Parse(points[0]), int.Parse(points[1]));
                panel_True_False.Location = newPoints;
            }
        }
        public void Submit()
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            int ProductID = Convert.ToInt32(comboBox1.SelectedValue);

            tb_Reports obj = new tb_Reports();
            db.tb_Reports.Add(obj);

            if (panel_Max.Visible == true)
            {
                foreach (DataRow elem in Dt_Max.Rows)
                {
                    if (elem["ID"].ToString() != "") 
                    {
                        int ID = Convert.ToInt32(elem["ID"]);
                        var objMax_update = db.tb_ParamValue.FirstOrDefault(u=>u.ProductID == ProductID && u.ID == ID);
                        objMax_update.Val1 = (string)elem["Param_Max_Value"];
                        objMax_update.Tolerance = Convert.ToString(elem["Tolerance"]);
                    }
                    else
                    {
                        tb_ParamValue objParamVal = new tb_ParamValue();
                        string ParamName = (string)elem["Param_Max_Name"];
                        objParamVal.ParamNameID = db.tb_ParamName.FirstOrDefault(u => u.Name == ParamName).ID;
                        objParamVal.ProductID = ProductID;
                        objParamVal.Val1 = (string)elem["Param_Max_Value"];
                        objParamVal.Tolerance = Convert.ToString(elem["Tolerance"]);
                        objParamVal.Status = "Active";
                        db.tb_ParamValue.Add(objParamVal);
                    }
                }
            }
            if (panel_Min.Visible == true)
            {
                foreach (DataRow elem in Dt_Min.Rows)
                {

                    if (elem["ID"].ToString() != "")
                    {
                        int ID = Convert.ToInt32(elem["ID"]);
                        var objMax_update = db.tb_ParamValue.FirstOrDefault(u => u.ProductID == ProductID && u.ID == ID);
                        objMax_update.Val1 = (string)elem["Param_Min_Value"];
                        objMax_update.Tolerance = Convert.ToString(elem["Tolerance"]);
                    }
                    else
                    {
                        tb_ParamValue objParamVal = new tb_ParamValue();
                        string ParamName = (string)elem["Param_Min_Name"];
                        objParamVal.ParamNameID = db.tb_ParamName.FirstOrDefault(u => u.Name == ParamName).ID;
                        objParamVal.ProductID = ProductID;
                        objParamVal.Val1 = (string)elem["Param_Min_Value"];
                        objParamVal.Tolerance = Convert.ToString(elem["Tolerance"]);

                        objParamVal.Status = "Active";
                        db.tb_ParamValue.Add(objParamVal);
                    }
                }
            }
            if (panel_Btw.Visible == true)
            {
                foreach (DataRow elem in Dt_btw.Rows)
                {
                    if (elem["ID"].ToString() != "")
                    {
                        int ID = Convert.ToInt32(elem["ID"]);
                        var objMax_update = db.tb_ParamValue.FirstOrDefault(u => u.ProductID == ProductID && u.ID == ID);
                        objMax_update.Val1 = Convert.ToString(elem["Param_Btw_Value_From"]);
                        objMax_update.Val2 = Convert.ToString(elem["Param_Btw_Value_To"]);
                        objMax_update.Tolerance = Convert.ToString(elem["Tolerance"]);
                    }
                    else
                    {
                        tb_ParamValue objParamVal = new tb_ParamValue();
                        string ParamName = (string)elem["Param_Btw_Name"];
                        objParamVal.ParamNameID = db.tb_ParamName.FirstOrDefault(u => u.Name == ParamName).ID;
                        objParamVal.ProductID = ProductID;
                        objParamVal.Val1 = Convert.ToString(elem["Param_Btw_Value_From"]);
                        objParamVal.Val2 = Convert.ToString(elem["Param_Btw_Value_To"]);
                        objParamVal.Tolerance = Convert.ToString(elem["Tolerance"]);

                        objParamVal.Status = "Active";
                        db.tb_ParamValue.Add(objParamVal);
                    }
                }
            }
            if (panel_True_False.Visible == true)
            {
                int pvt_Cont = 0;
                int Rows = gridView4.RowCount;
                foreach (DataRow elem in Dt_TrueFalse.Rows)
                {
                    if (elem["ID"].ToString() != "")
                    {
                        //Case Update...
                        int Praram_ID = Convert.ToInt32(elem["ID"]);
                        var obj_Update = db.tb_ParamValue.FirstOrDefault(u=>u.ID == Praram_ID);
                        obj_Update.Val1 = gridView4.IsRowSelected(pvt_Cont) == true ? "true" : "false";
                    }
                    else
                    {
                        tb_ParamValue objParam = new tb_ParamValue();
                        objParam.Val1 = gridView4.IsRowSelected(pvt_Cont) == true ? "true" : "false";
                        objParam.ProductID = ProductID;
                        objParam.Status = "Active";
                        string param_name = Convert.ToString(elem["Param_TrueFalse_Name"]);
                        objParam.ParamNameID = db.tb_ParamName.FirstOrDefault(u => u.Name == param_name).ID;

                        db.tb_ParamValue.Add(objParam);
                    }
                    pvt_Cont++;
                }
            }
            if (panel_Same_Value.Visible == true)
            {
                foreach (DataRow elem in Dt_SameValue.Rows)
                {
                    if (elem["ID"].ToString() != "")
                    {
                        int ParamID = Convert.ToInt32(elem["ID"]);
                        var obj_Update = db.tb_ParamValue.FirstOrDefault(u => u.ID == ParamID);
                        string ParamName = (string)elem["Param_SameVal_Name"];
                        obj_Update.ParamNameID = db.tb_ParamName.FirstOrDefault(u => u.Name == ParamName).ID;
                        obj_Update.Val1 = Convert.ToString(elem["Param_SameVal_Value"]);
                        obj_Update.Tolerance = Convert.ToString(elem["Tolerance"]);
                    }
                    else
                    {
                        tb_ParamValue objParamVal = new tb_ParamValue();
                        string ParamName = Convert.ToString(elem["Param_SameVal_Name"]);
                        objParamVal.ParamNameID = db.tb_ParamName.FirstOrDefault(u => u.Name == ParamName).ID;
                        objParamVal.ProductID = Convert.ToInt32(comboBox1.SelectedValue);
                        objParamVal.Val1 = Convert.ToString(elem["Param_SameVal_Value"]);
                        objParamVal.Tolerance = Convert.ToString(elem["Tolerance"]);
                        objParamVal.Status = "Active";
                        db.tb_ParamValue.Add(objParamVal);
                    }
                }
            }
            db.SaveChanges();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)//Button Click OK...
        {
            int ID = Convert.ToInt32(comboBox1.SelectedValue);
            GridsWeNeed(ID);
            //Checking if values exsits already...
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var obj_Param_Values = from u in db.tb_ParamValue
                                   where u.ID == ID 
                                   select new
                                   {
                                       u.ID,
                                       u.Val1,
                                       u.Val2,
                                       u.Tolerance,
                                       u.tb_ParamName.Name,
                                   };
            if (obj_Param_Values != null)
            {
                isUpdate = 1;
                int wire_ID = (int)db.tb_Product.FirstOrDefault(u => u.ID == ID).WirenameID;
                if (panel_Max.Visible == true)
                {
                    
                    var obj1 = from u in db.tb_ParamValue
                               where u.ProductID == ID && u.tb_ParamName.tb_ParamTypes.ParamName == "Max" && u.Status == "Active" && u.tb_ParamName.Enable == 1
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

                    if (obj1.Count() > 0)
                    {
                        foreach (var elem in obj1.ToList())
                        {
                              Dt_Max.Rows.Add(new object[]{
                              elem.Name,elem.Val1,elem.ID,elem.Tolerance
                         });
                        }
                    }
                    var obj_1 = from u in db.tb_ParamName
                                    where u.tb_ParamTypes.ParamName == "Max" && u.WireID == wire_ID && u.Status == "Active" && u.Enable == 1 && u.Formula == null
                                    select new
                                    {
                                        u.ID,
                                        u.WireID,
                                        u.Name,
                                        _ID = ""
                                    };
                        foreach (var elem in obj_1.ToList())
                        {
                            var obj = db.tb_ParamValue.Where(u => u.ProductID == ID && u.ParamNameID == elem.ID && u.tb_ParamName.Enable == 1).FirstOrDefault();
                            if (obj == null)
                            {
                                Dt_Max.Rows.Add(
                                    elem.Name, "",elem._ID,""
                                );
                            }
                        }
                    grdMax.DataSource = Dt_Max;
                }
                if (panel_Min.Visible == true)
                {
                    var obj2 = from u in db.tb_ParamValue
                               where u.ProductID == ID && u.tb_ParamName.tb_ParamTypes.ParamName == "Min" && u.Status == "Active" && u.tb_ParamName.Enable == 1
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
                    if (obj2.Count() > 0)
                    {
                        
                        foreach (var elem in obj2.ToList())
                        {
                                Dt_Min.Rows.Add(new object[]{
                                elem.Name,elem.Val1,elem.ID,elem.Tolerance
                                });
                            }
                        }
                        var obj_2 = from u in db.tb_ParamName
                                    where u.tb_ParamTypes.ParamName == "Min" && u.WireID == wire_ID && u.Status == "Active" && u.Enable == 1 && u.Formula == null
                                    select new
                                    {
                                        u.ID,
                                        u.WireID,
                                        u.Name,
                                        _ID = ""
                                    };

                        foreach (var elem in obj_2.ToList())
                        {
                            var obj = db.tb_ParamValue.Where(u => u.ProductID == ID && u.ParamNameID == elem.ID && u.tb_ParamName.Enable == 1).FirstOrDefault();
                            if (obj == null)
                            {
                                Dt_Min.Rows.Add(
                                    elem.Name,"",elem._ID,""
                                );
                            }
                        }
                    grdMin.DataSource = Dt_Min;
                }

                if (panel_Btw.Visible == true)
                {
                    var obj3 = from u in db.tb_ParamValue
                               where u.ProductID == ID && u.tb_ParamName.tb_ParamTypes.ParamName == "Between" && u.Status == "Active" && u.tb_ParamName.Enable == 1
                               select new
                               {
                                   u.ID,
                                   u.tb_ParamName.Name,
                                   u.Val1,
                                   u.Val2,
                                   u.Tolerance
                               };

                    Dt_btw = new DataTable();
                    Dt_btw.Columns.Add("Param_Btw_Name");
                    Dt_btw.Columns.Add("Param_Btw_Value_From");
                    Dt_btw.Columns.Add("Param_Btw_Value_To");
                    Dt_btw.Columns.Add("ID");
                    Dt_btw.Columns.Add("Tolerance");

                    if (obj3.Count() > 0)
                    {
                       
                        foreach (var elem in obj3.ToList())
                        {
                            Dt_btw.Rows.Add(new object[]{
                                elem.Name,elem.Val1,elem.Val2,elem.ID,elem.Tolerance
                            });
                        }
                    }

                        var obj_3 = from u in db.tb_ParamName
                                    where u.tb_ParamTypes.ParamName == "Between" && u.WireID == wire_ID && u.Status == "Active" && u.Enable == 1 && u.Formula == null
                                    select new
                                    {
                                        u.ID,
                                        u.WireID,
                                        u.Name,
                                        _ID = ""
                                    };

                        foreach (var elem in obj_3.ToList())
                        {
                            var obj = db.tb_ParamValue.Where(u => u.ProductID == ID && u.ParamNameID == elem.ID && u.tb_ParamName.Enable == 1).FirstOrDefault();
                            if (obj == null)
                            {
                                Dt_btw.Rows.Add(
                                    elem.Name, "",elem._ID,""
                                );
                            }
                        }
                    grdBtw.DataSource = Dt_btw;
                }
                if (panel_True_False.Visible == true)
                {
                    var obj4 = from u in db.tb_ParamValue
                               where u.ProductID == ID && u.tb_ParamName.tb_ParamTypes.ParamName == "True False" && u.Status == "Active" && u.tb_ParamName.Enable == 1
                               select new
                               {
                                   u.ID,
                                   u.tb_ParamName.Name,
                                   u.Val1,
                               };
                    Dt_TrueFalse = new DataTable();
                    Dt_TrueFalse.Columns.Add("Param_TrueFalse_Name");
                    Dt_TrueFalse.Columns.Add("Param_TrueFalse_Value");
                    Dt_TrueFalse.Columns.Add("ID");
                    int RowIndex = 0;
                    if (obj4.Count() > 0)
                    {
                            foreach (var elem in obj4.ToList())
                            {
                                Dt_TrueFalse.Rows.Add(new object[]{
                             elem.Name,elem.Val1,elem.ID
                            });
                            }
                            foreach (DataRow el in Dt_TrueFalse.Rows)
                            {
                                if (el["Param_TrueFalse_Value"].ToString() == "true")
                                {
                                    gridView4.SelectRow(RowIndex);
                                }
                            }
                            RowIndex = 0;
                    }

                    var obj_4 = from u in db.tb_ParamName
                                where u.tb_ParamTypes.ParamName == "True False" && u.WireID == wire_ID && u.Status == "Active" && u.Enable == 1 && u.Formula == null
                                select new
                                {
                                    u.ID,
                                    u.WireID,
                                    u.Name,
                                    _ID = ""
                                };

                    foreach (var elem in obj_4.ToList())
                    {
                        var obj = db.tb_ParamValue.Where(u => u.ProductID == ID && u.ParamNameID == elem.ID && u.tb_ParamName.Enable == 1).FirstOrDefault();
                        if (obj == null)
                        {
                            Dt_TrueFalse.Rows.Add(
                                elem.Name, "", elem._ID
                            );
                        }
                        grdTrueFalse.DataSource = Dt_TrueFalse;
                    }
                    foreach (DataRow el in Dt_TrueFalse.Rows)
                    {
                        if (el["Param_TrueFalse_Value"].ToString() == "true")
                        {
                            gridView4.SelectRow(RowIndex);
                        }
                    }
                    RowIndex = 0;
                }
                    if (panel_List.Visible == true)
                    {
                        var obj5 = from u in db.tb_ParamValue
                                   where u.ProductID == ID && u.tb_ParamName.tb_ParamTypes.ParamName == "List" && u.Status == "Active" && u.tb_ParamName.Enable == 1
                                   select new
                                   {
                                       u.ID,
                                       u.tb_ParamName.Name,
                                       u.Val1,
                                       u.Tolerance
                                   };
                        Dt_List = new DataTable();
                        Dt_List.Columns.Add("Param_List_Self_ID").ColumnMapping = MappingType.Hidden;
                        Dt_List.Columns.Add("Param_List_Name");
                        Dt_List.Columns.Add("Param_List_Value");
                        Dt_List.Columns.Add("ID");
                        if (obj5.Count() > 0)
                        {
                            foreach (var elem in obj5.ToList())
                            {
                                Dt_List.Rows.Add(new object[]{
                                 elem.ID,elem.Name,elem.Val1,elem.ID
                                });
                            }
                        }

                            var obj_5 = from u in db.tb_ParamName
                                        where u.tb_ParamTypes.ParamName == "List" && u.WireID == wire_ID && u.Status == "Active" && u.Enable == 1 && u.Formula == null
                                        select new
                                        {
                                            u.ID,
                                            u.WireID,
                                            u.Name,
                                            _ID = ""
                                        };
                        
                            foreach (var elem in obj_5.ToList())
                            {
                                if (obj_5 != null)
                                {
                                    Dt_List.Rows.Add(
                                        elem.ID,elem.Name, "",elem._ID
                                    );
                                }
                            }
                        grdList.DataSource = Dt_List;
                    }
                    if (panel_Same_Value.Visible == true)
                    {
                        var obj6 = from u in db.tb_ParamValue
                                   where u.ProductID == ID && u.tb_ParamName.tb_ParamTypes.ParamName == "Same Value" && u.Status == "Active" && u.tb_ParamName.Enable == 1
                                   select new
                                   {
                                       u.ID,
                                       u.tb_ParamName.Name,
                                       u.Val1,
                                       u.Tolerance
                                   };
                        Dt_SameValue = new DataTable();
                        Dt_SameValue.Columns.Add("Param_SameVal_Name");
                        Dt_SameValue.Columns.Add("Param_SameVal_Value");
                        Dt_SameValue.Columns.Add("ID");
                        Dt_SameValue.Columns.Add("Tolerance");
                        if (obj6.Count() > 0)
                        {
                      
                            foreach (var elem in obj6.ToList())
                            {
                                Dt_SameValue.Rows.Add(new object[]{
                                    elem.Name,elem.Val1,elem.ID,elem.Tolerance
                                });
                            }
                        }
                        
                            var obj_6 = from u in db.tb_ParamName
                                        where u.tb_ParamTypes.ParamName == "Same Value" && u.WireID == wire_ID && u.Status == "Active" && u.Enable == 1 && u.Formula == null
                                        select new
                                        {
                                            u.ID,
                                            u.WireID,
                                            u.Name,
                                            _ID = ""
                                        };
                            
                            foreach (var elem in obj_6.ToList())
                            {
                                var obj = db.tb_ParamValue.Where(u => u.ProductID == ID && u.ParamNameID == elem.ID && u.tb_ParamName.Enable == 1).FirstOrDefault();
                                if (obj == null)
                                {
                                    Dt_SameValue.Rows.Add(
                                        elem.Name, "",elem._ID,""
                                    );
                                }
                            }
                        grdSameVal.DataSource = Dt_SameValue;
                    }
                }
            }
        public void SelectRow(string Case, int index)
        {
            if (Case == "true")
            {
                gridView4.SelectRow(index);
            }
        }
        private void button2_Click(object sender, EventArgs e)//Button Click Submit...
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            int ID = Convert.ToInt32(comboBox1.SelectedValue);
            //int wire_ID = (int)db.tb_Product.FirstOrDefault(u => u.ID == ID).WirenameID;
            Submit();
            MessageBox.Show("Record Has Been Sucessfully Updated!");
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void grdMin_Click(object sender, EventArgs e)
        {

        }
        private void Parameter_Value_Load(object sender, EventArgs e)
        {

        }
        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //int ID = gridView5.GetFocusedRowCellValue("Param_List_Self_ID") != null ? Convert.ToInt32(gridView5.GetFocusedRowCellValue("Param_List_Self_ID")) : Convert.ToInt32(gridView5.GetFocusedRowCellValue("ID"));
            int ID = Convert.ToInt32(gridView5.GetFocusedRowCellValue("Param_List_Self_ID"));
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            var obj = db.tb_ParamName.FirstOrDefault(u => u.ID == ID).ID;
            int P_ID = Convert.ToInt32(obj);
            int Product_ID = Convert.ToInt32(comboBox1.SelectedValue);
            List_Values obj_ListValues_Form = new List_Values(P_ID,Product_ID);
            obj_ListValues_Form.ShowDialog();

        }
        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                (grdMax.FocusedView as ColumnView).FocusedRowHandle++;
                e.Handled = true;
            }
        }

        private void gridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                (grdMin.FocusedView as ColumnView).FocusedRowHandle++;
                e.Handled = true;
            }
        }

        private void gridView3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                (grdBtw.FocusedView as ColumnView).FocusedRowHandle++;
                e.Handled = true;
            }
        }

        private void gridView4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                (grdTrueFalse.FocusedView as ColumnView).FocusedRowHandle++;
                e.Handled = true;
            }
        }

        private void gridView5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                (grdList.FocusedView as ColumnView).FocusedRowHandle++;
                e.Handled = true;
            }
        }

        private void gridView6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                (grdSameVal.FocusedView as ColumnView).FocusedRowHandle++;
                e.Handled = true;
            }
        }

        private void gridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void gridView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {

        }

        private void gridView1_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
        }

        private void grdBtw_Click(object sender, EventArgs e)
        {

        }
    }
}
