using OpenQA.Selenium.Chrome;
using RealEstateDataCollector.PageObjects;
using RealEstateDataCollector.PropertyObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateDataCollector
{
    class Program
    {
        static void Main(string[] args)
        {
            string URL = "https://www.sreality.cz/hledani/prodej/byty/praha";
            ChromeDriver driver;
            PageSReality page;

            ChromeOptions options = new ChromeOptions();
            //options.AddArgument("--headless");
            //options.AddArgument("--window-size=1300,1000");

            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), options);
            //driver.Manage().Window.Size = new Size(650, 500);
            // top left of the primary monitor
            //driver.Manage().Window.Position = new Point(0, 0);

            driver.Url = URL;

            page = new PageSReality(driver);

            List<BasePropertyObject> data = new List<BasePropertyObject>();

            data.AddRange(page.PullData());

            driver.Close();



            //BasePropertyObject p = new Apartment(PropertyTransaction.Rent, "xxx", "xxx", "xxx", 12000, DateTime.Today, DateTime.Today, true, 32, Features.Balcony | Features.Cellar);
            Console.WriteLine("");
            Console.ReadKey();
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
    start_time = time.time()

    # when using the "with" keywrod automatically quit the driver at the end of execution
    # replaces try: {} finally: {driver.quit()}
    #with webdriver.PhantomJS(pythojJSDriverPath) as driver:
    with webdriver.Chrome(CHROMEDRIVER_PATH) as myWebDriver:     #, chrome_options=options

        url = "https://www.sreality.cz/hledani/prodej/byty/praha?velikost=1%2B1" #+ "&strana=" + str(page) #+ "&stari=dnes"
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
