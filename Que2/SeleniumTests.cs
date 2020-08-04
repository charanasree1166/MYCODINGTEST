using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Que2
{
   public class SeleniumTests
   {
      IWebDriver _browserDriver;
      [SetUp]
      public void startBrowser()
      {
         _browserDriver = new ChromeDriver("C:\\Users\\keerthi.katakam\\Downloads\\Personal\\Charana Sree\\Coding");
      }

      [Test]
      public void test()
      {
         _browserDriver.Url = "https://www.weightwatchers.com/us/";
         _browserDriver.Manage().Window.Maximize();

         String expectedtitle = "WW (Weight Watchers): Weight Loss & Wellness Help | WW USA";
         _browserDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(100);
         String actualtitle = _browserDriver.Title;

         Assert.AreEqual(expectedtitle, actualtitle);

         _browserDriver.FindElement(By.XPath("//*[@class='MenuItem_menu-item__inner-wrapper__1trJ0 MenuItem_menu-item__inner-wrapper--with-icon__2l1uq']/span[2]")).Click();
         expectedtitle = "Find WW Studios & Meetings Near You | WW USA";
         Thread.Sleep(1000);
         actualtitle = _browserDriver.Title;
         Assert.AreEqual(expectedtitle, actualtitle);

         _browserDriver.FindElement(By.XPath("//*[@class='input input-3TfT5']")).SendKeys("10011");
         _browserDriver.FindElement(By.XPath("//*[@class='ww button primary cta-1JqRp']")).Click();

         Thread.Sleep(1000);
         string titleOfFirstResult = _browserDriver.FindElement(By.XPath("//*[@class='linkContainer-1NkqM']")).Text;
         _browserDriver.FindElement(By.XPath("//*[@class='linkContainer-1NkqM']")).Click();
         Thread.Sleep(1000);
         string ValidateText = _browserDriver.FindElement(By.XPath("//*[@class='locationName-1jro_']")).Text;

         Assert.AreEqual(titleOfFirstResult, ValidateText);
         int d = (int)System.DateTime.Now.DayOfWeek;
         string result = PrintMeetings(d);
         Assert.IsNotNull(result);
      }

      public string PrintMeetings(int Day)
      {
         ReadOnlyCollection<IWebElement> elements = null;
         switch (Day)
         {
            case 1:
               elements = _browserDriver.FindElements(By.XPath("//*[@class='day-NhBOb'][2]/div"));
               break;
            case 2:
               elements = _browserDriver.FindElements(By.XPath("//*[@class='day-NhBOb'][3]/div"));
               break;
            case 3:
               elements = _browserDriver.FindElements(By.XPath("//*[@class='day-NhBOb'][4]/div"));
               break;
            case 4:
               elements = _browserDriver.FindElements(By.XPath("//*[@class='day-NhBOb'][5]/div"));
               break;
            case 5:
               elements = _browserDriver.FindElements(By.XPath("//*[@class='day-NhBOb'][6]/div"));
               break;
            case 6:
               elements = _browserDriver.FindElements(By.XPath("//*[@class='day-NhBOb'][7]/div"));
               break;
            case 7:
               elements = _browserDriver.FindElements(By.XPath("//*[@class='day-NhBOb'][1]/div"));
               break;
         }

         return print(elements);

      }

      private string print(ReadOnlyCollection<IWebElement> elements)
      {
         List<string> arr = new List<string>();
         List<int> freq = new List<int>();
         foreach (var elem in elements)
         {
            arr.Add(elem.Text.Split('\n')[1]);
            freq.Add(-1);
         }
         for (int i = 0; i < arr.Count; i++)
         {
            int count = 1;
            for (int j = i + 1; j < arr.Count; j++)
            {
               /* If duplicate element is found */
               if (arr[i] == arr[j])
               {
                  count++;
                  /* Make sure not to count frequency of same element again */
                  freq[j] = 0;
               }
            }
            /* If frequency of current element is not counted */
            if (freq[i] != 0)
            {
               freq[i] = count;
            }
         }

         /* Print frequency of each element */

         // printf(“nFrequency of all elements of array: n”);
         string s = string.Empty;
         for (int i = 0; i < arr.Count; i++)
         {
            if (freq[i] != 0)
            {
               s = s + arr[i] + " " + freq[i] + "\n";
               //printf(“% s occurs % d timesn”, arr[i], freq[i]);
            }
         }
         Console.WriteLine(s);
         return s;
      }

      [TearDown]
      public void closeBrowser()
      {
         _browserDriver.Close();
      }
   }
}
