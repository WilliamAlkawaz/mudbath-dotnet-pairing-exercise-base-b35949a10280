using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using dotnet_pairing_exercise_base.data;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace dotnet_pairing_exercise_base.service{
    public interface IProcessService
    {
        void Run();
    }

    public class ProcessService : IProcessService
    {
        private List<Order> orders;
        private List<Payment> payments;
        private List<Price> prices; 
        public ProcessService()
        {
            orders = InitialiseOrders();
            payments = InitialisePayments();
            prices = InitialisePrices(); 
        }

        public void Run()
        {
            Console.WriteLine("Hello World");
            PrintOrders();
            PrintOrdersByUser();
            PrintOrdersByUserWithTotal();
            try
            {
                PrintOrdersByUserWithPaymentTotal();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message); 
            }

            try
            {
                PrintOrdersByUserWithPaymentTotalControlled();
            }
            catch (Exception e)
            {
                Console.WriteLine("");
                Console.WriteLine("The sixth requirement");
                Console.WriteLine(e.Message); 
            }
        }

        public void PrintOrders()
        {
            Console.WriteLine("");
            Console.WriteLine("The first requirement");
            foreach(var item in orders)
            {
                Console.WriteLine("User: {0}, Drink: {1}, Size: {2}", item.User, item.Drink, item.Size); 
            }
        }

        public void PrintOrdersByUser()
        {
            List<dynamic> ordersByUsers = new List<dynamic>();

            foreach (var item in orders)
            {
                if (ordersByUsers.Any(x => x.name == item.User))
                {
                    ordersByUsers.First(x => x.name == item.User).count += 1;
                }
                else
                {
                    dynamic obj = new System.Dynamic.ExpandoObject();
                    obj.name = item.User;
                    obj.count = 1;
                    ordersByUsers.Add(obj);
                }
            }

            Console.WriteLine("");
            Console.WriteLine("The second requirement");
            Console.WriteLine(JsonConvert.SerializeObject(ordersByUsers));
        }

        public void PrintOrdersByUserWithTotal()
        {
            List<dynamic> ordersByUsers = new List<dynamic>();

            foreach (var item in orders)
            {
                int i;
                if (item.Size == "small")
                    i = 0;
                else if (item.Size == "medium")
                    i = 1;
                else
                    i = 2;
                if (ordersByUsers.Any(x => x.name == item.User))
                {
                    ordersByUsers.First(x => x.name == item.User).count += 1;
                    ordersByUsers.First(x => x.name == item.User).balance += 
                        prices.First(x => x.Drink == item.Drink).Prices[i];
                }
                else
                {
                    dynamic obj = new System.Dynamic.ExpandoObject();
                    obj.name = item.User;
                    obj.count = 1;
                    obj.balance = prices.First(x => x.Drink == item.Drink).Prices[i]; 
                    ordersByUsers.Add(obj);
                }
            }

            Console.WriteLine("");
            Console.WriteLine("The third requirement");
            Console.WriteLine(JsonConvert.SerializeObject(ordersByUsers));
        }

        public void PrintOrdersByUserWithPaymentTotal()
        {
            List<dynamic> ordersByUsers = new List<dynamic>();

            foreach (var item in orders)
            {
                int i;
                if (item.Size == "small")
                    i = 0;
                else if (item.Size == "medium")
                    i = 1;
                else
                    i = 2;
                if (ordersByUsers.Any(x => x.name == item.User))
                {
                    ordersByUsers.First(x => x.name == item.User).count += 1;
                    ordersByUsers.First(x => x.name == item.User).balance +=
                        prices.First(x => x.Drink == item.Drink).Prices[i];
                    ordersByUsers.First(x => x.name == item.User).amountOfMoneyUserOwes =
                        ordersByUsers.First(x => x.name == item.User).balance -
                        ordersByUsers.First(x => x.name == item.User).paymentTotal; 
                }
                else
                {
                    dynamic obj = new System.Dynamic.ExpandoObject();
                    obj.name = item.User;
                    obj.count = 1;
                    obj.balance = prices.First(x => x.Drink == item.Drink).Prices[i];
                    obj.paymentTotal = payments.First(x => x.User == item.User).Amount;
                    obj.amountOfMoneyUserOwes = obj.balance - obj.paymentTotal; 
                    ordersByUsers.Add(obj);
                }
            }

            Console.WriteLine("");
            Console.WriteLine("The fifth requirement");
            Console.WriteLine(JsonConvert.SerializeObject(ordersByUsers));
        }

        public void PrintOrdersByUserWithPaymentTotalControlled()
        {
            List<dynamic> ordersByUsers = new List<dynamic>();

            foreach (var item in orders)
            {
                int i;
                if (item.Size == "small")
                    i = 0;
                else if (item.Size == "medium")
                    i = 1;
                else if (item.Size == "large")
                    i = 2;
                else
                    throw new Exception("Unsupported order type.");
                if (ordersByUsers.Any(x => x.name == item.User))
                {
                    ordersByUsers.First(x => x.name == item.User).count += 1;
                    ordersByUsers.First(x => x.name == item.User).balance +=
                        prices.First(x => x.Drink == item.Drink).Prices[i];
                    ordersByUsers.First(x => x.name == item.User).amountOfMoneyUserOwes =
                        ordersByUsers.First(x => x.name == item.User).balance -
                        ordersByUsers.First(x => x.name == item.User).paymentTotal;
                }
                else
                {
                    dynamic obj = new System.Dynamic.ExpandoObject();
                    obj.name = item.User;
                    obj.count = 1;
                    obj.balance = prices.First(x => x.Drink == item.Drink).Prices[i];
                    obj.paymentTotal = payments.First(x => x.User == item.User).Amount;
                    obj.amountOfMoneyUserOwes = obj.balance - obj.paymentTotal;
                    ordersByUsers.Add(obj);
                }
            }

            Console.WriteLine("");
            Console.WriteLine("The sixth requirement");
            Console.WriteLine(JsonConvert.SerializeObject(ordersByUsers));
        }
        public List<Order> InitialiseOrders()
        {
            List<Order> orders = new List<Order>(); 
            using (StreamReader r = new StreamReader("../../../data/orders.json"))
            {
                string json = r.ReadToEnd();
                dynamic array = JsonConvert.DeserializeObject(json);
                foreach (var item in array)
                {
                    orders.Add(new Order() { Drink = item.drink, Size = item.size, User = item.user });
                }
            }

            return orders; 
        }

        public List<Payment> InitialisePayments()
        {
            List<Payment> payments = new List<Payment>(); 
            using (StreamReader r = new StreamReader("../../../data/payments.json"))
            {
                string json = r.ReadToEnd();
                dynamic array = JsonConvert.DeserializeObject(json);
                foreach (var item in array)
                {
                    payments.Add(new Payment() { User = item.user, Amount = item.amount });
                }
            }

            return payments; 
        }

        public List<Price> InitialisePrices()
        {
            List<Price> prices = new List<Price>(); 
            using (StreamReader r = new StreamReader("../../../data/prices.json"))
            {
                string json = r.ReadToEnd();
                dynamic array = JsonConvert.DeserializeObject(json);
                foreach (var item in array)
                {

                    decimal[] priceArr = new decimal[3];
                    if(item.prices.small != null)
                        priceArr[0] = Decimal.Parse(item.prices.small.ToString());
                    if (item.prices.medium != null)
                        priceArr[1] = Decimal.Parse(item.prices.medium.ToString());
                    if (item.prices.large != null)
                        priceArr[2] = Decimal.Parse(item.prices.large.ToString());
                    prices.Add(new Price() { Drink = item.drink_name, Prices = priceArr});
                }
            }

            return prices; 
        }
    }
}