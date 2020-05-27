using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLibrary
{
    public class PerishableGoods : Commodity
    {
        public int MaxShelfLife { get; set; }

        public PerishableGoods(string name, string articleNumber, CommodityUnits commodityUnit, double wholeselePrice, double retailPrice, int maxShelfLife) : base(name, articleNumber, commodityUnit, wholeselePrice, retailPrice)
        {
            MaxShelfLife = maxShelfLife;
        }

        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"Срок годности: {MaxShelfLife} дней");
        }
    }
}
