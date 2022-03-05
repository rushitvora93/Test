using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace UI_TestProjekt.Helper
{
    public class ManufacturerUIHelper
    {
        public AppiumWebElement ManuView { get; set; }
        public AppiumWebElement ManuSMScrollViewer { get; set; }
        public AppiumWebElement ManuName { get; set; }
        public AppiumWebElement ManuPerson { get; set; }
        public AppiumWebElement ManuPhoneNumber { get; set; }
        public AppiumWebElement ManuFax { get; set; }
        public AppiumWebElement ManuStreet { get; set; }
        public AppiumWebElement ManuHouseNumber { get; set; }
        public AppiumWebElement ManuPlz { get; set; }
        public AppiumWebElement ManuCity { get; set; }
        public AppiumWebElement ManuCountry { get; set; }
        public AppiumWebElement ManuComment { get; set; }

        public ManufacturerUIHelper(AppiumWebElement pManuView,
                                    AppiumWebElement pManuSMScrollViewer,
                                    AppiumWebElement pManuName,
                                    AppiumWebElement pManuPerson,
                                    AppiumWebElement pManuPhoneNumber,
                                    AppiumWebElement pManuFax,
                                    AppiumWebElement pManuStreet,
                                    AppiumWebElement pManuHouseNumber,
                                    AppiumWebElement pManuPlz,
                                    AppiumWebElement pManuCity,
                                    AppiumWebElement pManuCountry,
                                    AppiumWebElement pManuComment
                                    )
        {
            ManuView = pManuView;
            ManuSMScrollViewer = pManuSMScrollViewer;
            ManuName = pManuName;
            ManuPerson = pManuPerson;
            ManuPhoneNumber = pManuPhoneNumber;
            ManuFax = pManuFax;
            ManuStreet = pManuStreet;
            ManuHouseNumber = pManuHouseNumber;
            ManuPlz = pManuPlz;
            ManuCity = pManuCity;
            ManuCountry = pManuCountry;
            ManuComment = pManuComment;
        }
    }
}
