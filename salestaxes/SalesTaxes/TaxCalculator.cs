using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesTaxes
{
    public class TaxCalculator
    {
        private List<Good> goods;

        public TaxCalculator(List<Good> goods)
        {
            this.goods = goods;
        }

        public decimal SumTaxes()
        {
            List<decimal> taxPerItem = new List<decimal>();
            foreach (var good in goods)
            {
                if (good is Good)
                {
                    taxPerItem.Add(good.CountTaxes());
                }
            }

            return taxPerItem.Sum();
        }

        public string Output()
        {
            return "Sales Taxes: " + this.SumTaxes();
        }
    }
}
