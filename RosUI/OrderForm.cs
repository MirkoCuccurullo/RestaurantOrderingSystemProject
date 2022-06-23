using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using RosLogic;
using RosModel;

namespace RosUI
{
    /// <summary>

    /// </summary>
    ////// Vedat Türk --- 683343 --- GROUP1- IT1D - ///////////////////////
    public partial class FormOrder : Form
    {
        Table table;
        DishLogic dishLogic;
        DrinkLogic drinkLogic;
        Order order;
        OrderLogic orderLogic;
        TableLogic tableLogic;
        List<Dish> dishesInOrderProcess; // For the orders that are in process (between order- will be ordered)
        List<Drink> drinkInOrderProcess; // For the orders that are in process (between order- will be ordered)
        RosMain rosMain;
        Employee emp;
        public FormOrder(Table table, Employee emp, RosMain rosMain)
        {
            InitializeComponent();
            this.rosMain = rosMain;
            this.emp = emp;
            this.table = table;
            order = new Order(emp, table);
            orderLogic = new OrderLogic();
            dishLogic = new DishLogic();
            drinkLogic = new DrinkLogic();
            tableLogic = new TableLogic();
            dishesInOrderProcess = new List<Dish>();
            drinkInOrderProcess = new List<Drink>();
            lblTableNumber.Text = $"{lblTableNumber.Text} {table.TableNumber.ToString()}";

            ReadContainedItems();
        }


        private void showPanel(string panelName)
        {
            HideListViews();
            switch (panelName)
            {
                case "Lunch":
                    listviewLunch.Show();
                    List<Dish> lunchMeals = dishLogic.GetDishes(1);
                    ReadFood(listviewLunch, lunchMeals);                              
                    break;
                case "Dinner":
                    listviewDinner.Show();
                    List<Dish> dinnerMeals = dishLogic.GetDishes(2);
                    ReadFood(listviewDinner, dinnerMeals);
                    break;
                case "Drinks":
                    listviewDrinks.Show();                
                    List<Drink> drinks = drinkLogic.GetAllDrinks();
                    ReadDrinks(listviewDrinks,drinks);                  
                    break;
            }
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
            HideListViews();
            ButtonColorReset();
        }

        private void ButtonColorReset()
        {
            btnLunch.BackColor = Color.LightSkyBlue;
            btnDrinks.BackColor = Color.LightSkyBlue;
            btnDinner.BackColor = Color.LightSkyBlue;
        }
        private void SendCancelNoteButtonsVisible()
        {
            btnSendOrder.Visible = true;
            btnCancelOrder.Visible = true;
            btnOrderAddNote.Visible = true;
            txtNote.Visible = true;
        }
        void HideListViews()
        {
            listviewLunch.Hide();
            listviewDinner.Hide();
            listviewDrinks.Hide();
        }
        private void ReadContainedItems() // Getting the ordered list
        {
            try
            {
                List<Dish> orderedDishes = dishLogic.ReadContainedDishes(table); // Read all ordered Dishes 
                List<Drink> orderedDrinks = drinkLogic.ReadContainedDrinks(table); // Read all ordered Drinks

                List<Item> itemsInOrder = new List<Item>();
                itemsInOrder.AddRange(orderedDishes); // Add Dishes to item list
                itemsInOrder.AddRange(orderedDrinks);// Add Drinks to item list
                listviewOrder.Items.Clear();

                foreach (Item item in itemsInOrder)
                {
                    ListViewItem lvItem = new ListViewItem(item.ItemName.ToString());
                    lvItem.SubItems.Add($"€ {item.ItemPrice}");
                    lvItem.SubItems.Add(item.ItemAmount.ToString());
                    if (item.NoteAmount >= 1)
                    {
                        lvItem.SubItems.Add("✓");
                    }
                    else
                    {
                        lvItem.SubItems.Add("");
                    }
                    lvItem.ForeColor = Color.Green; // Change color from previous orders
                    lvItem.Tag = (Item)item;
                    listviewOrder.Items.Add(lvItem);
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong!");
            }
        }
        private void ReadFood(ListView listview, List<Dish> dishes)
        {
            listview.Items.Clear();

            foreach (Dish dish in dishes)
            {
                ListViewItem lvItem = new ListViewItem(dish.ItemName.ToString());
                lvItem.SubItems.Add($"€ {dish.ItemPrice}");
                lvItem.SubItems.Add(dish.Course);
                lvItem.Tag = dish;
                listview.Items.Add(lvItem);
                if (listview.Items.IndexOf(lvItem) % 2 == 0)
                {
                    lvItem.BackColor = Color.FromArgb(224, 234, 255);
                }

                if (dish.ItemStock == 0)
                {
                    lvItem.Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Strikeout);
                }
            }
        }
        private void ReadDrinks(ListView listview, List<Drink> drinks)
        {
            listview.Items.Clear();

            foreach (Drink drink in drinks)
            {
                ListViewItem lvItem = new ListViewItem(drink.ItemName.ToString());
                lvItem.SubItems.Add($"€ {drink.ItemPrice}");
                lvItem.SubItems.Add(drink.DrinkCategory);
                lvItem.Tag = drink;
                listview.Items.Add(lvItem);

                if (listview.Items.IndexOf(lvItem) % 2 == 0)
                {
                    lvItem.BackColor = Color.FromArgb(224, 234, 255);
                }

                if (drink.ItemStock == 0)
                {
                   lvItem.Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Strikeout);
                }
            }
        }

