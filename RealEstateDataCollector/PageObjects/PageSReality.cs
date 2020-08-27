using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using RealEstateDataCollector.PropertyObjects;

namespace RealEstateDataCollector.PageObjects
{
    public class PageSReality : BasePageObject
    {
        private List<BasePropertyObject> propertiesPulledFromPage = new List<BasePropertyObject>();

        private PropertyTransaction transactionType;
        private PropertyType propertyType;

        private Func<IWebElement, string, bool> ElementContainsByClassName = (_obj, _string) => _obj.FindElements(By.ClassName(_string)).Count > 0 ? true : false;
        private Func<IWebElement, By, string> GetTextBy = (_obj, _selector) => _obj.FindElement(_selector).Text;

        public PageSReality(IWebDriver driver) : base(driver) 
        {
            string _url = driver.Url;

            if (_url.Contains("/prodej/"))
            {
                transactionType = PropertyTransaction.Sale;
            }
            else if (_url.Contains("/pronajem/"))
            {
                transactionType = PropertyTransaction.Rent;
            }
            else
            {
                transactionType = PropertyTransaction.Unknown;
                Console.WriteLine("Unknown Property Transaction!");
            }

            if (_url.Contains("/byty/"))
            {
                propertyType = PropertyType.Apartment;
            }
            else if (_url.Contains("/domy/"))
            {
                propertyType = PropertyType.House;
            }
            else if (_url.Contains("/pozemky/"))
            {
                propertyType = PropertyType.Lot;
            }
            else
            {
                propertyType = PropertyType.Unknown;
                Console.WriteLine("Unknown Property Type!");
            }



        }

        public override List<BasePropertyObject> PullData()
        {
            Console.WriteLine("Pulling data from {0}", driver.Url);

            // <div class="property ng-scope">
            List<IWebElement> propertiesOnPage = new List<IWebElement>();
            propertiesOnPage = driver.FindElements(By.ClassName("property")).ToList();

            foreach (IWebElement property in propertiesOnPage)
            {
                //property.find_elements(By.CLASS_NAME, "tip-region")
                //List<IWebElement> _tmp = new List<IWebElement>(element.FindElements(By.ClassName("tip-region")));
                if (!ElementContainsByClassName(property, "tip-region"))
                {
                    TryParse(property);
                }
            }
            return propertiesPulledFromPage;
        }

