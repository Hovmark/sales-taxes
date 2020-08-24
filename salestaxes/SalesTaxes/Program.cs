using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesTaxes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Shopping list");
            Console.WriteLine("Choose output 1, 2 or 3: ");
            if (!int.TryParse(Console.ReadLine(), out int output))
            {
                throw new Exception("Invalid output");
            }

            var goods = GetSelectedGoods(output);

            var reciept = new TaxCalculator(goods);

            var sumTaxes = reciept.SumTaxes();

            Console.WriteLine("OUTPUT");
            Console.WriteLine();
            foreach (var g in goods)
            {
                var imported = g.Imported ? "imported " : "";
                Console.WriteLine(g.Quantity + " " + imported + g.Title + ": " + Math.Round(g.PriceWithTax, 2));
            }

            Console.WriteLine("Sales Taxes: " + sumTaxes);
            Console.WriteLine("Total: " + Math.Round(goods.Sum(s => s.PriceWithTax), 2));
            Console.WriteLine();

        }

        private static List<Good> GetSelectedGoods(int selectedNum)
        {
            IGoodFactory goodFactory = new GoodFactory();
            List<Good> goods = new List<Good>();
            Good product1 = null;
            Good product2 = null;
            Good product3 = null;
            Good product4 = null;

            if (selectedNum == 1)
            {
                product1 = goodFactory.GetGood("book", Convert.ToDecimal(12.49), false, Category.Book, 2);
                product2 = goodFactory.GetGood("music CD", Convert.ToDecimal(14.99), false, Category.Other, 1);
                product3 = goodFactory.GetGood("chocolate bar", Convert.ToDecimal(0.85), false, Category.Food, 1);
                goods.Add(product1);
                goods.Add(product2);
                goods.Add(product3);

            }
            if (selectedNum == 2)
            {
                product1 = goodFactory.GetGood("box of chocolates", Convert.ToDecimal(10.00), true, Category.Food, 1);
                product2 = goodFactory.GetGood("bottle of perfume", Convert.ToDecimal(47.50), true, Category.Other, 1);
                goods.Add(product1);
                goods.Add(product2);
            }
            if (selectedNum == 3)
            {
                product1 = goodFactory.GetGood("bottle of perfume", Convert.ToDecimal(27.99), true, Category.Other, 1);
                product2 = goodFactory.GetGood("bottle of perfume", Convert.ToDecimal(18.99), false, Category.Other, 1);
                product3 = goodFactory.GetGood("headache pills", Convert.ToDecimal(9.75), false, Category.Medical, 1);
                product4 = goodFactory.GetGood("box chocolate", Convert.ToDecimal(11.25), true, Category.Food, 3);
                goods.Add(product1);
                goods.Add(product2);
                goods.Add(product3);
                goods.Add(product4);

            }

            return goods;
        }
    }
}
