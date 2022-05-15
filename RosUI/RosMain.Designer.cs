﻿namespace RosUI
{
    partial class RosMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.applicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dashboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.barViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kitchenViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlDashboard = new System.Windows.Forms.Panel();
            this.pnlKitchenView = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDishReady = new System.Windows.Forms.Button();
            this.lvOrderedDishes = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.pnlBarView = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.brnDrinkReady = new System.Windows.Forms.Button();
            this.lvOrderedDrinks = new System.Windows.Forms.ListView();
            this.clTable = new System.Windows.Forms.ColumnHeader();
            this.clDrinks = new System.Windows.Forms.ColumnHeader();
            this.clOrderTime = new System.Windows.Forms.ColumnHeader();
            this.pnlTableView = new System.Windows.Forms.Panel();
            this.btnTableOne = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.pnlKitchenView.SuspendLayout();
            this.pnlBarView.SuspendLayout();
            this.pnlTableView.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applicationToolStripMenuItem,
            this.barViewToolStripMenuItem,
            this.kitchenViewToolStripMenuItem,
            this.tableViewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1216, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // applicationToolStripMenuItem
            // 
            this.applicationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dashboardToolStripMenuItem,
            this.logoutToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.applicationToolStripMenuItem.Name = "applicationToolStripMenuItem";
            this.applicationToolStripMenuItem.Size = new System.Drawing.Size(100, 24);
            this.applicationToolStripMenuItem.Text = "Application";
            // 
            // dashboardToolStripMenuItem
            // 
            this.dashboardToolStripMenuItem.Name = "dashboardToolStripMenuItem";
            this.dashboardToolStripMenuItem.Size = new System.Drawing.Size(165, 26);
            this.dashboardToolStripMenuItem.Text = "Dashboard";
            this.dashboardToolStripMenuItem.Click += new System.EventHandler(this.dashboardToolStripMenuItem_Click);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(165, 26);
            this.logoutToolStripMenuItem.Text = "Logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(165, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // barViewToolStripMenuItem
            // 
            this.barViewToolStripMenuItem.Name = "barViewToolStripMenuItem";
            this.barViewToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.barViewToolStripMenuItem.Text = "Bar View";
            this.barViewToolStripMenuItem.Click += new System.EventHandler(this.barViewToolStripMenuItem_Click);
            // 
            // kitchenViewToolStripMenuItem
            // 
            this.kitchenViewToolStripMenuItem.Name = "kitchenViewToolStripMenuItem";
            this.kitchenViewToolStripMenuItem.Size = new System.Drawing.Size(106, 24);
            this.kitchenViewToolStripMenuItem.Text = "Kitchen view";
            this.kitchenViewToolStripMenuItem.Click += new System.EventHandler(this.kitchenViewToolStripMenuItem_Click);
            // 
            // tableViewToolStripMenuItem
            // 
            this.tableViewToolStripMenuItem.Name = "tableViewToolStripMenuItem";
            this.tableViewToolStripMenuItem.Size = new System.Drawing.Size(92, 24);
            this.tableViewToolStripMenuItem.Text = "Table view";
            this.tableViewToolStripMenuItem.Click += new System.EventHandler(this.tableViewToolStripMenuItem_Click);
            // 
            // pnlDashboard
            // 
            this.pnlDashboard.Location = new System.Drawing.Point(0, 31);
            this.pnlDashboard.Name = "pnlDashboard";
            this.pnlDashboard.Size = new System.Drawing.Size(1216, 578);
            this.pnlDashboard.TabIndex = 1;
            // 
            // pnlKitchenView
            // 
            this.pnlKitchenView.Controls.Add(this.label1);
            this.pnlKitchenView.Controls.Add(this.btnDishReady);
            this.pnlKitchenView.Controls.Add(this.lvOrderedDishes);
            this.pnlKitchenView.Location = new System.Drawing.Point(0, 28);
            this.pnlKitchenView.Name = "pnlKitchenView";
            this.pnlKitchenView.Size = new System.Drawing.Size(1216, 578);
            this.pnlKitchenView.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(536, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Kitchen View";
            // 
            // btnDishReady
            // 
            this.btnDishReady.Location = new System.Drawing.Point(1031, 518);
            this.btnDishReady.Name = "btnDishReady";
            this.btnDishReady.Size = new System.Drawing.Size(149, 30);
            this.btnDishReady.TabIndex = 3;
            this.btnDishReady.Text = "Ready!";
            this.btnDishReady.UseVisualStyleBackColor = true;
            this.btnDishReady.Click += new System.EventHandler(this.btnDishReady_Click);
            // 
            // lvOrderedDishes
            // 
            this.lvOrderedDishes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvOrderedDishes.FullRowSelect = true;
            this.lvOrderedDishes.HideSelection = false;
            this.lvOrderedDishes.Location = new System.Drawing.Point(43, 58);
            this.lvOrderedDishes.Name = "lvOrderedDishes";
            this.lvOrderedDishes.Size = new System.Drawing.Size(1137, 428);
            this.lvOrderedDishes.TabIndex = 0;
            this.lvOrderedDishes.UseCompatibleStateImageBehavior = false;
            this.lvOrderedDishes.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Table";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Dishes";
            this.columnHeader2.Width = 800;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Order Time";
            this.columnHeader3.Width = 200;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Course";
            // 
            // pnlBarView
            // 
            this.pnlBarView.Controls.Add(this.label2);
            this.pnlBarView.Controls.Add(this.brnDrinkReady);
            this.pnlBarView.Controls.Add(this.lvOrderedDrinks);
            this.pnlBarView.Location = new System.Drawing.Point(0, 34);
            this.pnlBarView.Name = "pnlBarView";
            this.pnlBarView.Size = new System.Drawing.Size(1216, 572);
            this.pnlBarView.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(536, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Bar View";
            // 
            // brnDrinkReady
            // 
            this.brnDrinkReady.Location = new System.Drawing.Point(1031, 518);
            this.brnDrinkReady.Name = "brnDrinkReady";
            this.brnDrinkReady.Size = new System.Drawing.Size(149, 30);
            this.brnDrinkReady.TabIndex = 3;
            this.brnDrinkReady.Text = "Ready!";
            this.brnDrinkReady.UseVisualStyleBackColor = true;
            this.brnDrinkReady.Click += new System.EventHandler(this.btnDrinkReady_Click);
            // 
            // lvOrderedDrinks
            // 
            this.lvOrderedDrinks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clTable,
            this.clDrinks,
            this.clOrderTime});
            this.lvOrderedDrinks.FullRowSelect = true;
            this.lvOrderedDrinks.HideSelection = false;
            this.lvOrderedDrinks.Location = new System.Drawing.Point(43, 58);
            this.lvOrderedDrinks.Name = "lvOrderedDrinks";
            this.lvOrderedDrinks.Size = new System.Drawing.Size(1137, 428);
            this.lvOrderedDrinks.TabIndex = 0;
            this.lvOrderedDrinks.UseCompatibleStateImageBehavior = false;
            this.lvOrderedDrinks.View = System.Windows.Forms.View.Details;
            // 
            // clTable
            // 
            this.clTable.Text = "Table";
            // 
            // clDrinks
            // 
            this.clDrinks.Text = "Drinks";
            this.clDrinks.Width = 800;
            // 
            // clOrderTime
            // 
            this.clOrderTime.Text = "Order Time";
            this.clOrderTime.Width = 200;
            // 
            // pnlTableView
            // 
            this.pnlTableView.Controls.Add(this.btnTableOne);
            this.pnlTableView.Location = new System.Drawing.Point(0, 28);
            this.pnlTableView.Name = "pnlTableView";
            this.pnlTableView.Size = new System.Drawing.Size(1216, 567);
            this.pnlTableView.TabIndex = 4;
            // 
            // btnTableOne
            // 
            this.btnTableOne.Location = new System.Drawing.Point(147, 67);
            this.btnTableOne.Name = "btnTableOne";
            this.btnTableOne.Size = new System.Drawing.Size(94, 29);
            this.btnTableOne.TabIndex = 0;
            this.btnTableOne.Text = "Table 1";
            this.btnTableOne.UseVisualStyleBackColor = true;
            this.btnTableOne.Click += new System.EventHandler(this.btnTableOne_Click);
            // 
            // RosMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 607);
            this.Controls.Add(this.pnlKitchenView);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pnlTableView);
            this.Controls.Add(this.pnlBarView);
            this.Controls.Add(this.pnlDashboard);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "RosMain";
            this.Text = "Ordering System";
            this.Load += new System.EventHandler(this.RosMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlKitchenView.ResumeLayout(false);
            this.pnlKitchenView.PerformLayout();
            this.pnlBarView.ResumeLayout(false);
            this.pnlBarView.PerformLayout();
            this.pnlTableView.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem applicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dashboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem barViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kitchenViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tableViewToolStripMenuItem;
        private System.Windows.Forms.Panel pnlDashboard;
        private System.Windows.Forms.Panel pnlKitchenView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDishReady;
        private System.Windows.Forms.ListView lvOrderedDishes;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Panel pnlBarView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button brnDrinkReady;
        private System.Windows.Forms.ListView lvOrderedDrinks;
        private System.Windows.Forms.ColumnHeader clTable;
        private System.Windows.Forms.ColumnHeader clDrinks;
        private System.Windows.Forms.ColumnHeader clOrderTime;
        private System.Windows.Forms.Panel pnlTableView;
        private System.Windows.Forms.Button btnTableOne;
    }
}