        private void CleanSelectedItems(ListView[] lists)
        {
            foreach (ListView list in lists)
            {
                list.SelectedItems.Clear();
            }
        }

        private void btnLunch_Click(object sender, EventArgs e)
        {
            try
            {
                showPanel("Lunch");
                ButtonColorReset();
                CleanSelectedItems(new ListView[] {listviewDinner, listviewDrinks});
                btnOrderAddNote.Enabled = false;
                txtNote.Clear();
                btnLunch.BackColor = Color.LightGreen;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        private void btnDinner_Click(object sender, EventArgs e)
        {
            try
            {
                showPanel("Dinner");
                ButtonColorReset();
                CleanSelectedItems(new ListView[] { listviewLunch, listviewDrinks });
                btnOrderAddNote.Enabled = false;
                txtNote.Clear();
                btnDinner.BackColor = Color.LightGreen;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        private void btnDrinks_Click(object sender, EventArgs e)
        {
            try
            {
                showPanel("Drinks");
                ButtonColorReset();
                CleanSelectedItems(new ListView[] { listviewLunch, listviewDinner });
                btnOrderAddNote.Enabled = false;
                txtNote.Clear();
                btnDrinks.BackColor = Color.LightGreen;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void listviewLunch_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                CleanSelectedItems(new ListView[] { listviewOrder });
                Dish selectedDish = (Dish)listviewLunch.SelectedItems[0].Tag;
                if (selectedDish.ItemStock == 0)
                {
                    listviewLunch.SelectedItems[0].Selected = false;
                    throw new Exception("This item is sold out!");
                }
                btnOrderAddNote.Enabled = true;
                AddFood(selectedDish);
                SendCancelNoteButtonsVisible();
                txtNote.Clear();
            }
            catch (Exception exp)
            {
                MessageBox.Show($"{exp.Message}", "Sorry");
            }
        }

        private void listviewDinner_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                CleanSelectedItems(new ListView[] { listviewOrder });
                Dish selectedDish = (Dish)listviewDinner.SelectedItems[0].Tag;
                if (selectedDish.ItemStock == 0)
                {
                    listviewDinner.SelectedItems[0].Selected = false;
                    throw new Exception("This item is sold out!");
                }
                btnOrderAddNote.Enabled = true;
                AddFood(selectedDish);
                SendCancelNoteButtonsVisible();
                txtNote.Clear();
            }
            catch (Exception exp)
            {
                MessageBox.Show($"{exp.Message}", "Sorry");
            }
        }
        private void listviewDrinks_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                CleanSelectedItems(new ListView[] { listviewOrder });
                Drink selectedDrink = (Drink)listviewDrinks.SelectedItems[0].Tag;
                if (selectedDrink.ItemStock == 0)
                {
                    listviewDrinks.SelectedItems[0].Selected = false;
                    throw new Exception("This item is sold out!");
                }
                btnOrderAddNote.Enabled = true;
                AddDrink(selectedDrink);
                SendCancelNoteButtonsVisible();
                txtNote.Clear();
            }
            catch (Exception exp)
            {
                MessageBox.Show($"{exp.Message}", "Sorry");
            }
        }

        private void listviewOrder_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                CleanSelectedItems(new ListView[] {listviewDinner, listviewLunch, listviewDrinks});
                btnOrderAddNote.Enabled = false;

