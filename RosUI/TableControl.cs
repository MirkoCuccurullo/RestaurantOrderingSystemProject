using System;
using System.Drawing;
using System.Windows.Forms;
using RosModel;
using RosLogic;
using System.Collections.Generic;

////////////////////Jason Xie, 659045, GROUP 1, IT1D/////////////////////////////////////////////////////////////////////////////////////////////

namespace RosUI
{
    public partial class TableControl : Form
    {
        private FormOrder orderForm;
        private Employee employee;
        private Table table;
        private RosMain rosMain;
        private TableLogic tableLogic;
        private TableOverview tableOverview;
        private OrderedDishLogic dishLogic;
        private OrderedDrinkLogic drinkLogic;
        private List<OrderedDish> dishes;
        private List<OrderedDrink> drinks;

        public TableControl(Employee employee, RosMain rosMain, Table table)
        {
            InitializeComponent();
            this.employee = employee;
            this.rosMain = rosMain;
            this.table = table;
            tableLogic = new TableLogic();
            dishLogic = new OrderedDishLogic();
            drinkLogic = new OrderedDrinkLogic();
            dishes = dishLogic.GetAllOrderedDishByTable(table);
            drinks = drinkLogic.GetAllOrderedDrinksByTable(table);
            ShowAllDishes(dishes);
            ShowAllDrinks(drinks);
            orderForm = new FormOrder(table, employee, rosMain);
            tableOverview = new TableOverview(employee, rosMain);
            lblTable.Text = "Table: " + table.TableNumber;
            lblWaiter.Text = "Waiter: " + employee.Name;
            FillComboBox();
            cmbStatus.SelectedIndex = 0;
            UpdateOccupyButton(table, btnOccupy);
        }

        //fills in the combobox
        private void FillComboBox()
        {
            cmbStatus.Items.Add("All");
            cmbStatus.Items.Add("Running Dish");
            cmbStatus.Items.Add("Running Drink");
            cmbStatus.Items.Add("Dish Ready");
            cmbStatus.Items.Add("Drink Ready");
            cmbStatus.Items.Add("Served");
        }

        //sorts the items according to the type that is selected
        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            SortItems(dishes, drinks);
        }

        //displays the items according to the combobox
        private void SortItems(List<OrderedDish> dishes, List<OrderedDrink> drinks)
        {
            lvOrders.Items.Clear();
            switch (cmbStatus.Text)
            {
                case "All":
                    ShowAllDishes(dishes);
                    ShowAllDrinks(drinks);
                    break;
                case "Running Dish":
                    ShowDishes(dishes, DishStatus.ToPrepare, "Preparing...", Color.LightBlue);
                    break;
                case "Running Drink":
                    ShowDrinks(drinks, DrinkStatus.ToPrepare, "Preparing...", Color.LightBlue);
                    break;
                case "Dish Ready":
                    ShowDishes(dishes, DishStatus.PickUp, "Dish Ready", Color.LightGreen);
                    break;
                case "Drink Ready":
                    ShowDrinks(drinks, DrinkStatus.PickUp, "Drink Ready", Color.LightGreen);
                    break;
                case "Served":
                    ShowServedItems(dishes, drinks);
                    break;
            }
        }
        
        //show all the dishes in the list view
        private void ShowAllDishes(List<OrderedDish> dishes)
        {
            foreach (OrderedDish dish in dishes)
            { 
                if (dish.Status == DishStatus.ToPrepare)
                {
                    AddDishesToListView(dish, "Preparing...", Color.LightBlue);
                }
                else if (dish.Status == DishStatus.PickUp)
                {
                    AddDishesToListView(dish, "Dish Ready", Color.LightGreen);
                }
                else if (dish.Status == DishStatus.Serve)
                {
                    AddDishesToListView(dish, "Served", Color.Yellow);
                }
                else
                    return;
            }
        }

        //show all the drinks in the list view
        private void ShowAllDrinks(List<OrderedDrink> drinks)
        {
            foreach (OrderedDrink drink in drinks)
            {
                if (drink.DrinkStatus == DrinkStatus.ToPrepare)
                {
                    AddDrinksToListView(drink, "Preparing...", Color.LightBlue);
                }
                else if (drink.DrinkStatus == DrinkStatus.PickUp)
                {
                    AddDrinksToListView(drink, "Drink Ready", Color.LightGreen);
                }
                else if (drink.DrinkStatus == DrinkStatus.Serve)
                {
                    AddDrinksToListView(drink, "Served", Color.Yellow);
                }
                else
                    return;
            }
        }

        //show all the preparing drinks in the list view
        private void ShowDrinks(List<OrderedDrink> drinks, DrinkStatus status, string text, Color color)
        {
            foreach (OrderedDrink drink in drinks)
            {
                if (drink.DrinkStatus == status)
                {
                    AddDrinksToListView(drink, text, color);
                }
            }
        }

