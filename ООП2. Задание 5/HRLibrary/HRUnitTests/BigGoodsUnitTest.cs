using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HRLibrary;
using System.IO;

namespace HRUnitTests
{
    [TestClass]
    public class BigGoodsUnitTest
    {
        [TestMethod]
        public void ConstructerTestMethod()
        {
            var goods = CreateGoods();
            Assert.AreEqual(50, goods.Length);
            Assert.AreEqual(150, goods.Height);
            Assert.AreEqual(30, goods.Width);
        }

        [TestMethod]
        public void PrintInfoTestMethod()
        {
            var goods = CreateGoods();

            var lines = new[]
            {
                "Шкаф Что-то",
                "Оптовая цена за единицу товара: 10000руб. за шт," +
                " Розничная цена за единицу товара 10020руб. за шт," +
                " Описание: Шкаф для дачи и дома На складе: 10",
                "Размеры товара: Длинна 50см, Высота 150см, Ширина 30см"
            };

            using(FileStream file = new FileStream("test.txt", FileMode.Create))
            {
                using (TextWriter writer = new StreamWriter(file))
                {
                    Console.SetOut(writer);
                    goods.PrintInfo();
                }
            }

            TextWriter oldOut = Console.Out;
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

        private BigGoods CreateGoods()
        {
            var goods = new BigGoods("Шкаф", "Что-то", CommodityUnits.Pieces, 10000, 10020, 50, 150, 30);
            goods.Count = 10;
            goods.Description = "Шкаф для дачи и дома";
            return goods;
        }
    }
}
