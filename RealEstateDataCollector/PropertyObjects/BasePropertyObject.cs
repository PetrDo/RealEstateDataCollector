using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateDataCollector.PropertyObjects
{
    public enum PropertyType { Unknown, Apartment, House, Lot }
    public enum PropertyTransaction { Unknown, Sale, Rent }


    public abstract class BasePropertyObject
    {
        protected PropertyType Type { get; private set; }
        protected PropertyTransaction Transaction { get; private set; }
        protected string Title { get; private set; }
        protected string Location { get; private set; }
        protected string Link { get; private set; }
        protected int Price { get; private set; }
        //protected DateTime MarketEntryDate { get; private set; }
        //protected DateTime MarketExitDate { get; private set; }
        //protected bool IsOnMarketToday { get; private set; }
        //protected TimeSpan DaysOnMarket { get => MarketExitDate - MarketEntryDate + TimeSpan.FromDays(1); }

        //public BasePropertyObject(PropertyType type, PropertyTransaction transaction, string title, string location, string link, int price, DateTime marketEntryDate, DateTime marketExitDate, bool isOnMarketToday)
        public BasePropertyObject(PropertyType type, PropertyTransaction transaction, string title, string location, string link, int price)
        {
            Type = type;
            Transaction = transaction;
            Title = title;
            Location = location;
            Link = link;
            Price = price;
            //MarketEntryDate = marketEntryDate;
            //MarketExitDate = marketExitDate;
            //IsOnMarketToday = isOnMarketToday;
        }

        public abstract int PricePerMeter();

    }
}
