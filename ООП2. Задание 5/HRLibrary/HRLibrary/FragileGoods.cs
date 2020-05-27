using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLibrary
{
    public class FragileGoods : Commodity
    {
        public int MaxCountInStack { get; set; }

        public FragileGoods(string name, string articleNumber, CommodityUnits commodityUnit, double wholeselePrice, double retailPrice, int maxCountInStack) : base(name, articleNumber, commodityUnit, wholeselePrice, retailPrice)
        {
            MaxCountInStack = maxCountInStack;
        }

        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"максимальное количество единиц товара в стопке: {MaxCountInStack}");
        }
    }
}
