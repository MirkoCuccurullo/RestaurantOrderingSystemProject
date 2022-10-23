using RosModel;
using System;
using RosLogic;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

////////////////////Mirko Cuccurullo, 691362, GROUP 1, IT1D/////////////////////////////////////////////////////////////////////////////////////////////

namespace RosUI
{
    public partial class RosMain : Form
    {
        const int NumberOfTables = 10;

        Employee employee;
        OrderedDishLogic dishLogic = new OrderedDishLogic();
        OrderedDrinkLogic drinkLogic = new OrderedDrinkLogic();
        TableLogic tableLogic = new TableLogic();
        List<TableOverview> tableOverview;
        
        public RosMain(Employee employee)
        {
            InitializeComponent();
            InitialiseComboBoxes();
            UpdateAllListViews();

            this.employee = employee;

            InitialiseLablesWithNames();

            tableOverview = new List<TableOverview>();

            AdaptFormOnRole(employee);
        }

        private void InitialiseLablesWithNames()
        {
            lblEmpNameBar.Text += $"{employee.Name}";
            lblEmpNameBarFin.Text += $"{employee.Name}";
            lblEmpNameKit.Text += $"{employee.Name}";
            lblEmpNameKitFin.Text += $"{employee.Name}";
        }

        private void InitialiseComboBoxes()
        {
            //combo table kitchen view/ finished order combo
            for (int i = 1; i <= NumberOfTables; i++)
            {
                cmbTableKit.Items.Add($"Table {i}");
                cmbKitFinished.Items.Add($"Table {i}");
                cmbBarFinished.Items.Add($"Table {i}");
                cmbSelByTabBar.Items.Add($"Table {i}");
            }

            cmbTableKit.SelectedIndex = 0;
            cmbKitFinished.SelectedIndex = 0;
            cmbSelByTabBar.SelectedIndex = 0;
            cmbBarFinished.SelectedIndex = 0;

            //combo courses kitchen view
            cmbCourseKit.Items.Add("Starter");
            cmbCourseKit.Items.Add("Main");
            cmbCourseKit.Items.Add("Dessert");

            cmbCourseKit.SelectedIndex = 0;

        }

        public void UpdateAllListViews()
        {
            UpdateDishes();
            UpdateDrinks();
            UpdateFinishedDrinks();
            UpdateFinishedDishes();
        }

        private void AdaptFormOnRole(Employee employee)
        {
            if (employee.Roles == Roles.Manager)
            {
                ShowPanel("KitchenView");
            }
            else if (employee.Roles == Roles.Chef)
            {
                barViewToolStripMenuItem.Visible = false;
                tableViewToolStripMenuItem.Visible = false;
                btnUndoKitFin.Enabled = false;
                ShowPanel("KitchenView");

            }
            else if (employee.Roles == Roles.Bartender)
            {
                kitchenViewToolStripMenuItem.Visible = false;
                tableViewToolStripMenuItem.Visible = false;
                btnUndoKitFin.Enabled = false;
                ShowPanel("BarView");
            }
        }

        private void ShowPanel(string panelName)
        {
            //shwitch between the different panels
            switch (panelName)
            {
                case "KitchenView":

                    HideAllPanels();
                    pnlKitchenView.Show();
                    UpdateDishes();

                    break;

                case "BarView":

                    HideAllPanels();
                    pnlBarView.Show();
                    UpdateDrinks();

                    break;

                case "FinishedDrinkView":

                    HideAllPanels();
                    pnlBarViewFinished.Show();
                    UpdateFinishedDrinks();

                    break;

                case "FinishedDishView":

                    HideAllPanels();
                    pnlKitchenViewFinished.Show();
                    UpdateFinishedDishes();

                    break;
            }

        }

        private void HideAllPanels()
        {
            //hiding all panels
            pnlKitchenView.Hide();
            pnlBarView.Hide();
            pnlKitchenViewFinished.Hide();
            pnlBarViewFinished.Hide();

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            new Login().Show();
        }

        private void tableViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TableOverview(employee, this).Show();
        }

        //calculates the timespan in minutes (for calculations)
        private int CalculateTimeSpentInMinutes(DateTime timeDrinkOrdered)
        {
            TimeSpan ts = DateTime.Now - timeDrinkOrdered;
            return (int)ts.TotalMinutes;
        }