        //show all the dishes that are ready in the list view
        private void ShowDishes(List<OrderedDish> dishes, DishStatus status, string text, Color color)
        {
            foreach (OrderedDish dish in dishes)
            {
                if (dish.Status == status)
                {
                    AddDishesToListView(dish, text, color);
                }
            }
        }

        //show all the items that are served in the list view
        private void ShowServedItems(List<OrderedDish> dishes, List<OrderedDrink> drinks)
        {
            foreach (OrderedDish dish in dishes)
            {
                if (dish.Status == DishStatus.Serve)
                {
                    AddDishesToListView(dish, "Served", Color.Yellow);
                }               
            }

            foreach (OrderedDrink drink in drinks)
            {
                if (drink.DrinkStatus == DrinkStatus.Serve)
                {
                    AddDrinksToListView(drink, "Served", Color.Yellow);
                }
            }
        }

        //adds dishes to the list view
        private void AddDishesToListView(OrderedDish dish, string text, Color color)
        {
            ListViewItem lvItem = new ListViewItem(dish.ItemName.ToString());
            lvItem.SubItems.Add(dish.TimeDishOrdered.ToShortTimeString());
            lvItem.SubItems.Add(text);
            lvItem.BackColor = color;
            lvItem.SubItems.Add(dish.OrderedDishAmount.ToString());
            lvOrders.Items.Add(lvItem);
        }

        //adds drinks to the listview
        private void AddDrinksToListView(OrderedDrink drink, string text, Color color)
        {
            ListViewItem lvItem = new ListViewItem(drink.ItemName.ToString());
            lvItem.SubItems.Add(drink.TimeDrinkOrdered.ToShortTimeString());
            lvItem.SubItems.Add(text);
            lvItem.BackColor = color;
            lvItem.SubItems.Add(drink.OrderedDrinkAmount.ToString());
            lvOrders.Items.Add(lvItem);
        }

        //When Clicked will Occupy and unOccupy the tables
        private void btnOccupy_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnOccupy.Text == "Empty")
                {
                    btnOccupy.Text = "Occupied";
                    btnOccupy.BackColor = Color.Red;
                    btnOccupy.ForeColor = Color.White;
                    table.TableStatus = TableStatus.Occupied;
                    table.WaiterID = employee.EmplID;
                    tableLogic.UpdateTableWaiter(table);
                }
                else if (btnOccupy.Text == "Occupied")
                {
                    btnOccupy.Text = "Empty";
                    btnOccupy.BackColor = Color.LightGray;
                    btnOccupy.ForeColor = Color.Black;
                    table.TableStatus = TableStatus.Empty;
                    tableLogic.Update(table);
                }
                else
                {
                    return;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error");
            }                     
        }

        //Will take you to the OrderFrom
        private void btnTakeOrder_Click(object sender, EventArgs e)
        {
            try
            {
                table = tableLogic.GetTableById(table.TableNumber);
                orderForm = new FormOrder(table, employee, rosMain);
                this.Close();
                orderForm.Show();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error");
            }
        }

        //Will take you to the PaymentForm
        private void btnPay_Click(object sender, EventArgs e)
        {
            try
            {
                FormPayment formPayment = new FormPayment(table, employee, rosMain);
                formPayment.Show();
                this.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error");
            }
        }

        //Goes back to the TableOverview
        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                new TableOverview(employee, rosMain).Show();
                this.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error");
            }
        }

        //Serves the dishes oredered by the table and update the kitchen view
        private void btnDishServed_Click(object sender, EventArgs e)
        {
            try
            {           
                tableOverview.GetAllOrderedDishes(table);
                UpdateButtonAndListViews();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }          
        }

        //Serves the drinks oredered by the table and update the bar view
        private void btnDrinkServed_Click(object sender, EventArgs e)
        {
            try
            {             
                tableOverview.GetAllOrderedDrinks(table);
                UpdateButtonAndListViews();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            
        }

        private void UpdateButtonAndListViews()
        {
            tableOverview.CheckOrderedItems(table);
            UpdateOccupyButton(table, btnOccupy);
            rosMain.UpdateAllListViews();
        }

        //updates the occupy button.
        private void UpdateOccupyButton(Table table, Button button)
        {
            Label l = new Label();
            PictureBox pb = new PictureBox();
            PictureBox pb2 = new PictureBox();
            button.Enabled = false;
            tableOverview.UpdateButtonColor(table, button, l, pb, pb2);
            if (button.Text == "Empty" || button.Text == "Occupied")
            {
                button.Enabled = true;
            }               
        }

        //updates the list view and the button occupy
        private void tmrTimer_Tick(object sender, EventArgs e)
        {
            lvOrders.Items.Clear();
            dishes = dishLogic.GetAllOrderedDishByTable(table);
            drinks = drinkLogic.GetAllOrderedDrinksByTable(table);
            
            SortItems(dishes, drinks);
            UpdateOccupyButton(table, btnOccupy);
            return;
        }
    }
}
