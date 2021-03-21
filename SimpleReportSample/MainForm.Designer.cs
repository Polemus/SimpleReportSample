
using System.Windows.Forms;

namespace SimpleReportSample
{
    partial class MainForm
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
            this.ActionButton = new System.Windows.Forms.Button();
            this.PathToContractorsAndContracts = new System.Windows.Forms.TextBox();
            this.PathToPaymentsDoc = new System.Windows.Forms.TextBox();
            this.PathToTaskSummaryDoc = new System.Windows.Forms.TextBox();
            this.SelectPathToContractorsAndContracts = new System.Windows.Forms.Button();
            this.SelectPathToTaskSummaryDoc = new System.Windows.Forms.Button();
            this.SelectPathToPaymentsDoc = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DateTimeFrom = new System.Windows.Forms.DateTimePicker();
            this.DateTimeTo = new System.Windows.Forms.DateTimePicker();
            this.PeriodFrom = new System.Windows.Forms.Label();
            this.PeriodTo = new System.Windows.Forms.Label();
            this.SaveToPDF = new System.Windows.Forms.Button();
            this.EmploeeGridView = new System.Windows.Forms.DataGridView();
            this.CreateInvoiceFiles = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.EmployeeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GenerateReportProgressBar = new System.Windows.Forms.ProgressBar();
            this.SelectAllRows = new System.Windows.Forms.Button();
            this.ProgressBarLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.EmploeeGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ActionButton
            // 
            this.ActionButton.Location = new System.Drawing.Point(694, 558);
            this.ActionButton.Name = "ActionButton";
            this.ActionButton.Size = new System.Drawing.Size(262, 32);
            this.ActionButton.TabIndex = 0;
            this.ActionButton.Text = "Обработать";
            this.ActionButton.UseVisualStyleBackColor = true;
            this.ActionButton.Visible = false;
            this.ActionButton.Click += new System.EventHandler(this.ActionButton_Click);
            // 
            // PathToContractorsAndContracts
            // 
            this.PathToContractorsAndContracts.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PathToContractorsAndContracts.Location = new System.Drawing.Point(34, 116);
            this.PathToContractorsAndContracts.Name = "PathToContractorsAndContracts";
            this.PathToContractorsAndContracts.Size = new System.Drawing.Size(528, 21);
            this.PathToContractorsAndContracts.TabIndex = 1;
            // 
            // PathToPaymentsDoc
            // 
            this.PathToPaymentsDoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PathToPaymentsDoc.Location = new System.Drawing.Point(34, 180);
            this.PathToPaymentsDoc.Name = "PathToPaymentsDoc";
            this.PathToPaymentsDoc.Size = new System.Drawing.Size(528, 21);
            this.PathToPaymentsDoc.TabIndex = 2;
            // 
            // PathToTaskSummaryDoc
            // 
            this.PathToTaskSummaryDoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PathToTaskSummaryDoc.Location = new System.Drawing.Point(34, 241);
            this.PathToTaskSummaryDoc.Name = "PathToTaskSummaryDoc";
            this.PathToTaskSummaryDoc.Size = new System.Drawing.Size(528, 21);
            this.PathToTaskSummaryDoc.TabIndex = 3;
            // 
            // SelectPathToContractorsAndContracts
            // 
            this.SelectPathToContractorsAndContracts.Location = new System.Drawing.Point(589, 116);
            this.SelectPathToContractorsAndContracts.Name = "SelectPathToContractorsAndContracts";
            this.SelectPathToContractorsAndContracts.Size = new System.Drawing.Size(75, 21);
            this.SelectPathToContractorsAndContracts.TabIndex = 4;
            this.SelectPathToContractorsAndContracts.Text = "Выбрать";
            this.SelectPathToContractorsAndContracts.UseVisualStyleBackColor = true;
            this.SelectPathToContractorsAndContracts.Click += new System.EventHandler(this.SelectPathToContractorsAndContracts_Click);
            // 
            // SelectPathToTaskSummaryDoc
            // 
            this.SelectPathToTaskSummaryDoc.Location = new System.Drawing.Point(589, 242);
            this.SelectPathToTaskSummaryDoc.Name = "SelectPathToTaskSummaryDoc";
            this.SelectPathToTaskSummaryDoc.Size = new System.Drawing.Size(75, 21);
            this.SelectPathToTaskSummaryDoc.TabIndex = 5;
            this.SelectPathToTaskSummaryDoc.Text = "Выбрать";
            this.SelectPathToTaskSummaryDoc.UseVisualStyleBackColor = true;
            this.SelectPathToTaskSummaryDoc.Click += new System.EventHandler(this.SelectPathToTaskSummaryDoc_Click);
            // 
            // SelectPathToPaymentsDoc
            // 
            this.SelectPathToPaymentsDoc.Location = new System.Drawing.Point(589, 180);
            this.SelectPathToPaymentsDoc.Name = "SelectPathToPaymentsDoc";
            this.SelectPathToPaymentsDoc.Size = new System.Drawing.Size(75, 21);
            this.SelectPathToPaymentsDoc.TabIndex = 6;
            this.SelectPathToPaymentsDoc.Text = "Выбрать";
            this.SelectPathToPaymentsDoc.UseVisualStyleBackColor = true;
            this.SelectPathToPaymentsDoc.Click += new System.EventHandler(this.SelectPathToPaymentsDoc_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Контракты и исполнители";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Данные о платежах";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 225);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(532, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Time Report ( Следует указать путь к папке с Time Reports всех работников отмечен" +
    "ных для обработки)";
            // 
            // DateTimeFrom
            // 
            this.DateTimeFrom.Location = new System.Drawing.Point(34, 53);
            this.DateTimeFrom.Name = "DateTimeFrom";
            this.DateTimeFrom.Size = new System.Drawing.Size(200, 20);
            this.DateTimeFrom.TabIndex = 10;
            // 
            // DateTimeTo
            // 
            this.DateTimeTo.Location = new System.Drawing.Point(362, 53);
            this.DateTimeTo.Name = "DateTimeTo";
            this.DateTimeTo.Size = new System.Drawing.Size(200, 20);
            this.DateTimeTo.TabIndex = 11;
            // 
            // PeriodFrom
            // 
            this.PeriodFrom.AutoSize = true;
            this.PeriodFrom.Location = new System.Drawing.Point(31, 37);
            this.PeriodFrom.Name = "PeriodFrom";
            this.PeriodFrom.Size = new System.Drawing.Size(62, 13);
            this.PeriodFrom.TabIndex = 12;
            this.PeriodFrom.Text = "Период, от";
            // 
            // PeriodTo
            // 
            this.PeriodTo.AutoSize = true;
            this.PeriodTo.Location = new System.Drawing.Point(359, 37);
            this.PeriodTo.Name = "PeriodTo";
            this.PeriodTo.Size = new System.Drawing.Size(63, 13);
            this.PeriodTo.TabIndex = 13;
            this.PeriodTo.Text = "Период, до";
            // 
            // SaveToPDF
            // 
            this.SaveToPDF.Location = new System.Drawing.Point(962, 558);
            this.SaveToPDF.Name = "SaveToPDF";
            this.SaveToPDF.Size = new System.Drawing.Size(262, 32);
            this.SaveToPDF.TabIndex = 14;
            this.SaveToPDF.Text = "Обработать в PDF";
            this.SaveToPDF.UseVisualStyleBackColor = true;
            this.SaveToPDF.Click += new System.EventHandler(this.SaveToPDF_Click);
            // 
            // EmploeeGridView
            // 
            this.EmploeeGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EmploeeGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CreateInvoiceFiles,
            this.EmployeeName});
            this.EmploeeGridView.Location = new System.Drawing.Point(694, 37);
            this.EmploeeGridView.Name = "EmploeeGridView";
            this.EmploeeGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.EmploeeGridView.Size = new System.Drawing.Size(529, 515);
            this.EmploeeGridView.TabIndex = 15;
            // 
            // CreateInvoiceFiles
            // 
            this.CreateInvoiceFiles.HeaderText = "Generate invoice documents";
            this.CreateInvoiceFiles.Name = "CreateInvoiceFiles";
            // 
            // EmployeeName
            // 
            this.EmployeeName.DataPropertyName = "EmployeeName";
            this.EmployeeName.HeaderText = "Employee Name";
            this.EmployeeName.Name = "EmployeeName";
            this.EmployeeName.Width = 385;
            // 
            // GenerateReportProgressBar
            // 
            this.GenerateReportProgressBar.Location = new System.Drawing.Point(729, 266);
            this.GenerateReportProgressBar.Name = "GenerateReportProgressBar";
            this.GenerateReportProgressBar.Size = new System.Drawing.Size(472, 38);
            this.GenerateReportProgressBar.TabIndex = 16;
            this.GenerateReportProgressBar.Visible = false;
            // 
            // SelectAllRows
            // 
            this.SelectAllRows.Location = new System.Drawing.Point(694, 8);
            this.SelectAllRows.Name = "SelectAllRows";
            this.SelectAllRows.Size = new System.Drawing.Size(123, 23);
            this.SelectAllRows.TabIndex = 17;
            this.SelectAllRows.Text = "Выбрать все";
            this.SelectAllRows.UseVisualStyleBackColor = true;
            this.SelectAllRows.Click += new System.EventHandler(this.SelectAllRows_Click);
            // 
            // ProgressBarLabel
            // 
            this.ProgressBarLabel.AutoSize = true;
            this.ProgressBarLabel.Location = new System.Drawing.Point(956, 278);
            this.ProgressBarLabel.Name = "ProgressBarLabel";
            this.ProgressBarLabel.Size = new System.Drawing.Size(0, 13);
            this.ProgressBarLabel.TabIndex = 18;
            this.ProgressBarLabel.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1235, 602);
            this.Controls.Add(this.ProgressBarLabel);
            this.Controls.Add(this.SelectAllRows);
            this.Controls.Add(this.GenerateReportProgressBar);
            this.Controls.Add(this.EmploeeGridView);
            this.Controls.Add(this.SaveToPDF);
            this.Controls.Add(this.PeriodTo);
            this.Controls.Add(this.PeriodFrom);
            this.Controls.Add(this.DateTimeTo);
            this.Controls.Add(this.DateTimeFrom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SelectPathToPaymentsDoc);
            this.Controls.Add(this.SelectPathToTaskSummaryDoc);
            this.Controls.Add(this.SelectPathToContractorsAndContracts);
            this.Controls.Add(this.PathToTaskSummaryDoc);
            this.Controls.Add(this.PathToPaymentsDoc);
            this.Controls.Add(this.PathToContractorsAndContracts);
            this.Controls.Add(this.ActionButton);
            this.Name = "MainForm";
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.EmploeeGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ActionButton;
        private System.Windows.Forms.TextBox PathToContractorsAndContracts;
        private System.Windows.Forms.TextBox PathToPaymentsDoc;
        private System.Windows.Forms.TextBox PathToTaskSummaryDoc;
        private System.Windows.Forms.Button SelectPathToContractorsAndContracts;
        private System.Windows.Forms.Button SelectPathToTaskSummaryDoc;
        private System.Windows.Forms.Button SelectPathToPaymentsDoc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker DateTimeFrom;
        private System.Windows.Forms.DateTimePicker DateTimeTo;
        private System.Windows.Forms.Label PeriodFrom;
        private System.Windows.Forms.Label PeriodTo;
        private System.Windows.Forms.Button SaveToPDF;
        private System.Windows.Forms.DataGridView EmploeeGridView;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CreateInvoiceFiles;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeeName;
        private ProgressBar GenerateReportProgressBar;
        private Button SelectAllRows;
        private Label ProgressBarLabel;
    }
}

