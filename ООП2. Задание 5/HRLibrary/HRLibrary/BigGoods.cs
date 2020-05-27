using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLibrary
{
    public class BigGoods : Commodity
    {
        public int Length { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public BigGoods(string name, string articleNumber, CommodityUnits commodityUnit, double wholeselePrice, double retailPrice, int length, int height, int width) : base(name, articleNumber, commodityUnit, wholeselePrice, retailPrice)
        {
            Length = length;
            Height = height;
            Width = width;
        }

        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"Размеры товара: Длинна {Length}см, Высота {Height}см, Ширина {Width}см");
        }
    }
}
