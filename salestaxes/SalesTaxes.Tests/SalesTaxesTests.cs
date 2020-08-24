using System;
using System.Collections.Generic;
using Xunit;

namespace SalesTaxes.Tests
{
    public class SalesTaxesTests
    {
        IGoodFactory _goodFactory;

        public SalesTaxesTests()
        {
            _goodFactory = new GoodFactory();
        }

        [Fact]
        public void CountTaxes_NoImportedOneBasicTax_BasicTaxCalculated()
        {
            //Arrange
            var product1 = _goodFactory.GetGood("book", Convert.ToDecimal(12.49), false, Category.Book, 2);
            var product2 = _goodFactory.GetGood("music CD", Convert.ToDecimal(14.99), false, Category.Other, 1);
            var product3 = _goodFactory.GetGood("chocolate bar", Convert.ToDecimal(0.85), false, Category.Food, 1);

            //Act
            var tax1 = product1.CountTaxes();
            var tax2 = product2.CountTaxes();
            var tax3 = product3.CountTaxes();

            //Assert
            var taxProduct2 = Math.Round(product2.Price * Convert.ToDecimal(0.1), 2);
            Assert.True(tax1 == 0);
            Assert.True(tax2 == taxProduct2);
            Assert.True(tax3 == 0);

        }

        [Fact]
        public void CountTaxes_TwoImportedOneBasicTax_BasicAndImportedTaxCalculated()
        {
            //Arrange
            var product1 = _goodFactory.GetGood("box of chocolates", Convert.ToDecimal(10.00), true, Category.Food, 1);
            var product2 = _goodFactory.GetGood("bottle of perfume", Convert.ToDecimal(47.50), true, Category.Other, 1);

            //Act
            var tax1 = product1.CountTaxes();
            var tax2 = product2.CountTaxes();

            //Assert
            var taxProduct1 = Math.Round(product1.Price * Convert.ToDecimal(0.05), 2);
            var baseTaxProduct2 = product2.Price * Convert.ToDecimal(0.1);
            var taxProduct2 = product2.Price * Convert.ToDecimal(0.05);
            Assert.True(tax1 == taxProduct1);
            Assert.True(tax2 == Math.Round(baseTaxProduct2 + taxProduct2,2));

        }

        [Fact]
        public void CountTaxes_TwoImportedTwoBasicTax_BasicAndImportedTaxCalculated()
        {
            //Arrange
            var product1 = _goodFactory.GetGood("bottle of perfume", Convert.ToDecimal(27.99), true, Category.Other, 1);
            var product2 = _goodFactory.GetGood("bottle of perfume", Convert.ToDecimal(18.99), false, Category.Other, 1);
            var product3 = _goodFactory.GetGood("headache pills", Convert.ToDecimal(9.75), false, Category.Medical, 1);
            var product4 = _goodFactory.GetGood("box chocolate", Convert.ToDecimal(11.25), true, Category.Food, 3);

            //Act
            var tax1 = product1.CountTaxes();
            var tax2 = product2.CountTaxes();
            var tax3 = product3.CountTaxes();
            var tax4 = product4.CountTaxes();

            //Assert
            var baseTaxProduct1 = product1.Price * Convert.ToDecimal(0.1);
            var taxProduct1 = product1.Price * Convert.ToDecimal(0.05);
            var baseTaxProduct2 = Math.Round(product2.Price * Convert.ToDecimal(0.1), 2);
            var taxProduct4 = Math.Round(product4.Price * product4.Quantity * Convert.ToDecimal(0.05), 2);
            Assert.True(tax1 ==  Math.Round(baseTaxProduct1 + taxProduct1,2));
            Assert.True(tax2 == baseTaxProduct2);
            Assert.True(tax3 == 0);
            Assert.True(tax4 == taxProduct4);

        }
    }
}
