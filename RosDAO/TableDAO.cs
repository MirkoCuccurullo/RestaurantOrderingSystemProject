using RosModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace RosDAL
{
    public class TableDAO : BaseDAO
    {
        private SqlConnection conn;
        public TableDAO()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString);
        }
        public Table GetTableById(int tableNumber)
        {
            string query = $"SELECT TableNumber, TableStatus FROM [Table] WHERE TableNumber = @TableNumber ORDER BY [TableNumber]";

            SqlParameter[] sqlParameters = { new SqlParameter("@TableNumber", tableNumber)};

            return ReadTable(ExecuteSelectQuery(query, sqlParameters));
        }

        public List<Table> GetAllTables()
        {
            string query = "SELECT TableNumber, TableStatus FROM [Table] ORDER BY [TableNumber]";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));    
        }

        public int GetAmountOfTables()
        {
            string query = $"SELECT COUNT(TableNumber) AS [count] FROM [Table]";

            SqlParameter[] sqlParameters = new SqlParameter[0];

            return ReadAmount(ExecuteSelectQuery(query, sqlParameters));
        }

        public List<OrderedDrink> GetOrderedDrinks(int tableNumber, DrinkStatus status)
        {
            string query = "SELECT O.TableNumber as tableNumber, OD.TimeDrinkOrdered as [Time], OD.DrinkStatus as [Status], OD.DrinkID as ID, OD.OrderID as [OrderID], I.ItemName as name, OD.DrinkNote as [Note], " +
                "SUM(OD.OrderedDrinkAmount) as [Amount] from OrderDrink as OD " +
                "join [Order] as O on OD.OrderID = O.OrderID " +
                $"join Item as I on OD.DrinkID = I.ItemID join Drink as D on OD.DrinkID = D.DrinkID where OD.DrinkStatus = @status and O.TableNumber = @TableNumber " +
                "group by O.TableNumber, OD.DrinkStatus, OD.DrinkID, I.ItemName, OD.DrinkNote, OD.OrderID, OD.TimeDrinkOrdered " +
                "order by OD.TimeDrinkOrdered; ";

            SqlParameter[] sqlParameters = { new SqlParameter("@TableNumber", tableNumber),
             new SqlParameter("@status", status)};
            return ReadOrderedDrinks(ExecuteSelectQuery(query, sqlParameters));
        }

        public List<OrderedDish> GetOrderedDishes(int tableNumber, DishStatus status)
        {
            string query = "SELECT O.TableNumber as tableNumber, OD.DishStatus as [Status], OD.DishID as ID, OD.OrderID as [OrderID], " +
                 "I.ItemName as name,OD.TimeDishOrdered as [Time], OD.DishNote as [Note], SUM(OD.OrderedDishAmount) as [Amount], D.Course " +
                 "from OrderDish as OD join[Order] as O on OD.OrderID = O.OrderID " +
                 "join Item as I on OD.DishID = I.ItemID join Dish as D on OD.DishID = D.DishID " +
                $"where OD.DishStatus = @status and O.TableNumber = @TableNumber " +
                "group by O.TableNumber, OD.DishStatus, OD.DishID, I.ItemName, OD.DishNote, D.Course, OD.OrderID, OD.TimeDishOrdered; ";

            SqlParameter[] sqlParameters = { new SqlParameter("@TableNumber", tableNumber),
             new SqlParameter("@status", status) };
            return ReadOrderedDishes(ExecuteSelectQuery(query, sqlParameters));
        }

        public void Update(Table table)
        {
                string query = "UPDATE [Table] SET TableStatus = @TableStatus WHERE TableNumber = @TableNumber";
                SqlParameter[] sqlParameters = { new SqlParameter("@TableNumber", table.TableNumber),
                new SqlParameter("@TableStatus", table.TableStatus) };

                ExecuteEditQuery(query, sqlParameters);
        }

        public void UpdateTableWaiter(Table table)
        {
            try
            {
                string query = "UPDATE [Table] SET TableStatus = @TableStatus, WaiterID = @WaiterID WHERE TableNumber = @TableNumber";
                SqlParameter[] sqlParameters = { new SqlParameter("@TableNumber", table.TableNumber),
                new SqlParameter("@TableStatus", table.TableStatus), new SqlParameter("@WaiterID", table.WaiterID) };

                ExecuteEditQuery(query, sqlParameters);
            }
            catch (Exception e)
            {
                throw new Exception("Update failed! " + e.Message);
            }
        }


        private List<Table> ReadTables(DataTable dataTable)
        {
            try
            {
                List<Table> tables = new List<Table>();

                foreach (DataRow dr in dataTable.Rows)
                {
                    Table table = new Table();
                    {
                        table.TableNumber = (int)dr["TableNumber"];
                        table.TableStatus = (TableStatus)dr["TableStatus"];
                    };
                    tables.Add(table);
                }
                return tables;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public List<OrderedDrink> ReadOrderedDrinks(DataTable dataTable)
        {
            try
            {
                List<OrderedDrink> drinks = new List<OrderedDrink>();

                foreach (DataRow dr in dataTable.Rows)
                {
                    string note = "";
                    if (dr["Note"].Equals(DBNull.Value))
                    {
                        note = "null";
                    }
                    else
                    {
                        note = (string)dr["Note"];
                    }

                    OrderedDrink drink = new OrderedDrink()
                    {
                        TableNumber = (int)dr["tableNumber"],
                        DrinkStatus = (DrinkStatus)dr["Status"],
                        OrderID = (int)dr["OrderID"],
                        ItemID = (int)dr["ID"],
                        ItemNote = note,
                        OrderedDrinkAmount = (int)dr["Amount"],
                        ItemName = (string)dr["name"],
                        TimeDrinkOrdered = (DateTime)dr["Time"]
                    };
                    drinks.Add(drink);
                }
                return drinks;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        private List<OrderedDish> ReadOrderedDishes(DataTable dataTable)
        {
            try
            {
                List<OrderedDish> orderedDishes = new List<OrderedDish>();

                foreach (DataRow dr in dataTable.Rows)
                {
                    string note = "";

                    if (dr["Note"].Equals(DBNull.Value))
                    {
                        note = "null";
                    }
                    else
                    {
                        note = (string)dr["Note"];
                    }

                    OrderedDish dish = new OrderedDish()
                    {
                        TableNumber = (int)dr["tableNumber"],
                        Status = (DishStatus)dr["Status"],
                        ItemID = (int)dr["ID"],
                        OrderID = (int)dr["OrderID"],
                        ItemNote = note,
                        OrderedDishAmount = (int)dr["Amount"],
                        ItemName = (string)dr["name"],
                        TimeDishOrdered = (DateTime)dr["Time"],
                        Course = (string)dr["Course"]

                    };
                    orderedDishes.Add(dish);
                }
                return orderedDishes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }      
        }

        private Table ReadTable(DataTable dataTable)
        {
            try
            {
                Table table = new Table();
                foreach (DataRow dr in dataTable.Rows)
                {
                    table.TableNumber = (int)dr["TableNumber"];
                    table.TableStatus = (TableStatus)dr["TableStatus"];
                }
                return table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        private int ReadAmount(DataTable dataTable)
        {
            try
            {
                int count = 0;
                foreach (DataRow dr in dataTable.Rows)
                {
                    count = (int)dr["count"];
                }
                return count;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }
    }
}
