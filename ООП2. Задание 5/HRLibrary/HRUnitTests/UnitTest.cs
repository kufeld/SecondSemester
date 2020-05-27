using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HRLibrary;
using System.IO;
    
namespace HRUnitTests
{
    [TestClass]
    public class CommodityUnitTest
    {
        
        [TestMethod]
        public void ConstructorTestMethod()
        {
            var product = CreateCommodity();

            Assert.AreEqual("Кресло", product.Name);
            Assert.AreEqual("Для офиса", product.ArticleNumber);
            Assert.AreEqual(CommodityUnits.Pieces, product.CommodityUnit);
            Assert.AreEqual(5000, product.WholesalePrice);
            Assert.AreEqual(6540, product.RetailPrice);
        }

        [TestMethod]
        public void ToStringTestMethod()
        {
            var product = CreateCommodity();
            Assert.AreEqual("Кресло Для офиса", product.ToString());
        }

        [TestMethod]
        public void PrintInfoTestMethod()
        {
            var firstProduct = CreateCommodity();
            var secondProduct = new Commodity("Стол", "Для кухни", CommodityUnits.Pieces, 2000, 2300)
            {
                Count = 100,
                Description = "Обычный обеденный стол для кафе, кухни, дачи. Столешница- пластик." +
                " Разные варианты расцветок. Простой и недорогой. Самый минимум." +
                " Ножки стола могут быть цилиндрическими-хром."
            };

            var consoleOut = new[]
            {
                "Кресло Для офиса",

                "Оптовая цена за единицу товара: 5000руб. за шт," +
                " Розничная цена за единицу товара 6540руб. за шт," +
                " Описание: Современое и удобное кресло «на полозьях». Стильное и оригинальное," +
                " на стальном основании, оно позволит Вашим сотрудникам работать в офисе в комфортных условиях." +
                " Прекрасный вариант для домашнего использования, особенно в случаях с покрытием пола из ламината."+
                " На складе: 100",

                "Стол Для кухни",

                "Оптовая цена за единицу товара: 2000руб. за шт," +
                " Розничная цена за единицу товара 2300руб. за шт," +
                " Описание: Обычный обеденный стол для кафе, кухни, дачи. Столешница- пластик." +
                " Разные варианты расцветок. Простой и недорогой. Самый минимум." +
                " Ножки стола могут быть цилиндрическими-хром."+
                " На складе: 100"
            };

            TextWriter oldOut = Console.Out;

            using (FileStream file = new FileStream("test.txt", FileMode.Create))
            {
                using (TextWriter writer = new StreamWriter(file))
                {
                    Console.SetOut(writer);
                    firstProduct.PrintInfo();
                    secondProduct.PrintInfo();
                }
            }

            Console.SetOut(oldOut);
            var i = 0;

            foreach (var line in File.ReadLines("test.txt"))
                Assert.AreEqual(consoleOut[i++], line);

            File.Delete("test.txt");
        }

        public Commodity CreateCommodity()
        {
            return new Commodity("Кресло", "Для офиса", CommodityUnits.Pieces, 5000, 6540)
            { Count = 100, Description = "Современое и удобное кресло «на полозьях». Стильное и оригинальное," +
            " на стальном основании, оно позволит Вашим сотрудникам работать в офисе в комфортных условиях." +
            " Прекрасный вариант для домашнего использования, особенно в случаях с покрытием пола из ламината." };
        }
    }
}
