﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosModel
{
    public enum DishStatus { ToPrepare, PickUp, Serve, Payed }

    public class OrderedDish
    {
        public int OrderID { get; set; }
        public int DishID { get; set; }

        public DishStatus Status { get; set; }
        public DateTime TimeDishOrdered { get; set; }
        public DateTime TimeDishDelivered { get; set; }

        public int TableNumber { get; set; }

        public decimal Price { get; set; }

        public string Course { get; set; }

        public string Name { get; set; }
        public int OrderedDishAmount { get; set; }
        public string DishNote { get; set; }
    }
}
