using System;

namespace SalesTaxes
{
    public interface IGoodFactory
    {
        Good GetGood(string Name, decimal price, bool imported, Category category, int quantity);
    }

    public class GoodFactory : IGoodFactory
    {
        public Good GetGood(string name, decimal price, bool imported, Category category, int quantity)
        {
            switch (category)
            {
                case Category.Book:
                case Category.Food:
                case Category.Medical:
                    return new TaxFreeGood(name, price, imported, category, quantity);
                case Category.Other:
                    return new BaseTaxGood(name, price, imported, category, quantity);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}