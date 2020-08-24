
using System;

namespace SalesTaxes
{

    public class Good
    {
        public string Title;
        public decimal Price;

        public bool Imported;
        public Category Category;
        public int Quantity;
        public decimal PriceWithTax => Price * Quantity + CountTaxes();
        public decimal Tax;

        public Good(string title, decimal price, bool imported, Category category, int quantity)
        {
            Title = title;
            Price = price;
            Imported = imported;
            Category = category;
            Quantity = quantity;
        }

        public virtual decimal CountTaxes()
        {
            decimal tax = 0;

            if (Imported == true)
                tax = Convert.ToDecimal(Convert.ToDouble(Price * Quantity) * 0.05);
            return Math.Round(tax, 2);
        }

        private decimal CalculateImportedTax()
        {
            decimal importedTax = 0;

            //All imported goods at a rate of 5%
            var taxrate = Convert.ToDecimal(0.05);
            if (Imported)
                importedTax = Price * taxrate * Quantity;
            return importedTax;
        }
    }

    public class TaxFreeGood : Good
    {
        public TaxFreeGood(string title, decimal price, bool imported, Category category, int quantity) : base(title, price, imported, category, quantity)
        {

        }

        //No tax on Books, food and medical products
    }

    public class BaseTaxGood : Good
    {
        public BaseTaxGood(string title, decimal price, bool imported, Category category, int quantity) : base(title, price, imported, category, quantity)
        {
        }

        public override decimal CountTaxes()
        {
            decimal importedTax = 0;
            var totalPrice = Convert.ToDouble(Price * Quantity);

            if (Imported == true)
                importedTax = Convert.ToDecimal(totalPrice * 0.05);

            var salesTax = Convert.ToDecimal(totalPrice * 0.1);
            return Math.Round(importedTax + salesTax, 2);
        }
    }
}
