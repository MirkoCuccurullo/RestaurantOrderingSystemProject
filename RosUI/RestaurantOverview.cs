﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RosModel;

namespace RosUI
{
    public partial class RestaurantOverview : Form
    {
        private Table table;
        private Employee employee;
        private RosMain rosMain;

        public RestaurantOverview()
        {
            InitializeComponent();
        }

        private void btnTableOne_Click(object sender, EventArgs e)
        {
            table = new Table(1);
            FormOrder orderForm = new FormOrder(table, employee, rosMain);

            orderForm.ShowDialog();
        }

        private void btnTableTwo_Click(object sender, EventArgs e)
        {
            table = new Table(2);
            FormOrder orderForm = new FormOrder(table, employee, rosMain);

            orderForm.ShowDialog();
        }

        private void btnTableThree_Click(object sender, EventArgs e)
        {
            table = new Table(3);
            FormOrder orderForm = new FormOrder(table, employee, rosMain);

            orderForm.ShowDialog();
        }

        private void btnTableFour_Click(object sender, EventArgs e)
        {
            table = new Table(4);
            FormOrder orderForm = new FormOrder(table, employee, rosMain);

            orderForm.ShowDialog();
        }

        private void btnTableFive_Click(object sender, EventArgs e)
        {
            table = new Table(5);
            FormOrder orderForm = new FormOrder(table, employee, rosMain);

            orderForm.ShowDialog();
        }

        private void btnTableSix_Click(object sender, EventArgs e)
        {
            table = new Table(6);
            FormOrder orderForm = new FormOrder(table, employee, rosMain);

            orderForm.ShowDialog();
        }

        private void btnTableSeven_Click(object sender, EventArgs e)
        {
            table = new Table(7);
            FormOrder orderForm = new FormOrder(table, employee, rosMain);

            orderForm.ShowDialog();
        }

        private void btnTableEight_Click(object sender, EventArgs e)
        {
            table = new Table(8);
            FormOrder orderForm = new FormOrder(table, employee, rosMain);

            orderForm.ShowDialog();
        }

        private void btnTableNine_Click(object sender, EventArgs e)
        {
            table = new Table(9);
            FormOrder orderForm = new FormOrder(table, employee, rosMain);

            orderForm.ShowDialog();
        }

        private void btnTableTen_Click(object sender, EventArgs e)
        {
            table = new Table(10);
            FormOrder orderForm = new FormOrder(table, employee, rosMain);

            orderForm.ShowDialog();
        }
        public void OrderRecieved(int number)
        {
            //Changing color of buttons when Order is initiated
            switch (number)
            {
                case 1:
                    btnTableOne.BackColor = Color.Blue;
                    break;

                case 2:
                    btnTableTwo.BackColor = Color.Blue;

                    break;
                case 3:
                    btnTableThree.BackColor = Color.Blue;

                    break;
                case 4:
                    btnTableFour.BackColor = Color.Blue;

                    break;
                case 5:
                    btnTableFive.BackColor = Color.Blue;

                    break;
                case 6:
                    btnTableSix.BackColor = Color.Blue;

                    break;
                case 7:
                    btnTableSeven.BackColor = Color.Blue;

                    break;
                case 8:
                    btnTableEight.BackColor = Color.Blue;

                    break;
                case 9:
                    btnTableNine.BackColor = Color.Blue;

                    break;
                case 10:
                    btnTableTen.BackColor = Color.Blue;

                    break;
            }
        }

        public void PickUpReady(int number)
        {
            //changing color of buttons when order is ready for the pick up 
            switch (number)
            {
                case 1:
                    btnTableOne.BackColor = Color.Green;
                    break;

                case 2:
                    btnTableTwo.BackColor = Color.Green;

                    break;
                case 3:
                    btnTableThree.BackColor = Color.Green;

                    break;
                case 4:
                    btnTableFour.BackColor = Color.Green;

                    break;
                case 5:
                    btnTableFive.BackColor = Color.Green;

                    break;
                case 6:
                    btnTableSix.BackColor = Color.Green;

                    break;
                case 7:
                    btnTableSeven.BackColor = Color.Green;

                    break;
                case 8:
                    btnTableEight.BackColor = Color.Green;

                    break;
                case 9:
                    btnTableNine.BackColor = Color.Green;

                    break;
                case 10:
                    btnTableTen.BackColor = Color.Green;

                    break;
            }
        }
        public void ItemServed(int number)
        {
            //changing color of buttons when order is ready for the pick up 
            switch (number)
            {
                case 1:
                    btnTableOne.BackColor = Color.Yellow;
                    break;

                case 2:
                    btnTableTwo.BackColor = Color.Yellow;

                    break;
                case 3:
                    btnTableThree.BackColor = Color.Yellow;

                    break;
                case 4:
                    btnTableFour.BackColor = Color.Yellow;

                    break;
                case 5:
                    btnTableFive.BackColor = Color.Yellow;

                    break;
                case 6:
                    btnTableSix.BackColor = Color.Yellow;

                    break;
                case 7:
                    btnTableSeven.BackColor = Color.Yellow;

                    break;
                case 8:
                    btnTableEight.BackColor = Color.Yellow;

                    break;
                case 9:
                    btnTableNine.BackColor = Color.Yellow;

                    break;
                case 10:
                    btnTableTen.BackColor = Color.Yellow;

                    break;
            }
        }
    }
}