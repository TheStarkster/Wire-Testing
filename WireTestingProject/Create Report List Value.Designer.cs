namespace WireTestingProject
{
    partial class Create_Report_List_Value
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleContains formatConditionRuleContains1 = new DevExpress.XtraEditors.FormatConditionRuleContains();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Create_Report_List_Value));
            DevExpress.XtraGrid.GridFormatRule gridFormatRule2 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleContains formatConditionRuleContains2 = new DevExpress.XtraEditors.FormatConditionRuleContains();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.List_ParameterName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.List_Parameter_Values = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ParamName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Result = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(550, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 17);
            this.label4.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(471, 681);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 37);
            this.button1.TabIndex = 8;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 83);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1027, 567);
            this.gridControl1.TabIndex = 7;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.Click += new System.EventHandler(this.gridControl1_Click);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.List_ParameterName,
            this.List_Parameter_Values,
            this.ParamName,
            this.gridColumn1,
            this.Result});
            gridFormatRule1.Column = this.Result;
            gridFormatRule1.ColumnApplyTo = this.Result;
            gridFormatRule1.Name = "Format0";
            formatConditionRuleContains1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            formatConditionRuleContains1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            formatConditionRuleContains1.Appearance.ForeColor = System.Drawing.Color.White;
            formatConditionRuleContains1.Appearance.Options.UseBackColor = true;
            formatConditionRuleContains1.Appearance.Options.UseFont = true;
            formatConditionRuleContains1.Appearance.Options.UseForeColor = true;
            formatConditionRuleContains1.Values = ((System.Collections.IList)(resources.GetObject("formatConditionRuleContains1.Values")));
            gridFormatRule1.Rule = formatConditionRuleContains1;
            gridFormatRule2.Column = this.Result;
            gridFormatRule2.ColumnApplyTo = this.Result;
            gridFormatRule2.Name = "Format1";
            formatConditionRuleContains2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            formatConditionRuleContains2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            formatConditionRuleContains2.Appearance.ForeColor = System.Drawing.Color.White;
            formatConditionRuleContains2.Appearance.Options.UseBackColor = true;
            formatConditionRuleContains2.Appearance.Options.UseFont = true;
            formatConditionRuleContains2.Appearance.Options.UseForeColor = true;
            formatConditionRuleContains2.Values = ((System.Collections.IList)(resources.GetObject("formatConditionRuleContains2.Values")));
            gridFormatRule2.Rule = formatConditionRuleContains2;
            this.gridView1.FormatRules.Add(gridFormatRule1);
            this.gridView1.FormatRules.Add(gridFormatRule2);
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gridView1_SelectionChanged);
            this.gridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridView1_KeyDown);
            this.gridView1.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.gridView1_ValidatingEditor);
            // 
            // List_ParameterName
            // 
            this.List_ParameterName.Caption = "Parameter";
            this.List_ParameterName.FieldName = "Name";
            this.List_ParameterName.Name = "List_ParameterName";
            this.List_ParameterName.OptionsColumn.AllowEdit = false;
            this.List_ParameterName.OptionsColumn.AllowFocus = false;
            this.List_ParameterName.Visible = true;
            this.List_ParameterName.VisibleIndex = 1;
            // 
            // List_Parameter_Values
            // 
            this.List_Parameter_Values.Caption = "Values";
            this.List_Parameter_Values.FieldName = "Val";
            this.List_Parameter_Values.Name = "List_Parameter_Values";
            this.List_Parameter_Values.Visible = true;
            this.List_Parameter_Values.VisibleIndex = 3;
            // 
            // ParamName
            // 
            this.ParamName.Caption = "Type";
            this.ParamName.FieldName = "Type";
            this.ParamName.Name = "ParamName";
            this.ParamName.OptionsColumn.AllowEdit = false;
            this.ParamName.OptionsColumn.AllowFocus = false;
            this.ParamName.Visible = true;
            this.ParamName.VisibleIndex = 2;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Tolerance";
            this.gridColumn1.FieldName = "Tolerance";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 4;
            // 
            // Result
            // 
            this.Result.Caption = "Result";
            this.Result.FieldName = "Result";
            this.Result.Name = "Result";
            this.Result.Visible = true;
            this.Result.VisibleIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(409, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Parameter Name : ";
            // 
            // Create_Report_List_Value
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1051, 730);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.label2);
            this.Name = "Create_Report_List_Value";
            this.Text = "Create_Report_List_Value";
            this.Load += new System.EventHandler(this.Create_Report_List_Value_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn List_ParameterName;
        private DevExpress.XtraGrid.Columns.GridColumn List_Parameter_Values;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraGrid.Columns.GridColumn ParamName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn Result;
    }
}