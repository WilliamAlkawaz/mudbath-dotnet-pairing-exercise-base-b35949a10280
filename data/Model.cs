using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_pairing_exercise_base.data
{
    public class Model
    {
    }

    public class Order
    {
        public string User { get; set; }
        public string Drink { get; set; }
        public string Size { get; set; }
    }

    public class Payment
    {
        public string User { get; set; }
        public int Amount { get; set; }
    }

    public class Price
    {
        public string Drink { get; set; }
        public decimal[] Prices = new decimal[3]; // Index 0 for small, 1 for medium, and 2 for large
    }
}
