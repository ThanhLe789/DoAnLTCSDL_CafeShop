namespace CoffeeShopSystem.Table
{
    partial class TableFrm
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btDeleteTable = new System.Windows.Forms.Button();
            this.btAddTable = new System.Windows.Forms.Button();
            this.dgvTables = new System.Windows.Forms.DataGridView();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nudCapacity = new System.Windows.Forms.NumericUpDown();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCapacity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(630, 88);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 73);
            this.label1.TabIndex = 0;
            this.label1.Text = "Table";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.Controls.Add(this.btDeleteTable);
            this.panel2.Controls.Add(this.btAddTable);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 319);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(630, 88);
            this.panel2.TabIndex = 2;
            // 
            // btDeleteTable
            // 
            this.btDeleteTable.Location = new System.Drawing.Point(122, 20);
            this.btDeleteTable.Name = "btDeleteTable";
            this.btDeleteTable.Size = new System.Drawing.Size(78, 46);
            this.btDeleteTable.TabIndex = 0;
            this.btDeleteTable.Text = "Delete";
            this.btDeleteTable.UseVisualStyleBackColor = true;
            this.btDeleteTable.Click += new System.EventHandler(this.btDeleteTable_Click);
            // 
            // btAddTable
            // 
            this.btAddTable.Location = new System.Drawing.Point(25, 20);
            this.btAddTable.Name = "btAddTable";
            this.btAddTable.Size = new System.Drawing.Size(78, 46);
            this.btAddTable.TabIndex = 0;
            this.btAddTable.Text = "Add";
            this.btAddTable.UseVisualStyleBackColor = true;
            this.btAddTable.Click += new System.EventHandler(this.btAddTable_Click);
            // 
            // dgvTables
            // 
            this.dgvTables.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTables.Location = new System.Drawing.Point(178, 92);
            this.dgvTables.Name = "dgvTables";
            this.dgvTables.Size = new System.Drawing.Size(452, 221);
            this.dgvTables.TabIndex = 3;
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(82, 95);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(88, 29);
            this.txtTableName.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 21);
            this.label3.TabIndex = 5;
            this.label3.Text = "Tên bàn:";
            // 
            // nudCapacity
            // 
            this.nudCapacity.Location = new System.Drawing.Point(82, 130);
            this.nudCapacity.Name = "nudCapacity";
            this.nudCapacity.Size = new System.Drawing.Size(90, 29);
            this.nudCapacity.TabIndex = 4;
            this.nudCapacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(-2, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 21);
            this.label4.TabIndex = 5;
            this.label4.Text = "Số người";
            // 
            // TableFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 407);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nudCapacity);
            this.Controls.Add(this.txtTableName);
            this.Controls.Add(this.dgvTables);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "TableFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TableFrm";
            this.Load += new System.EventHandler(this.TableFrm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCapacity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btDeleteTable;
        private System.Windows.Forms.Button btAddTable;
        private System.Windows.Forms.DataGridView dgvTables;
        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudCapacity;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Label label4;
    }
}