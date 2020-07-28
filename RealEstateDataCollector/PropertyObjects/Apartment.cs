using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateDataCollector.PropertyObjects
{
    [Flags]
    public enum Features
    {
        Nothing = 0,

        New = 1,
        Concrete = 2,
        AfterRenovation = 4,
        InConstruction = 8,

        PrivateOwnership = 16,

        Balcony = 32,
        Loggia = 64,
        Terrace = 128,
        Cellar = 256,
        Garage = 512,
        Parking = 1024,

        Elevator = 2048
    }

    class Apartment : BasePropertyObject
    {
        public int Size_m2 { get; private set; }
        public Features Features { get; private set; }


        public Apartment(PropertyTransaction transaction, string title, string location, string link, int price, DateTime marketEntryDate, DateTime marketExitDate, bool isOnMarketToday, int size_m2, Features features) : base(PropertyType.Apartment, transaction, title, location, link, price, marketEntryDate, marketExitDate, isOnMarketToday)
        {
            Size_m2 = size_m2;
            Features = features;
        }

        public override int PricePerMeter()
        {
            return Price / Size_m2;
        }
    }
}
