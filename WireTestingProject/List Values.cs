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
    public partial class List_Values : Form
    {
        int Paramter_Name_ID = 0;
        int P_Id = 0;
        DataTable Dt_List = new DataTable();
        bool IsTrueOrFalse = false;
        public List_Values(int ID,int PRoduct_ID)
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            InitializeComponent();
            Paramter_Name_ID = ID;
            P_Id = PRoduct_ID;
            IsTrueOrFalse = db.tb_ParamList.FirstOrDefault(u => u.ParamNameID == Paramter_Name_ID).tb_ParamTypes.ParamName == "True False" ? true : false;
            if (IsTrueOrFalse == true)
            {
                List_Parameter_Values.Visible = false;
                gridColumn2.Visible = false;
            }   
            BindList();
        }
        public void BindList()
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            //tb_ParamValue objParamValues = new tb_ParamValue();
            Dt_List = new DataTable();
            Dt_List.Columns.Add("ID");
            Dt_List.Columns.Add("Self_ID");
            Dt_List.Columns.Add("Name");
            Dt_List.Columns.Add("Val");
            Dt_List.Columns.Add("Val2");
            Dt_List.Columns.Add("Type");
            Dt_List.Columns.Add("Tolerance");
            int RowIndex = 0;
            var obj = from u in db.tb_ParamListValues
                      where u.ParamNameID == Paramter_Name_ID
                      select new
                      {
                        u.tb_ParamList.ID,
                        self_Id = u.ID,
                        u.tb_ParamList.Name,
                        u.Val,
                        u.Val2,
                        Type = u.tb_ParamList.tb_ParamTypes.ParamName,
                        u.Tolerance
                      };    
            if (obj.Count() > 0)
            {
                foreach (var elem in obj.ToList())
                {
                    Dt_List.Rows.Add(new object[]{
                        elem.ID,elem.self_Id,elem.Name,elem.Val,elem.Val2,elem.Type,elem.Tolerance
                    });
                }
            }
                var obj_1 = from u in db.tb_ParamList
                            where u.ParamNameID == Paramter_Name_ID
                          select new
                          {
                              u.ID,
                              u.Name,
                              Type = u.tb_ParamTypes.ParamName
                          };
                if (obj_1.Count() > 0)
                {
                    foreach (var elem1 in obj_1.ToList())
                    {
                        var obj_11 = db.tb_ParamListValues.Where(u =>u.ParamListID == elem1.ID).FirstOrDefault();
                        if (obj_11 == null)
                        {
                            Dt_List.Rows.Add(new object[]{
                                elem1.ID,"",elem1.Name,"","",elem1.Type,""
                            });
                        }
                    }
                    
                }
            gridControl1.DataSource = Dt_List;
            foreach (DataRow el in Dt_List.Rows)
            {
                if (el["Val"].ToString() == "true")
                {
                    gridView1.SelectRow(RowIndex);
                }
                RowIndex++;
            }
            RowIndex = 0;
        }
        public void Submit()
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            int RowIndex = 0;
            foreach (DataRow el in Dt_List.Rows)
            {
                if (el["Self_ID"].ToString() != "")
                {
                    int self_Id = Convert.ToInt32(el["Self_ID"]);
                    var obj = db.tb_ParamListValues.FirstOrDefault(u => u.ID == self_Id);
                    if (IsTrueOrFalse == false)
                    {
                        obj.Val = Convert.ToString(el["Val"]);
                        obj.Val2 = Convert.ToString(el["Val2"]);
                    }
                    else
                    {
                        if (gridView1.IsRowSelected(RowIndex))
                        {
                            obj.Val = "true";
                        }
                        else
                        {
                            obj.Val = "false";
                        }
                    }
                    obj.Tolerance = Convert.ToString(el["Tolerance"]);
                    RowIndex++;
                }
                else {
                    tb_ParamListValues objParamList = new tb_ParamListValues();
                    if (IsTrueOrFalse == false)
                    {
                        objParamList.Val = Convert.ToString(el["Val"]);
                        objParamList.Val2 = Convert.ToString(el["Val2"]);
                    }
                    else
                    {
                        if (gridView1.IsRowSelected(RowIndex))
                        {
                            objParamList.Val = "true";
                        }
                        else
                        {
                            objParamList.Val = "false";
                        }
                    }
                    objParamList.ParamListID = Convert.ToInt32(el["ID"]);
                    objParamList.Tolerance = Convert.ToString(el["Tolerance"]);
                    objParamList.ParamNameID = Paramter_Name_ID;
                    
                    objParamList.CreatedDate = DateTime.Today;
                    objParamList.Status = "Active";
                    db.tb_ParamListValues.Add(objParamList);
                    RowIndex++;
                   }
            }
            db.SaveChanges();
        }
        private void List_Values_Load(object sender, EventArgs e)
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            BindList();
            label4.Text = db.tb_ParamName.FirstOrDefault(u => u.ID == Paramter_Name_ID).Name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Submit();
            MessageBox.Show("Record Added Succesesfully!");
        }
    }
}
