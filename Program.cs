using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8
{
    class Program
    {
        public static DataClasses1DataContext context = new DataClasses1DataContext();
        static void Main(string[] args)
        {

            // IntroToLINQ();
            // DataSource();
            //Filtering();
            //Ordering();
            // Grouping();
            // Grouping2();
            // Joining();
            //IntroToLINQLambda();
            // DataSourceLambda();
            //FilteringLambda();
            //OrderingLambda();
            //GroupingLambda();
            //Grouping2Lambda();
            JoiningLambda();
            Console.Read();
        }

        static void IntroToLINQ()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };
            var numQuery =
                from num in numbers
                where (num % 2) == 0
                select num;
            foreach (int num in numQuery)
            {
                Console.Write("{0,1}", num);
            }
        }

        static void DataSource()
        {
            var queryAllCustomers = from cust in context.clientes
                                    select cust;

            foreach (var item in queryAllCustomers)
            {

                Console.WriteLine(item.NombreCompañia);

            }
        }

        static void Filtering()
        {
            var queryLondonCostumers = from cust in context.clientes
                                       where cust.Ciudad == "Londres"
                                       select cust;
            
            foreach(var item in queryLondonCostumers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        static void Ordering()
        {
            var queryLondonCustomers = from cust in context.clientes
                                       where cust.Ciudad == "Londres"
                                       orderby cust.NombreCompañia ascending
                                       select cust;

            foreach(var item in queryLondonCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        static void Grouping()
        {
            var queryCustomersByCity = from cust in context.clientes
                                       group cust by cust.Ciudad;

            foreach(var customerGroup in queryCustomersByCity)
            {
                Console.WriteLine(customerGroup.Key);
                foreach(clientes customer in customerGroup)
                {
                    Console.WriteLine("         {0}", customer.NombreCompañia);
                }
            }
        }

        static void Grouping2()
        {
            var custQuery = from cust in context.clientes
                            group cust by cust.Ciudad into custGroup
                            where custGroup.Count() > 2
                            orderby custGroup.Key
                            select custGroup;

            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
                
            }
        }

        static void Joining()
        {
            var innerJoinQuery =
                from cust in context.clientes
                join dist in context.Pedidos on cust.idCliente equals dist.IdCliente
                select new { CustomerName = cust.NombreCompañia, DistributorName = dist.PaisDestinatario };

            foreach(var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
        }
        /// <summary>
        /// Lambda functions 
        /// </summary>


        static void IntroToLINQLambda()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };
            var numLambda = numbers.Select(x => x).Where(x=> x%2 == 0).ToList();
            Console.WriteLine("Lambda");
            numLambda.ForEach(i => Console.WriteLine(i));
        }

        static void DataSourceLambda()
        {
            var AllCustomersLambda = context.clientes.Select(cust => cust).ToList();

            Console.WriteLine("Lambda2");
            AllCustomersLambda.ForEach(e => Console.WriteLine(e.NombreCompañia));
        }



        static void FilteringLambda()
        {
            var LondonCustomersLambda = context.clientes
                .Select(cust => cust)
                .Where(cust => cust.Ciudad == "Londres").ToList();

            Console.WriteLine("Lambda3");
            LondonCustomersLambda.ForEach(i => Console.WriteLine(i.NombreCompañia));
        }

        static void OrderingLambda()
        {
           
            var LondonCustomersLambda = context.clientes
                .Select(cust => cust)
                .Where(cust => cust.Ciudad == "Londres")
                .OrderBy(cust => cust.NombreCompañia)
                .ToList();

            Console.WriteLine("Lambda3");
            LondonCustomersLambda.ForEach(i => Console.WriteLine(i.NombreCompañia));

        }

        static void GroupingLambda()
        {

            var CustomersByCityLambda = context.clientes
                .Select(cust => cust)
                .GroupBy(cust => cust.Ciudad)
                .ToList();

            CustomersByCityLambda.ForEach(i =>
            {
                Console.WriteLine(i.Key);
                foreach(clientes costumer in i)
                {
                    Console.WriteLine("         {0}",costumer.NombreCompañia);
                }
            });

        }

        static void Grouping2Lambda()
        {
            var custLambda = context.clientes.Select(x => x).
                GroupBy(x => x.Ciudad)
                .Where(x => x.Count() > 2)
                .OrderBy(x => x.Key)
                .ToList();
            custLambda.ForEach(i => Console.WriteLine(i.Key));
        }

        static void JoiningLambda()
        {

            var innerJoinLambda = context.clientes
               .Join(context.Pedidos,
               cust => cust.idCliente,
               dist => dist.IdCliente,
               (cust, dist) => new { cust, dist }).
               Where(x=> x.dist.IdCliente == x.cust.idCliente).Select(x => new
               {
                   CustomerName = x.cust.NombreCompañia,
                   DistributorName = x.dist.PaisDestinatario
               }).ToList();
            innerJoinLambda.ForEach(i => Console.WriteLine(i.CustomerName));
        }

        




    }
}
