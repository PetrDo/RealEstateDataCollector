using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateDataCollector
{
    class Apartment : Property
    {
        public int Size_m2 { get; private set; }



        public Apartment(PropertyTransaction transaction, string title, string location, string link, int price, DateTime marketEntryDate, DateTime marketExitDate, bool isOnMarketToday) : base(PropertyType.Apartment, transaction, title, location, link, price, marketEntryDate, marketExitDate, isOnMarketToday)
        {


            set_if_label_found("Novostavba", labels),
            set_if_label_found("Družstevní", labels),
            set_if_label_found("Balkon", labels),

        # <span ng-repeat="label in i.labels" class="label ng-binding ng-scope">Družstevní                </span>
        # <span ng-repeat="label in i.labels" class="label ng-binding ng-scope">Sklep                     </span>
        # <span ng-repeat="label in i.labels" class="label ng-binding ng-scope">Výtah                     </span>
        # <span ng-repeat="label in i.labels" class="label ng-binding ng-scope">Osobní vlastnictví        </span>
        # <span ng-repeat="label in i.labels" class="label ng-binding ng-scope">Lodžie   

        }
    }
}
