using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HRLibrary;
using System.IO;

namespace HRUnitTests
{
    [TestClass]
    public class FragileGoodsUnitTest
    {
        [TestMethod]
        public void ConstructorTestMethod()
        {
            var goods = CreateGoods();
            Assert.AreEqual(10, goods.MaxCountInStack);
        }

        [TestMethod]
        public void PrintInfoTestMethod()
        {
            var goods = CreateGoods();

            var lines = new[]
            {
                "Зеркало Хрупкие товары",
                "Оптовая цена за единицу товара: 1000руб. за шт," +
                " Розничная цена за единицу товара 100руб. за шт," +
                " Описание: Для дома и дачи На складе: 100",
               "максимальное количество единиц товара в стопке: 10"
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

        private FragileGoods CreateGoods()
        {
            var goods = new FragileGoods("Зеркало", "Хрупкие товары", CommodityUnits.Pieces, 1000, 100, 10);
            goods.Count = 100;
            goods.Description = "Для дома и дачи";
            return goods;
        }
    }
}
