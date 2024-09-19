using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Formats.Asn1;

namespace ProjectPartA_A1
{
    class Program
    {
        struct Article
        {
            public string Name;
            public decimal Price;
        }

        const int _maxNrArticles = 10;
        const int _maxArticleNameLength = 20;
        const decimal _vat = 0.25M;
        

        static Article[] articles = new Article[_maxNrArticles];
        static int nrArticles;

        static void Main(string[] args)
        {
            System.Console.WriteLine("Welcome to Amo's Gatukiosk");
            System.Console.WriteLine("Lets print a receipt!\n\n");
            ReadArticles();
            PrintReciept();
        }

        public static void ReadArticles()
        {
            int i = 0;

            System.Console.WriteLine("How many articles do you want (Between 1-10)?");

            while (!int.TryParse(Console.ReadLine(), out nrArticles) || nrArticles < 1 || nrArticles > 10)
            {
                Console.WriteLine("ERROR! Input number between 1-10!. Try again!");
            }

            while (i < nrArticles)
            {
                System.Console.WriteLine($"Please enter name and price for the #0 in the format name; (example beer; 2.25)", _maxNrArticles);
                string _input = Console.ReadLine();

                string[] _output = _input.Split(";");

                if (_output.Length != 2)
                {
                    Console.Clear();
                    System.Console.WriteLine("Wrong input! Please try again!");
                    continue;
                }

                if (!decimal.TryParse(_output[1], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price) || _output[0].Length < 1 || _output[0].Length > _maxArticleNameLength) // I had help with this code snippet because my VS couldn't register "." in the terminal and this was the solution apparently.
                {
                    Console.Clear();
                    System.Console.WriteLine("Wrong input! Article name too long!");
                    continue;
                }

                articles[i] = new Article() {Name = _output[0], Price = price};
                i++;
            }
        }

        private static void PrintReciept()
        {
            decimal sum = 0.00M;
            System.Console.WriteLine("Reciept Purchased Articles!");
            DateTime aDate = DateTime.Now;
            System.Console.WriteLine(aDate.ToString("dddd, dd MMMM yyyy HH:mm:ss\n"));
            System.Console.WriteLine($"Number of items purchased: {nrArticles}\n");
            System.Console.WriteLine($"{"#"}  {"Name",-19} {"Price",10}");


            for (int i = 0; i < nrArticles; i++)
            {
              System.Console.WriteLine($"{i + 1}: {articles[i].Name,-19} {articles[i].Price,12:C}"); //The formating numbers ie -19 does not correspond with the
              //latter code. I do not know the reason of it or even if it should be the same corresponding number. But the formating looks sharp.
              sum += articles[i].Price;
            }
            decimal totalVat = _vat * sum;
            decimal totalWithVat = totalVat;
            System.Console.WriteLine($"Total purchased: {sum, 18:C}");
            System.Console.WriteLine($"Includes VAT (25%) {totalVat, 16:C}");
            
        }
    }
}