        private void TryParse(IWebElement property)
        {      
            /*
<div class="property ng-scope" ng-style="{order: $index != 1 ? $index: $index + 1}" ng-include="'/templates/_property-list-item.html'" style="order: 2;">
    <a ng-href="/detail/prodej/byt/2+kk/praha-michle-bohdalecka/1237700188" class="images count3 clear" href="/detail/prodej/byt/2+kk/praha-michle-bohdalecka/1237700188">
	    <span class="image-wrap ng-scope" ng-repeat="img in i.images track by $index">
		    <img ng-src="https://d18-a.sdn.cz/d_18/c_img_G_BY/JDBBw96.jpeg?fl=res,400,300,3|shr,,20|jpg,90" class="img" src="https://d18-a.sdn.cz/d_18/c_img_G_BY/JDBBw96.jpeg?fl=res,400,300,3|shr,,20|jpg,90">
	    </span>
        <span class="image-wrap ng-scope" ng-repeat="img in i.images track by $index">
		    <img ng-src="https://d18-a.sdn.cz/d_18/c_img_G_BY/SmOBw97.jpeg?fl=res,400,300,3|shr,,20|jpg,90" class="img" src="https://d18-a.sdn.cz/d_18/c_img_G_BY/SmOBw97.jpeg?fl=res,400,300,3|shr,,20|jpg,90">
	    </span>
        <span class="image-wrap ng-scope" ng-repeat="img in i.images track by $index">
		    <img ng-src="https://d18-a.sdn.cz/d_18/c_img_G_BY/Eu3Bw98.jpeg?fl=res,400,300,3|shr,,20|jpg,90" class="img" src="https://d18-a.sdn.cz/d_18/c_img_G_BY/Eu3Bw98.jpeg?fl=res,400,300,3|shr,,20|jpg,90">
	    </span>
    </a>

    <div class="info clear ng-scope" ng-class="{'project-info': type == 'project', 'with-logo': i.showAgencyLogo, 'with-project-items': i.projectDates &amp;&amp; i.projectDates.length, 'with-guide-btn': type == 'favorite'}">
	    <div class="text-wrap">
		    <span class="basic">
			    <h2>
				    <a ng-href="/detail/prodej/byt/2+kk/praha-michle-bohdalecka/1237700188" ng-click="beforeOpen(i.iterator, i.regionTip)" class="title" href="/detail/prodej/byt/2+kk/praha-michle-bohdalecka/1237700188">
					    <span class="name ng-binding">Prodej bytu 2+kk 76&nbsp;m²</span>
				    </a>
			    </h2>
			    <span class="locality ng-binding">Bohdalecká, Praha 10 - Michle</span>
			    <span class="price ng-scope" ng-if="i.price">
				    <span class="norm-price ng-binding">6&nbsp;698&nbsp;042&nbsp;Kč</span>
			    </span>
			    <span class="labels ng-scope" ng-if="i.labels.length || i.hasPanorama || i.hasGroundPlan || i.hasVirtualTour">
                    <span ng-if="i.hasGroundPlan" class="label highlight ng-binding ng-scope">
					    Půdorys
				    </span>
			    </span>
		    </span>
	    </div>
    </div>
</div>
            */

            string title;
            string link;
            string location;
            // int price;

            int size_m2 = 0;
            Rooms rooms = Rooms._unknown;                                   // Done
            Features features = Features.Nothing;


            try
            {
                title = GetTextBy(property, By.CssSelector("h2 > a"));
                link = property.FindElement(By.CssSelector("h2 > a")).GetAttribute("href");
                location = GetTextBy(property, By.ClassName("locality"));
                int.TryParse(GetTextBy(property, By.ClassName("price")).Replace(" ", "").Replace("Kč", ""), out int price);

                // size m2
                //Rooms
                //Features

                if (propertyType == PropertyType.Apartment)
                {
                    propertiesPulledFromPage.Add(new Apartment(
                        transactionType,
                        title,
                        location,
                        link,
                        price,
                        size_m2, // size m2
                        rooms,
                        features
                    ));
                }
                else
                {
                    throw new NotImplementedException();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {

            }


            //string[] titleParts = title.Split(' ');            

            //bytu 2+kk 42 m²
            //bytu 2+kk 66 m²
            //rodinného domu 223 m², pozemek 378 m²
            //vily 433 m², pozemek 400 m²
            //chalupy 73 m², pozemek 679 m²
            //Prodej bytu 6 pokojů a více 325 m²
            //Prodej bytu atypické 151 m²
            /*
            if (titleParts.Length < 5)
            {
                transaction = GetPropertyTransaction(titleParts[0]);


                if(titleParts[1] == "bytu")
                {
                    propertyType = PropertyType.Apartment;

                    switch (titleParts[2])
                    {
                        case "1+kk":
                            rooms = Rooms._1_kk;
                            break;
                        case "1+1":
                            rooms = Rooms._1_1;
                            break;
                        case "2+kk":
                            rooms = Rooms._2_kk;
                            break;
                        case "2+1":
                            rooms = Rooms._2_1;
                            break;
                        case "3+kk":
                            rooms = Rooms._3_kk;
                            break;
                        case "3+1":
                            rooms = Rooms._3_1;
                            break;
                        case "4+kk":
                            rooms = Rooms._4_kk;
                            break;
                        case "4+1":
                            rooms = Rooms._4_1;
                            break;
                        case "5+kk":
                            rooms = Rooms._5_kk;
                            break;
                        case "5+1":
                            rooms = Rooms._5_1;
                            break;
                        case "6+kk":
                            rooms = Rooms._6_plus;
                            break;
                        case "6+1":
                            rooms = Rooms._atypical;
                            break;
                        default:
                            rooms = Rooms._unknown;
                            //Console.WriteLine("Parsing failed \"{0}\"", title);
                            break;
                    }
                }
            }
            else
            {
                throw new NotImplementedException();
            }
                        */


            //properties.Add(new Apartment(PropertyTransaction.Sale, title, location, link, price, DateTime.Today, DateTime.Today, true, size_m2, Features.Balcony));
        }


        /*
                def pull_data(webdriver):

            data = []

            for property in properties_on_page:

                o_transaction = "Not Assigned"
                o_type = "Not Assigned"
                o_m2 = 0


                try:
                    if "Prodej bytu" in header_string:
                        tmp = header_string.split(" ")
                        o_transaction = header_string[:11]
                        o_type = tmp[2]
                        o_m2 = tmp[3]
                except Exception as e:
                    print(e.args)





        # <span ng-repeat="label in i.labels" class="label ng-binding ng-scope">Družstevní                </span>
        # <span ng-repeat="label in i.labels" class="label ng-binding ng-scope">Sklep                     </span>
        # <span ng-repeat="label in i.labels" class="label ng-binding ng-scope">Výtah                     </span>
        # <span ng-repeat="label in i.labels" class="label ng-binding ng-scope">Osobní vlastnictví        </span>
        # <span ng-repeat="label in i.labels" class="label ng-binding ng-scope">Lodžie                    </span>

                property_labels = property.find_elements(By.CLASS_NAME, "label")
                labels = []
                [labels.append(label.text) for label in property_labels]

                data.append([
                    o_transaction,
                    o_type,
                    o_m2,
                    o_price,
                    o_locality,
                    set_if_label_found("Novostavba", labels),
                    set_if_label_found("Družstevní", labels),
                    set_if_label_found("Balkon", labels),
                   ])

                # Log remaining labels
                if labels: logger.warning("Property labels not stored in DB:" + ' '.join(map(str, labels)))

                    */


    }
}
