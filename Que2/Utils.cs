using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Que2
{
   public static class Utils
   {
      private static Dictionary<string, Locator> locators = new Dictionary<string, Locator>();
      public static Locator GetLocator(String name)
      {
         if (locators.Count == 0)
         {
            lock (locators)
            {
               // load all for one time
               XmlDocument objectRepository = new XmlDocument();
               //TODO: Assume ObjectRepository is always @ exe location. Set project build to deploy it to bin
               objectRepository.Load(Path.Combine("Objects.xml"));

               foreach (XmlNode page in objectRepository.SelectNodes("/PageFactory/page"))
               {
                  foreach (XmlNode eachObject in page.ChildNodes)
                  {
                     Locator locator = null;

                     switch (eachObject.SelectSingleNode("identifyBy").InnerText.ToLower())
                     {
                        case "id":
                           locator = Locator.Get(LocatorType.ID, eachObject.SelectSingleNode("value").InnerText);
                           break;

                        case "xpath":
                           locator = Locator.Get(LocatorType.XPath, eachObject.SelectSingleNode("value").InnerText);
                           break;

                        case "classname":
                           locator = Locator.Get(LocatorType.ClassName, eachObject.SelectSingleNode("value").InnerText);
                           break;

                        case "name":
                           locator = Locator.Get(LocatorType.Name, eachObject.SelectSingleNode("value").InnerText);
                           break;
                     }

                     locators.Add(eachObject.SelectSingleNode("name").InnerText, locator);
                  }
               }
            }
         }
         return locators[name];
      }

      internal static By GetBy(string v)
      {
         return GetLocator(v)?.GetBy();
      }

      internal static void PerformAction(IWebDriver browserDriver, string input)
      {
         Actions action1 = new Actions(browserDriver);
         action1.SendKeys(input);
         action1.Perform();
      }
   }

   public enum LocatorType
   {
      XPath,
      ID,
      ClassName,
      Name,
   }

   public class Locator
   {
      public Locator(LocatorType locatorType, String location)
      {
         this.Location = location;
         this.LocatorType = locatorType;
      }

      public static Locator Get(LocatorType locatorType, String location)
      {
         return new Locator(locatorType, location);
      }

      internal By GetBy()
      {
         By by = null;
         switch (this.LocatorType)
         {
            case LocatorType.XPath:
               by = By.XPath(this.Location);
               break;

            case LocatorType.ID:
               by = By.Id(this.Location);
               break;

            case LocatorType.Name:
               by = By.Name(this.Location);
               break;

            case LocatorType.ClassName:
               by = By.ClassName(this.Location);
               break;
         }

         return by;
      }

      public String Location { get; set; }
      public LocatorType LocatorType { get; set; }
   }
}
