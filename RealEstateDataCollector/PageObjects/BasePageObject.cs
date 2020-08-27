using OpenQA.Selenium;
using RealEstateDataCollector.PropertyObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateDataCollector.PageObjects
{
    public abstract class BasePageObject
    {
        protected IWebDriver driver;

        public BasePageObject(IWebDriver driver)
        {
            this.driver = driver;
        }

        public abstract List<BasePropertyObject> PullData();
    }
}
