namespace CoffeeShopSystem.DashBoard
{
    partial class DashBoardForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btLogout = new System.Windows.Forms.Button();
            this.Order = new System.Windows.Forms.Button();
            this.btTable = new System.Windows.Forms.Button();
            this.btStaff = new System.Windows.Forms.Button();
            this.btHome = new System.Windows.Forms.Button();
            this.picCoffee = new System.Windows.Forms.PictureBox();
            this.lbWelcome = new System.Windows.Forms.Label();
            this.pnLoadForm = new System.Windows.Forms.Panel();
            this.btProducts = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCoffee)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.btLogout);
            this.panel1.Controls.Add(this.btProducts);
            this.panel1.Controls.Add(this.Order);
            this.panel1.Controls.Add(this.btTable);
            this.panel1.Controls.Add(this.btStaff);
            this.panel1.Controls.Add(this.btHome);
            this.panel1.Controls.Add(this.picCoffee);
            this.panel1.Controls.Add(this.lbWelcome);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(163, 527);
            this.panel1.TabIndex = 0;
            // 
            // btLogout
            // 
            this.btLogout.Location = new System.Drawing.Point(3, 482);
            this.btLogout.Name = "btLogout";
            this.btLogout.Size = new System.Drawing.Size(157, 42);
            this.btLogout.TabIndex = 2;
            this.btLogout.Text = "Logout";
            this.btLogout.UseVisualStyleBackColor = true;
            this.btLogout.Click += new System.EventHandler(this.btLogout_Click);
            // 
            // Order
            // 
            this.Order.Location = new System.Drawing.Point(3, 306);
            this.Order.Name = "Order";
            this.Order.Size = new System.Drawing.Size(157, 42);
            this.Order.TabIndex = 2;
            this.Order.Text = "Order";
            this.Order.UseVisualStyleBackColor = true;
            this.Order.Click += new System.EventHandler(this.Order_Click);
            // 
            // btTable
            // 
            this.btTable.Location = new System.Drawing.Point(3, 258);
            this.btTable.Name = "btTable";
            this.btTable.Size = new System.Drawing.Size(157, 42);
            this.btTable.TabIndex = 2;
            this.btTable.Text = "Table";
            this.btTable.UseVisualStyleBackColor = true;
            this.btTable.Click += new System.EventHandler(this.btTable_Click);
            // 
            // btStaff
            // 
            this.btStaff.Location = new System.Drawing.Point(3, 212);
            this.btStaff.Name = "btStaff";
            this.btStaff.Size = new System.Drawing.Size(157, 42);
            this.btStaff.TabIndex = 2;
            this.btStaff.Text = "Staff";
            this.btStaff.UseVisualStyleBackColor = true;
            this.btStaff.Click += new System.EventHandler(this.btStaff_Click);
            // 
            // btHome
            // 
            this.btHome.Location = new System.Drawing.Point(3, 164);
            this.btHome.Name = "btHome";
            this.btHome.Size = new System.Drawing.Size(157, 42);
            this.btHome.TabIndex = 2;
            this.btHome.Text = "Home";
            this.btHome.UseVisualStyleBackColor = true;
            this.btHome.Click += new System.EventHandler(this.btHome_Click);
            // 
            // picCoffee
            // 
            this.picCoffee.Location = new System.Drawing.Point(30, 3);
            this.picCoffee.Name = "picCoffee";
            this.picCoffee.Size = new System.Drawing.Size(100, 100);
            this.picCoffee.TabIndex = 1;
            this.picCoffee.TabStop = false;
            // 
            // lbWelcome
            // 
            this.lbWelcome.BackColor = System.Drawing.Color.White;
            this.lbWelcome.Location = new System.Drawing.Point(3, 106);
            this.lbWelcome.Name = "lbWelcome";
            this.lbWelcome.Size = new System.Drawing.Size(157, 45);
            this.lbWelcome.TabIndex = 0;
            this.lbWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnLoadForm
            // 
            this.pnLoadForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnLoadForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnLoadForm.Location = new System.Drawing.Point(163, 0);
            this.pnLoadForm.Name = "pnLoadForm";
            this.pnLoadForm.Size = new System.Drawing.Size(670, 527);
            this.pnLoadForm.TabIndex = 1;
            // 
            // btProducts
            // 
            this.btProducts.Location = new System.Drawing.Point(3, 354);
            this.btProducts.Name = "btProducts";
            this.btProducts.Size = new System.Drawing.Size(157, 42);
            this.btProducts.TabIndex = 2;
            this.btProducts.Text = "Product";
            this.btProducts.UseVisualStyleBackColor = true;
            this.btProducts.Click += new System.EventHandler(this.btProducts_Click);
            // 
            // DashBoardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 527);
            this.Controls.Add(this.pnLoadForm);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "DashBoardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DashBoardForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DashBoardForm_FormClosing);
            this.Load += new System.EventHandler(this.DashBoardForm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCoffee)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnLoadForm;
        private System.Windows.Forms.PictureBox picCoffee;
        private System.Windows.Forms.Label lbWelcome;
        private System.Windows.Forms.Button btLogout;
        private System.Windows.Forms.Button Order;
        private System.Windows.Forms.Button btTable;
        private System.Windows.Forms.Button btStaff;
        private System.Windows.Forms.Button btHome;
        private System.Windows.Forms.Button btProducts;
    }
}