                if (listviewOrder.SelectedItems[0].ForeColor == Color.Green)
                {
                    listviewOrder.SelectedItems[0].Selected = false;
                    throw new Exception("This item is already ordered!");
                }
                if (listviewOrder.SelectedItems.Count == 1)
                {
                    Item item = (Item)listviewOrder.SelectedItems[0].Tag;
                    if (item is Dish)
                    {
                        RemoveItem((Dish)item); // Remove the dish from the list
                    }
                    else if (item is Drink)
                    {
                        RemoveItem((Drink)item); // Remove the drink from the list
                    }
                }

                txtNote.Clear();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void RemoveItem(Item item) // Remove the items one by one
        {
            if (item.ItemAmount == 1)
            {
                listviewOrder.Items.RemoveAt(listviewOrder.SelectedItems[0].Index);
            }
            else
            {
                item.ItemAmount--;
                listviewOrder.SelectedItems[0].SubItems[2].Text = item.ItemAmount.ToString();
            }
        }

        private void AddFood(Dish selectedDish)
        {
            ListViewItem? checkItem = null;
            Dish dish; // actual dish object

            foreach (ListViewItem lvItem in listviewOrder.Items)
            {
                if (lvItem.Tag is Dish)
                {
                    dish = (Dish)lvItem.Tag;
                    if (selectedDish.ItemID == dish.ItemID && lvItem.ForeColor != Color.Green)
                    {
                        checkItem = lvItem; //Not null anymore
                        dish.ItemAmount = int.Parse(lvItem.SubItems[2].Text);
                        dish.ItemAmount++; // Increase the amount of the existing dish in the listviewOrder
                        lvItem.SubItems[2].Text = dish.ItemAmount.ToString();
                    }
                }
            }

            // For new dish
            if (checkItem == null)
            {
                dish = selectedDish; // if the listviewOrder doesn't contain, then write
                ListViewItem item = new ListViewItem(dish.ItemName);
                item.SubItems.Add($"€ {dish.ItemPrice}");
                dish.ItemAmount = 1;
                item.SubItems.Add(dish.ItemAmount.ToString());
                item.SubItems.Add(""); // Note column
                item.Tag = dish; // Tagging to add the DishesInOrderProcess list
                item.ForeColor = Color.Red; // Change color for the new ordered item
                listviewOrder.Items.Add(item);
                listviewOrder.Items[listviewOrder.Items.Count - 1].EnsureVisible();
            }
        }
        private void AddDrink(Drink selectedDrink)
        {
            ListViewItem? checkItem = null;
            Drink drink; // actual drink object

            foreach (ListViewItem lvItem in listviewOrder.Items)
            {
                if (lvItem.Tag is Drink)
                {
                    drink = (Drink)lvItem.Tag;
                    if (selectedDrink.ItemID == drink.ItemID && lvItem.ForeColor != Color.Green)
                    {
                        checkItem = lvItem;
                        drink.ItemAmount = int.Parse(lvItem.SubItems[2].Text);
                        drink.ItemAmount++;
                        lvItem.SubItems[2].Text = drink.ItemAmount.ToString();
                    }
                }
            }

            if (checkItem == null)
            {
                drink = selectedDrink; // if the listviewOrder doesn't contain, then write
                ListViewItem item = new ListViewItem(drink.ItemName);
                item.SubItems.Add($"€ {drink.ItemPrice}");
                drink.ItemAmount = 1;
                item.SubItems.Add(drink.ItemAmount.ToString());
                item.SubItems.Add(""); // Note column
                item.Tag = drink; // Tagging to add the DrinksInOrderProcess list
                item.ForeColor = Color.Red; // Change color for the new ordered item
                listviewOrder.Items.Add(item);
                listviewOrder.Items[listviewOrder.Items.Count - 1].EnsureVisible(); // To see the last item for scrollbar
            }          
        }

