using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HRLibrary;
using System.IO;

namespace HRUnitTests
{
    [TestClass]
    public class PerishableGoodsUnitTest
    {
        [TestMethod]
        public void ConstructerTestMethod()
        {
            var goods = CreateGoods();
            Assert.AreEqual(7, goods.MaxShelfLife);
        }

        [TestMethod]
        public void PrintInfoTestMethod()
        {
            var goods = CreateGoods();

            var lines = new[]
            {
                "Молоко Напитки",
                "Оптовая цена за единицу товара: 30руб. за пак," +
                " Розничная цена за единицу товара 30руб. за пак," +
                " Описание: Полезное и питательное На складе: 100",
                "Срок годности: 7 дней"
            };

            using (FileStream file = new FileStream("test.txt", FileMode.Create))
            {
                using (TextWriter writer = new StreamWriter(file))
                {
                    Console.SetOut(writer);
                    goods.PrintInfo();
                }
            }

            using (FileStream file = new FileStream("test.txt", FileMode.Open))
            {
                using (TextReader reader = new StreamReader(file))
                {
                    var i = 0;
                    while (!(reader as StreamReader).EndOfStream)
                        Assert.AreEqual(lines[i++], reader.ReadLine());
                    Assert.AreEqual(lines.Length, i);
                }
            }
            File.Delete("test.txt");
        }

        private PerishableGoods CreateGoods()
        {
            var goods = new PerishableGoods("Молоко", "Напитки", CommodityUnits.Packaging, 30, 30, 7);
            goods.Count = 100;
            goods.Description = "Полезное и питательное";
            return goods;
        }
    }
}