        //returns a given time in minutes or hour ago string
        private string ReturnTimeSpentAsString(DateTime placed)
        {
            TimeSpan ts = DateTime.Now - placed;

            if (ts.TotalMinutes > 60)
            {
                return $"{ts.TotalHours:00} hours ago";
            }
            else
            {
                return $"{ts.TotalMinutes:00} minutes ago";
            }

        }

        public void UpdateDrinks()
        {
            try
            {
                //retrieveing all ordered drinks
                List<OrderedDrink> orderedDrinks = drinkLogic.GetAllOrderedDrinks();

                //clearing preavious items
                lvOrderedDrinks.Items.Clear();

                //checking each item in the drinkList
                foreach (OrderedDrink drink in orderedDrinks)
                {
                    ListViewItem li = new ListViewItem("");
                    li.SubItems.Add(drink.OrderedDrinkAmount.ToString());
                    li.UseItemStyleForSubItems = false;
                    li.SubItems.Add(drink.ItemName);

                    //displaying output according if a note is present
                    if (drink.ItemNote == "null")
                    {
                        li.SubItems.Add("No");
                    }
                    else
                    {
                        li.SubItems.Add("Yes");

                        //if a note is present highlight it in light blue
                        li.SubItems[3].BackColor = Color.LightSkyBlue;
                    }

                    li.SubItems.Add(ReturnTimeSpentAsString(drink.TimeDrinkOrdered));

                    //if the time is 1 hour or more, hilight the time in red
                    if (CalculateTimeSpentInMinutes(drink.TimeDrinkOrdered) >= 60)
                    {
                        li.SubItems[4].BackColor = Color.Red;
                    }

                    li.SubItems.Add(drink.TableNumber.ToString());

                    li.Tag = drink;

                    //if the item is ready for pickup higlight it in green
                    if (drink.DrinkStatus == DrinkStatus.PickUp)
                    {
                        li.UseItemStyleForSubItems = true;
                        li.BackColor = Color.LightGreen;
                    }

                    //adding item to the list
                    lvOrderedDrinks.Items.Add(li);
                }
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message, "Error");
            }
        }

        public void UpdateFinishedDrinks()
        {
            try
            {
               //retrieveing all finished drinks

                List<OrderedDrink> finishedDrinks = drinkLogic.GetAllFinishedDrinks();

                //claring preavious items
                lvFinishedDrinks.Items.Clear();

                //checking each item in the drinkList
                foreach (OrderedDrink drink in finishedDrinks)
                {
                    ListViewItem li = new ListViewItem("");
                    li.SubItems.Add(drink.OrderedDrinkAmount.ToString());
                    li.UseItemStyleForSubItems = false;
                    li.SubItems.Add(drink.ItemName);

                    //displaying output according if a note is present
                    if (drink.ItemNote == "null")
                    {
                        li.SubItems.Add("No");
                    }
                    else
                    {
                        li.SubItems.Add("Yes");

                        //if a note is present highlight it in light blue
                        li.SubItems[3].BackColor = Color.LightSkyBlue;
                    }

                    li.SubItems.Add(ReturnTimeSpentAsString(drink.TimeDrinkOrdered));
                    li.SubItems.Add(drink.TableNumber.ToString());

                    li.Tag = drink;

                    //adding item to the list
                    lvFinishedDrinks.Items.Add(li);
                }
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message, "Error");
            }
        }

        public void UpdateDishes()
        {
            try
            {
                //retrieving all ordered dishes
                List<OrderedDish> orderedDishes = dishLogic.GetAllOrderedDish();


                //Clearing previous items
                lvOrderedDishes.Items.Clear();

                //checking all items in the dishList
                foreach (OrderedDish dish in orderedDishes)
                {

                    ListViewItem li = new ListViewItem("");
                    li.SubItems.Add(dish.OrderedDishAmount.ToString());
                    li.UseItemStyleForSubItems = false;
                    li.SubItems.Add(dish.ItemName);

                    //displaying output according if a note is present
                    if (dish.ItemNote == "null")
                    {
                        li.SubItems.Add("No");
                    }
                    else
                    {
                        li.SubItems.Add("Yes");

                        //if a note is present highlight it in light blue
                        li.SubItems[3].BackColor = Color.LightSkyBlue;
                    }

                    li.SubItems.Add(ReturnTimeSpentAsString(dish.TimeDishOrdered));

                    //if the time is 1 hour or more, hilight the time in red
                    if (CalculateTimeSpentInMinutes(dish.TimeDishOrdered) >= 60)
                    {
                        li.SubItems[4].BackColor = Color.Red;
                    }

                    li.SubItems.Add(dish.Course);
                    li.SubItems.Add(dish.TableNumber.ToString());

                    li.Tag = dish;

                    //higlighting in green items with status "ToPickUp"
                    if (dish.Status == DishStatus.PickUp)
                    {
                        li.UseItemStyleForSubItems = true;
                        li.BackColor = Color.LightGreen;
                    }

                    //adding items to listView
                    lvOrderedDishes.Items.Add(li);
                    

                }
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message, "Error");
            }

        }

        public void UpdateFinishedDishes()
        {
            try
            {
                //retrieving all finished dishes
                List<OrderedDish> finishedDishes = dishLogic.GetAllFinishedDish();

                //clearing preavious items
                lvFinishedDishes.Items.Clear();

                //adding items in DishListView
                foreach (OrderedDish dish in finishedDishes)
                {

                    ListViewItem li = new ListViewItem("");
                    li.SubItems.Add(dish.OrderedDishAmount.ToString());
                    li.UseItemStyleForSubItems = false;
                    li.SubItems.Add(dish.ItemName);

                    //displaying output according if a note is present
                    if (dish.ItemNote == "null")
                    {
                        li.SubItems.Add("No");
                    }
                    else
                    {
                        li.SubItems.Add("Yes");

                        //if a note is present highlight it in light blue
                        li.SubItems[3].BackColor = Color.LightSkyBlue;
                    }

                    li.SubItems.Add(ReturnTimeSpentAsString(dish.TimeDishOrdered));
                    li.SubItems.Add(dish.Course);
                    li.SubItems.Add(dish.TableNumber.ToString());

                    li.Tag = dish;

                    //adding finished items to listView
                    lvFinishedDishes.Items.Add(li);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error");
            }
        }

        private void btnDrinkReady_Click(object sender, EventArgs e)
        {
            try
            {
                //if no item is selected throw an exception
                if (lvOrderedDrinks.SelectedItems.Count == 0)
                {
                    throw new Exception("No item selected!");
                }

                //changing the status of each selected drink, update table status
                for (int i = 0; i < lvOrderedDrinks.SelectedItems.Count; i++)
                {
                    OrderedDrink drink = (OrderedDrink)lvOrderedDrinks.SelectedItems[i].Tag;
                    UpdateTableToReadyDrink(drink);

                    drinkLogic.UpdateDrinkStatusPickUp(drink);
                    drinkLogic.UpdateOrderWithBartenderID(employee, drink);
                }

                //update current listView
                UpdateDrinks();
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message, "Error");

            }
        }

        private void btnDishReady_Click(object sender, EventArgs e)
        {
            try
            {
                //if no item is selected throw an exception
                if (lvOrderedDishes.SelectedItems.Count == 0)
                {
                    throw new Exception("No item selected!");
                }

                //updating the status of each selected dish, update table status
                for (int i = 0; i < lvOrderedDishes.SelectedItems.Count; i++)
                {
                    OrderedDish dish = (OrderedDish)lvOrderedDishes.SelectedItems[i].Tag;
                    UpdateTableToReadyDish(dish);
                    dishLogic.UpdateDishStatusPickUp(dish);
                    dishLogic.UpdateOrderWithChefID(employee, dish);
                }

                //update current listView
                UpdateDishes();
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message, "Error");
            }
        }

        private void btnViewNote_Click(object sender, EventArgs e)
        {
            try
            {
                //if no item is selected than throw an exception
                if (lvOrderedDishes.SelectedItems.Count == 0)
                {
                    throw new Exception("No item selected!");
                }

                //assign selected item to dish 
                OrderedDish dish = (OrderedDish)lvOrderedDishes.SelectedItems[0].Tag;

                //display either the note text or if the note is empty display "No note"
                if (dish.ItemNote == "null")
                {
                    MessageBox.Show("No note", "Note");
                }
                else
                {
                    MessageBox.Show(dish.ItemNote, "Note");
                }
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message, "Error");
            }
        }
        private void btnViewDrinkNote_Click(object sender, EventArgs e)
        {
            try
            {
                //if no item is selected throw an exception
                if (lvOrderedDrinks.SelectedItems.Count == 0)
                {
                    throw new Exception("No item selected!");
                }

                //assign to drink the selected item
                OrderedDrink d = (OrderedDrink)lvOrderedDrinks.SelectedItems[0].Tag;

                //if the note is empty, display "no note", otherwise display the note
                if (d.ItemNote == "null")
                {
                    MessageBox.Show("No note", "Note");
                }
                else
                {
                    MessageBox.Show(d.ItemNote, "Note");
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error");
            }


        }
        private void btnViewNoteFinDrink_Click(object sender, EventArgs e)
        {
            try
            {
                //if no selected item is selected than throw an exception
                if (lvFinishedDrinks.SelectedItems.Count == 0)
                {
                    throw new Exception("No item selected!");
                }

                //assign to drink the selected item
                OrderedDrink d = (OrderedDrink)lvFinishedDrinks.SelectedItems[0].Tag;

                //if the note is null display "no note", otherwise display the note message
                if (d.ItemNote == "null")
                {
                    MessageBox.Show("No note", "Note");
                }
                else
                {
                    MessageBox.Show(d.ItemNote, "Note");
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error");
            }
        }
        private void btnViewNoteFinDish_Click(object sender, EventArgs e)
        {
            try
            {
                //if no item is selected throw an exception
                if (lvFinishedDishes.SelectedItems.Count == 0)
                {
                    throw new Exception("No item selected!");
                }

                //assign to dish the selected item
                OrderedDish dish = (OrderedDish)lvFinishedDishes.SelectedItems[0].Tag;

                //if the note is null display no note, otherwise display the note message
                if (dish.ItemNote == "null")
                {
                    MessageBox.Show("No note", "Note");
                }
                else
                {
                    MessageBox.Show(dish.ItemNote, "Note");
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error");
            }
        }
        private void btnUndoKitFin_Click(object sender, EventArgs e)
        {
            try
            {
                //if no item is selected throw an exception
                if (lvFinishedDishes.SelectedItems.Count == 0)
                {
                    throw new Exception("No item selected!");
                }

                DialogResult dialogResult = MessageBox.Show("Do you want to undo the selected items?", "Undo Dishes", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.No)
                {
                    return;
                }

                //update each dish status and update the respective table
                for (int i = 0; i < lvFinishedDishes.SelectedItems.Count; i++)
                {
                    OrderedDish finishedDish = (OrderedDish)lvFinishedDishes.SelectedItems[i].Tag;

                    UpdateTableToOrdered(finishedDish.TableNumber);

                    dishLogic.UpdateDishToStart(finishedDish);
                }

                //update current listView
                UpdateFinishedDishes();
                btnUndoKitFin.Enabled = false;

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error");
            }
        }

        private void btnUndoFinDrink_Click(object sender, EventArgs e)
        {
            try
            {
                //if no item is selected throw an exception
                if (lvFinishedDrinks.SelectedItems.Count == 0)
                {
                    throw new Exception("No item selected!");
                }

                DialogResult dialogResult = MessageBox.Show("Do you want to undo the selected items?", "Undo Drinks", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.No)
                {
                    return;
                }

                //update each drink status update the table
                for (int i = 0; i < lvFinishedDrinks.SelectedItems.Count; i++)
                {
                    OrderedDrink finishedDrink = (OrderedDrink)lvFinishedDrinks.SelectedItems[i].Tag;

                    UpdateTableToOrdered(finishedDrink.TableNumber);

                    drinkLogic.UpdateDrinkToStart(finishedDrink);
                }

                //update the current listView
                UpdateFinishedDrinks();
                btnUndoFinDrink.Enabled = false;

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message,"Error");
            }
        }

        private void btnUndoBarView_Click(object sender, EventArgs e)
        {
            try
            {
                //if no item is selected throw an exception
                if (lvOrderedDrinks.SelectedItems.Count == 0)
                {
                    throw new Exception("No selected item!");
                }

                DialogResult dialogResult = MessageBox.Show("Do you want to undo the selected items?", "Undo Drinks", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.No)
                {
                    return;
                }

                //update each item status and update the respective table
                for (int i = 0; i < lvOrderedDrinks.SelectedItems.Count; i++)
                {
                    OrderedDrink orderedDrink = (OrderedDrink)lvOrderedDrinks.SelectedItems[i].Tag;

                    //if the status id to prepare, update the current listView and throw an exception
                    if (orderedDrink.DrinkStatus == DrinkStatus.ToPrepare)
                    {
                        UpdateDrinks();

                        throw new Exception($"You can not undo this item {orderedDrink.ItemName}");

                    }


                    UpdateTableToOrdered(orderedDrink.TableNumber);

                    drinkLogic.BringStatusBack(orderedDrink);
                }

                //update current listView
                btnUndoBarView.Enabled = false;
                btnUndoBarView.BackColor = Button.DefaultBackColor;

                UpdateDrinks();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error");
            }
        }

        private void btnUndoKitView_Click(object sender, EventArgs e)
        {
            try
            {
                //if no item is selected throw an exception
                if (lvOrderedDishes.SelectedItems.Count == 0)
                {
                    throw new Exception("No selected items!");
                }

                DialogResult dialogResult = MessageBox.Show("Do you want to undo the selected items?", "Undo Dishes", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.No)
                {
                    return;
                }

                //update all selected items, update the table
                for (int i = 0; i < lvOrderedDishes.SelectedItems.Count; i++)
                {
                    OrderedDish orderedDish = (OrderedDish)lvOrderedDishes.SelectedItems[i].Tag;

                    //if the selected item is toPrepare, update the current listView and throw an exception
                    if (orderedDish.Status == DishStatus.ToPrepare)
                    {
                        UpdateDishes();
                        throw new Exception($"You can not undo this item {orderedDish.ItemName}");
                    }

                    dishLogic.BringStatusBack(orderedDish);
                    UpdateTableToOrdered(orderedDish.TableNumber);
                }

                //update current listView
                UpdateDishes();
                btnUndoKitView.Enabled = false;
                btnUndoKitView.BackColor = Button.DefaultBackColor;
            }
            catch (Exception exp)
            {
                MessageBox.Show($"{exp.Message}", "Error");
            }

        }

        private void UpdateTableToReadyDish(OrderedDish dish)
        {
            //send an update to all TableOverview
            foreach (TableOverview to in tableOverview)
                to.UpdateTableStatus(dish.TableNumber, TableStatus.DishReady);
        }
        private void UpdateTableToReadyDrink(OrderedDrink drink)
        {
            //send an update to all TableOverview
            foreach (TableOverview to in tableOverview)
                to.UpdateTableStatus(drink.TableNumber, TableStatus.DrinkReady);
        }

        //add waiter to "observers" list
        public void AddWaiterView(TableOverview to)
        {
            tableOverview.Add(to);
        }

        public void UpdateTableToOrdered(int number)
        {
            //Update all registered TableOverview OrderIsRecieved
            foreach (TableOverview to in tableOverview)
            {
                to.UpdateTableStatus(number, TableStatus.Standby);
            }
        }

        //Write error to text file
        public void WriteError(Exception e, string errorMessage)
        {
            StreamWriter writer = File.AppendText("ErrorLog.txt");
            writer.WriteLine($"Error occurred: {errorMessage}");
            writer.WriteLine(e);
            writer.WriteLine(DateTime.Now);
            writer.Close();
        }

        private void runningOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPanel("KitchenView");
        }

        private void runningOrdersToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowPanel("BarView");
        }

        private void finishedOrdersToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowPanel("FinishedDrinkView");
        }

        private void finishedOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPanel("FinishedDishView");
        }
        private void btnFinishedDishes_Click(object sender, EventArgs e)
        {
            ShowPanel("FinishedDishView");
        }

        private void btnFinishedOrdersBar_Click(object sender, EventArgs e)
        {
            ShowPanel("FinishedDrinkView");
        }

        private void btnRunningOrderesKit_Click(object sender, EventArgs e)
        {
            ShowPanel("KitchenView");
        }

        private void btnRunningOrdersBar_Click(object sender, EventArgs e)
        {
            ShowPanel("BarView");
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            UpdateAllListViews();
        }

        private void cmbTableKit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lvOrderedDishes.SelectedItems.Clear();

                string table = (string)cmbTableKit.SelectedItem;

                //select all items of the same table
                foreach (ListViewItem item in lvOrderedDishes.Items)
                    if ($"Table {int.Parse(item.SubItems[6].Text)}" == table)
                    {
                        item.Selected = true;
                    }
            }
            catch (Exception exp) { MessageBox.Show(exp.Message, "Error"); }
        }

        private void cmbCourseKit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lvOrderedDishes.SelectedItems.Clear();

                string course = (string)cmbCourseKit.SelectedItem;
                string table = (string)cmbTableKit.SelectedItem;

                //select all items of the selected table and course
                foreach (ListViewItem item in lvOrderedDishes.Items)
                    if (item.SubItems[5].Text == course && $"Table {int.Parse(item.SubItems[6].Text)}" == table)
                    {
                        item.Selected = true;
                    }
            }
            catch (Exception exp)
            {

                MessageBox.Show(exp.Message, "Error");
            }
        }

        private void lvOrderedDishes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lvOrderedDishes.SelectedItems.Count == 0)
                {
                    btnUndoKitView.Enabled = false;
                    btnUndoKitView.BackColor = DefaultBackColor;
                    return;
                }

                OrderedDish dish = (OrderedDish)lvOrderedDishes.SelectedItems[0].Tag;

                if (dish.Status == DishStatus.PickUp)
                {
                    btnUndoKitView.BackColor = Color.Red;
                    btnUndoKitView.Enabled = true;
                }
            }
            catch (Exception exp)
            {

                MessageBox.Show(exp.Message, "Error");
            }

        }

        private void cmbKitFinished_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lvFinishedDishes.SelectedItems.Clear();

                string table = (string)cmbKitFinished.SelectedItem;

                //select all items of the selected table
                foreach (ListViewItem item in lvFinishedDishes.Items)
                    if ($"Table {int.Parse(item.SubItems[6].Text)}" == table)
                    {
                        item.Selected = true;
                    }
            }
            catch (Exception exp)
            {

                MessageBox.Show(exp.Message, "Error");
            }
        }

        private void cmbBarFinished_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lvFinishedDrinks.SelectedItems.Clear();

                string table = (string)cmbBarFinished.SelectedItem;

                //select all items of the same table
                foreach (ListViewItem item in lvFinishedDrinks.Items)
                    if ($"Table {int.Parse(item.SubItems[5].Text)}" == table)
                    {
                        item.Selected = true;
                    }
            }
            catch (Exception exp)
            {

                MessageBox.Show(exp.Message, "Error");
            }
        }

        private void cmbSelByTabBar_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //clear all selected items
                lvOrderedDrinks.SelectedItems.Clear();

                string table = (string)cmbSelByTabBar.SelectedItem;

                //select all items of the same table
                foreach (ListViewItem item in lvOrderedDrinks.Items)
                    if ($"Table {int.Parse(item.SubItems[5].Text)}" == table)
                    {
                        item.Selected = true;
                    }
            }
            catch (Exception exp)
            {

                MessageBox.Show(exp.Message, "Error");
            }
        }

        private void lvOrderedDrinks_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {               
                if (lvOrderedDrinks.SelectedItems.Count == 0)
                {
                    btnUndoBarView.Enabled = false;
                    btnUndoBarView.BackColor = DefaultBackColor;
                    return;
                }

                OrderedDrink drink = (OrderedDrink)lvOrderedDrinks.SelectedItems[0].Tag;

                if (drink.DrinkStatus == DrinkStatus.PickUp)
                {
                    btnUndoBarView.BackColor = Color.Red;
                    btnUndoBarView.Enabled = true;
                }
            }
            catch (Exception exp)
            {

                MessageBox.Show(exp.Message, "Error");
            }
        }
    }
}