        private void btnCancelOrder_Click(object sender, EventArgs e) // Clear the order list
        {
            try
            {
                if (listviewOrder.Items.Count == 0)
                {
                    throw new Exception($"{emp.Name}, you did not order anything!");
                }

                DialogResult dialogResult = MessageBox.Show("Do you want to cancel complete order?", "Cancel Order", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    CancelOrder();
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                CancelOrder();
                this.Close();
                new TableControl(emp, rosMain, table).Show();
            }

            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        private void CancelOrder() // Cancel the complete order
        {
            foreach (ListViewItem lvOrderInProcess in listviewOrder.Items) // Remove the items from order list
            {
                if (lvOrderInProcess.ForeColor == Color.Red)
                {
                    listviewOrder.Items.Remove(lvOrderInProcess);
                }
            }
        }
        private void btnOrderAddNote_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNote.Text == "")
                {
                    throw new Exception("Text box is empty!");
                }
                if (listviewLunch.SelectedItems.Count == 1)
                {
                    AddDishNote(listviewLunch);
                }
                else if (listviewDinner.SelectedItems.Count == 1)
                {
                    AddDishNote(listviewDinner);
                }
                else if (listviewDrinks.SelectedItems.Count == 1)
                {
                    Drink selectedDrink = (Drink)listviewDrinks.SelectedItems[0].Tag;                  

                    foreach (ListViewItem item in listviewOrder.Items)
                    {
                        if (item.Tag is Drink)
                        {
                            Drink drink = (Drink)item.Tag; // Tag the drink from listviewOrder
                            if (drink.ItemName == selectedDrink.ItemName && item.ForeColor != Color.Green)
                            {
                                drink.ItemNote = txtNote.Text;
                                item.SubItems[3].Text = "✓";
                            }
                        }
                    }
                    CleanSelectedItems(new ListView[] { listviewDrinks });
                    MessageBox.Show("Note has been added!");
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            finally
            {
                txtNote.Clear();
            }
        }
        private void AddDishNote(ListView menuListView)
        {
            Dish selectedDish = (Dish)menuListView.SelectedItems[0].Tag;

            foreach (ListViewItem item in listviewOrder.Items)
            {
                if (item.Tag is Dish && item.ForeColor != Color.Green)
                {
                    Dish dish = (Dish)item.Tag; // Tag the dish from listviewOrder
                    if (dish.ItemID == selectedDish.ItemID)
                    {
                        dish.ItemNote = txtNote.Text;
                        item.SubItems[3].Text = "✓";
                    }
                }
            }
            CleanSelectedItems(new ListView[] { menuListView });
            MessageBox.Show("Note has been added!");
        }
        private void btnSendOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (listviewOrder.Items.Count == 0) // Extra checking if the list is empty
                {
                    throw new Exception($"Sorry {emp.Name}, there is nothing to send");
                }
                DialogResult dialogResult = MessageBox.Show("Do you want to send this order?", "Send Order", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    order.OrderID = orderLogic.AddOrder(emp, table); // Create new order with waiterId and tableNumber                
                    SendOrder(); // adding the items in the listviewOrder to dish and drink list              
                    if (dishesInOrderProcess.Count > 0) // If the list is empty don't go to the logic
                    {
                        dishLogic.AddOrderedDishes(dishesInOrderProcess, order); // DishesInOrderProcess(comes from SendOrder) is the new ordered dishes 
                    }
                    if (drinkInOrderProcess.Count > 0) // If the list is empty don't go to the logic
                    {
                        drinkLogic.AddOrderedDrinks(drinkInOrderProcess, order);// DrinkInOrderProcess(comes from SendOrder) is the new ordered drinks
                    }                   
                    DecreaseStock(); // Decrease the stock for all new ordered items by their amounts

                    table.TableStatus = TableStatus.Standby; // Jason
                    tableLogic.Update(table); // Jason                    
                    rosMain.UpdateAllListViews(); //Update KitchenView and Barview
                    rosMain.UpdateTableToOrdered(table.TableNumber); //Update TableView

                    this.Close();
                    new TableControl(emp, rosMain, table).Show();
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show($"{exp.Message}", "Send Order");
            }
        }
        private void SendOrder()
        {
            for (int i = 0; i < listviewOrder.Items.Count; i++)
            {
                Item item = (Item)listviewOrder.Items[i].Tag; // Tag all the item as item in listview
                ListViewItem lvItem = listviewOrder.Items[i];

                if (lvItem.ForeColor == Color.Red && item is Dish)
                {
                    Dish dish = (Dish)item;
                    dishesInOrderProcess.Add(dish); // Adding to the dish list to add to the database (orderedDish)
                }
                else if (lvItem.ForeColor == Color.Red && item is Drink)
                {
                    Drink drink = (Drink)item;
                    drinkInOrderProcess.Add(drink); // Adding to the drink list to add to the database (orderedDrink)
                }
            }
        }
        private void DecreaseStock()
        {
            if (dishesInOrderProcess.Count > 0)
            {
                foreach (Dish dish in dishesInOrderProcess)
                {
                    Item item = (Item)dish;
                    orderLogic.DecreaseStock(item);
                }
            }
            if (drinkInOrderProcess.Count > 0)
            {
                foreach (Drink drink in drinkInOrderProcess)
                {
                    Item item = (Item)drink;
                    orderLogic.DecreaseStock(item);
                }
            }
        }
    }
}
