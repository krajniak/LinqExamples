using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExamples.DataModel
{
    public static class Data
    {
        private static List<Product> products = new List<Product>
        {
            new Product(){Id=1,Name="Donut"},
            new Product(){Id=2,Name="Cheese Cake"},
            new Product(){Id=3,Name="Apple Pie"}
        };

        private static List<Customer> customers = new List<Customer>
        {
            new Customer{Id=1,Name="Walter Doe",Orders = new List<Order>
            {
               new Order(){Id=1,Product = products[0],Quantity = 1}, 
               new Order(){Id=2,Product = products[2],Quantity = 5}, 
            }},
            new Customer{Id=2,Name="Poor Jane",Orders = new List<Order>
            {
                
            }},
            new Customer{Id=3,Name="Sweet Mery",Orders = new List<Order>
            {
               new Order(){Id=3,Product = products[1],Quantity = 10}, 
               new Order(){Id=4,Product = products[0],Quantity = 15}, 
               new Order(){Id=5,Product = products[2],Quantity = 20}, 
               
            }}
        };

        public static List<Customer> Customers
        {
            get { return customers; }
        }

        private static List<String> cyfry = new List<string>
        {
            "zero","jeden","dwa","trzy","cztery","pięć","sześć","siedem","osiem","dziewięć"
        };

        public static List<String> Cyfry { get { return cyfry; } } 
    }
}
