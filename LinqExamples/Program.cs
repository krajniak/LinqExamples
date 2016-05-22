using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using LinqExamples.DataModel;

namespace LinqExamples
{
    static class Program
    {
        public static void PrintMe<T>(this IEnumerable<T> enumerable, 
            Func<T,string> toString = null)
        {
            foreach (var e in enumerable)
            {
                Console.Out.WriteLine(
                    toString == null 
                     ? e.ToString()
                     : toString(e)
                    );
            }
            Console.ReadKey();
        }

        public static void PrintObject(this object obj)
        {
            Console.Out.WriteLine(obj.ToString());
            Console.ReadKey();
        }

        /// <summary>
        /// Print all customer names
        /// </summary>
        static void Ex1()
        {
            // Data.Customers.Select(c => c.Name).PrintMe();
            IEnumerable<string> q = 
                from c in Data.Customers
                select c.Name;

            q.PrintMe();
        }

        /// <summary>
        /// Print order count of customers
        /// </summary>
        static void Ex2()
        {
            var q = from c in Data.Customers
                select new {Name = c.Name, Count = c.Orders.Count()};

            q.PrintMe(x => String.Format("Klient {0} zamowil {1} produktow",
                x.Name, x.Count ));

            //Program.PrintMe(q, x => String.Format("Klient {0} zamowil {1} produktow",
            //    x.Name, x.Count));
        }

        /// <summary>
        /// Print all orders
        /// </summary>
        /// <param name="args"></param>
        static void Ex3()
        {
            IEnumerable<Order> q = from c in Data.Customers
                from o in c.Orders
                select o;

            q.PrintMe();

            // Data.Customers.SelectMany(customer => customer.Orders).PrintMe();
        }

        /// <summary>
        /// Print all orders of multiple quantity
        /// </summary>
        /// <param name="args"></param>
        static void Ex4()
        {
            var q = from c in Data.Customers
                from o in c.Orders
                where o.Quantity > 1
                select o;

            // SELECT Id, Name, Quantity FROM Customers, Orders Wh
            
            q.PrintMe();
        }

        /// <summary>
        /// Print all customers that ordered nothing
        /// </summary>
        /// <param name="args"></param>
        static void Ex5()
        {
            var q = from c in Data.Customers
                where c.Orders.Count() == 0
                select c;

            q.PrintMe();
        }

        /// <summary>
        /// Print all products
        /// </summary>
        /// <param name="args"></param>
        static void Ex6()
        {
            var q = from c in Data.Customers
                from o in c.Orders
                select o.Product;

            q.Distinct().PrintMe();
        }

        /// <summary>
        /// Print all customers ordered by name
        /// </summary>
        static void Ex7()
        {
            var q = from c in Data.Customers
                orderby c.Name
                select c;

            q.PrintMe();
        }

        /// <summary>
        /// Print all products ordered by id
        /// </summary>
        /// <param name="args"></param>
        static void Ex8()
        {
            var q = from c in Data.Customers
                    from o in c.Orders
                    select o.Product;

            q.Distinct().OrderBy(product=>product.Id).PrintMe();
        }

        /// <summary>
        /// Print only 3 first orders
        /// </summary>
        /// <param name="args"></param>
        static void Ex9()
        {
            IEnumerable<Order> q = from c in Data.Customers
                                   from o in c.Orders
                                   select o;

            q.Take(5).PrintMe();
        }

        /// <summary>
        /// Print cyfry with index
        /// </summary>
        /// <param name="args"></param>
        static void Ex10()
        {
            Data.Cyfry.Select((s, i) => new {Id=i, Name=s}).PrintMe();
        }

        /// <summary>
        /// Print factorial of some number
        /// </summary>
        /// <param name="args"></param>
        static void Ex11()
        {
            Enumerable.Range(1, 4).Aggregate(
                (f, s) => f*s).PrintObject();
        }

        /// <summary>
        /// Numbers of letters equals that number+1
        /// </summary>
        /// <param name="args"></param>
        static void Ex12()
        {
            Data.Cyfry.Where((s,i)=>s.Length == i+1).PrintMe();
        }

        /// <summary>
        /// Count number of digits with number of letters equals 4
        /// </summary>
        /// <param name="args"></param>
        static void Ex13()
        {
//            Data.Cyfry.Where(c => c.Length == 4).PrintMe();
            var q = from c in Data.Cyfry
                where c.Length == 4
                select c;
            q.PrintMe();
        }

        static IEnumerable<int> getSome()
        {
            int i = 1;
            int j = 2;
            while (true)
            {
                yield return i;
                i = i + j;
                j++;
            }            
        }


        static IEnumerable<string> someStuff()
        {
            yield return "jeden";
            yield return "dwa";

        }

        /// <summary>
        /// Print all sums of 1..n smaller than some x
        /// </summary>
        /// <param name="args"></param>
        static void Ex14()
        {
            foreach (var i in getSome())
            {
                i.PrintObject();
                if ( i > 100)
                    break;
            }
        }


        /// <summary>
        /// Print avarage number of letters in digits
        /// </summary>
        /// <param name="args"></param>
        static void Ex15()
        {
            Data.Cyfry.Average(c=>(double)c.Length).PrintObject();
        }


        /// <summary>
        /// Group by example
        /// </summary>
        /// <param name="args"></param>
        private static void Ex16()
        {
            var q = from c in Data.Customers
                    from o in c.Orders
                    group o by o.Product
                        into g
                        select new { Product = g.Key, Orders = g };

            //foreach (var elem in q)
            //{
            //    elem.Product.PrintObject();
            //    elem.Orders.PrintMe();
            //}

            q.PrintMe(elem =>
                String.Format("Product:{0} Elementy:{1}",
                elem.Product,
                elem.Orders
                    .Select(o=>o.Id.ToString())
                    .Aggregate((f,s)=>f+", "+s)
                    )
                );
        }

        static void Main(string[] args)
        {
           Ex16();
        }
    }
}
