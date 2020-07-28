using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateDataCollector.PropertyObjects
{
    public enum PropertyType { Apartment, House, Lot }
    public enum PropertyTransaction { Sale, Rent }


    abstract class BasePropertyObject
    {
        public PropertyType Type { get; private set; }
        public PropertyTransaction Transaction { get; private set; }
        public string Title { get; private set; }
        public string Location { get; private set; }
        public string Link { get; private set; }
        public int Price { get; private set; }
        public DateTime MarketEntryDate { get; private set; }
        public DateTime MarketExitDate { get; private set; }
        public bool IsOnMarketToday { get; private set; }
        public TimeSpan DaysOnMarket { get => MarketExitDate - MarketEntryDate + TimeSpan.FromDays(1); }

        public BasePropertyObject(PropertyType type, PropertyTransaction transaction, string title, string location, string link, int price, DateTime marketEntryDate, DateTime marketExitDate, bool isOnMarketToday)
        {
            Type = type;
            Transaction = transaction;
            Title = title;
            Location = location;
            Link = link;
            Price = price;
            MarketEntryDate = marketEntryDate;
            MarketExitDate = marketExitDate;
            IsOnMarketToday = isOnMarketToday;
        }

        abstract public int PricePerMeter();
    }
}
