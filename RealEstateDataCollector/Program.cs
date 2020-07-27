using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateDataCollector
{
    class Program
    {
        static void Main(string[] args)
        {
            Property p = new Apartment(PropertyTransaction.Rent, "xxx", "xxx", "xxx", 12000, DateTime.Today, DateTime.Today, true);
            Console.WriteLine("");

        }
    }
}


//
/*
from selenium import webdriver
from selenium.webdriver.chrome.options import Options
from selenium.webdriver.common.by import By

from logger import logger

import bs4 as bs
import sys
import time

#import numpy as np
import pandas as pd


def time_convert(sec):
    mins = sec // 60
    sec = sec % 60
    hours = mins // 60
    mins = mins % 60
    print("Time Lapsed = {0}:{1}:{2}".format(int(hours),int(mins),sec))

def click_to_next_page(webdriver):
    
    # Click to next page only if next button is displayed and available
    for elem in webdriver.find_elements_by_css_selector("li > a"):
        if elem.get_attribute("class") == "btn-paging-pn icof icon-arr-right paging-next":
            # using of get() rather then click() since it waits for the page to load
            webdriver.get(elem.get_attribute("href"))
            #elem.click()
            return True
    return False

    # def goTo( url ):
    #if "errorPageContainer" in [ elem.get_attribute("id") for elem in driver.find_elements_by_css_selector("body > div") ]:
    #    raise Exception( "this page is an error" )
    #else:
    #    driver.get( url )
 
def pull_data(webdriver):

    data = []

    logger.info('Pulling data from ' + webdriver.current_url)

    #<div class="property ng-scope">
    properties_on_page = webdriver.find_elements(By.CLASS_NAME, "property")

    for property in properties_on_page:

        o_transaction = "Not Assigned"
        o_type = "Not Assigned"
        o_m2 = 0

        # <h2>
        #   <a ng-href="/detail/prodej/byt/1+1/praha-krc-bernolakova/1416506972" class="title" href="/detail/prodej/byt/1+1/praha-krc-bernolakova/1416506972">
        #     <span class="name ng-binding">Prodej bytu 1+1&nbsp;43&nbsp;m²</span>
        #   </a>
        # </h2>
        header_string = property.find_element(By.CSS_SELECTOR, "h2 > a > span.name").text

        try:
            if "Prodej bytu" in header_string:
                tmp = header_string.split(" ")
                o_transaction = header_string[:11]
                o_type = tmp[2]
                o_m2 = tmp[3]
        except Exception as e:
            print(e.args)

        #<span class="price ng-scope" ng-if="i.price">
        #    <span class="norm-price ng-binding">2&nbsp;580&nbsp;000&nbsp;Kč</span>
        o_price = property.find_element(By.CLASS_NAME, "price").text.replace(" ", "").replace("Kč", "")
                
        # <span class="locality ng-binding">Bernolákova, Praha - Krč</span>
        o_locality = property.find_element(By.CLASS_NAME, "locality").text

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
            set_if_label_found("Novostavba",  labels),
            set_if_label_found("Družstevní",  labels),
            set_if_label_found("Balkon",      labels),
           ])

        # Log remaining labels
        if labels: logger.warning("Property labels not stored in DB:" + ' '.join(map(str, labels)))
            

#                        <h2>
#                            <a ng-href="/detail/prodej/byt/1+1/praha-krc-bernolakova/1416506972" class="title" href="/detail/prodej/byt/1+1/praha-krc-bernolakova/1416506972">
#                                <span class="name ng-binding">Prodej bytu 1+1&nbsp;43&nbsp;m²</span>
#                            </a>
#                        </h2>
        #title = property.find_element(By.CSS_SELECTOR, "h2 > a.title")
        #print(title.text)
        #link = title.get_attribute("href")

        #print(link)

        #tip = property.find_elements(By.CLASS_NAME, "tip-region")

        #data.append([price.text, locality.text, link])
        #print(tip.text)

    return data 



    #elem = webdriver.find_elements_by_css_selector("span.basic")
    
    #for elem in webdriver.find_elements_by_css_selector("span.basic"):   
     #   string = elem.text
        #exclude class tip-region


    #    print(string)


def set_if_label_found(text, labels):
    ''' Return True and remove the text from labels if detected, othervise return False '''
    #set_if_label_found = lambda value, labels_list: True if (value in labels_list) else False
    if (text in labels):
        labels.remove(text)
        return True
    else:
        return False



def main():
    
    #pythojJSDriverPath = "C:/Program Files (x86)/Microsoft Visual Studio/Shared/Python37_64/Lib/site-packages/phantomjs/phantomjs-2.1.1-windows/bin/phantomjs.exe"    
    CHROMEDRIVER_PATH = "C:/Program Files (x86)/Microsoft Visual Studio/Shared/Python37_64/Lib/site-packages/chromedriver/chromedriver.exe"
    options = Options()
    options.headless = True

    start_time = time.time()

    # when using the "with" keywrod automatically quit the driver at the end of execution
    # replaces try: {} finally: {driver.quit()}
    #with webdriver.PhantomJS(pythojJSDriverPath) as driver:
    with webdriver.Chrome(CHROMEDRIVER_PATH) as myWebDriver:     #, chrome_options=options

        url = "https://www.sreality.cz/hledani/prodej/byty/praha?velikost=1%2B1" #+ "&strana=" + str(page) #+ "&stari=dnes"
        
        #driver.set_window_size(100, 100)
        # top left of the primary monitor
        #driver.set_window_position(200, 200)
        # driver.maximize_window()
        # driver.minimize_window()
        # driver.fullscreen_window()
        myWebDriver.get(url)



        #next_page_available = True
        #while next_page_available:                      
        #    pull_data(myWebDriver)
        #    next_page_available = click_to_next_page(myWebDriver)

        #dict = {'price': price, 'locality': locality, 'link': link}
        #dataFrame = pd.DataFrame(dict)

        realityData = []

        
        while True:
            realityData += pull_data(myWebDriver)
            #realityData.append(pull_data(myWebDriver))
            #realityData.extend(pull_data(myWebDriver))
            if not click_to_next_page(myWebDriver): break
        
        

        end_time = time.time()
        time_lapsed = end_time - start_time
        time_convert(time_lapsed)

        df = pd.DataFrame(realityData)
        df.to_csv('file.csv', sep=',', index = False, encoding = 'utf-8') #\t  #, columns = ['id','xx', 'xx', 'xx']

        print(df)

        print(df.describe())
            
        #time.sleep(1)

        print('')
    

        myWebDriver.close()


    #totalAddCount = int(soup.find_all(class_='info ng-binding')[0].contents[3].string)
    #totalPages = totalAddCount//20+1 if totalAddCount%20 else totalAddCount//20
    #actualPage = 1

    #print('Total {} adds found on {} pages'.format(totalAddCount, totalPages))
    
        #while actualPage < totalPages:
        #    actualPage += 1
        #    
        #    xpage = WebPage(_url)
        #    print(_url)

            #time.sleep(5)
            #_soup = bs.BeautifulSoup(_page.html, 'html.parser')
            #print(soup.find_all(class_='norm-price').text)

    #js_test = soup.find('p', class_='jstest')
    #print (js_test.text)
    #print(soup.prettify())


if __name__ == '__main__': main()
 */
