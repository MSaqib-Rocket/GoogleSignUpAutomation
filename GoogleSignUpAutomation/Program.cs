using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace GoogleSignUpAutomation
{
    public static class Program
    {
        static void Main(string[] args)
        {

            //Getting Emails From JSON file
            string jsonFilePath = "D:\\Saqib\\DesktopApplications\\GoogleSignUpAutomation\\GoogleAccounts.json";

            // Read the JSON file content
            string jsonContent = File.ReadAllText(jsonFilePath);

            // Deserialize the JSON data into a list of Account objects
            List<Account> accounts = JsonConvert.DeserializeObject<List<Account>>(jsonContent);

            // Now, you have a list of Account objects that you can iterate over.
            foreach (var account in accounts)
            {
                Console.WriteLine($"Email: {account.Email}, Password: {account.Password}");
            }
            Console.ReadLine();


            AppiumOptions options = new AppiumOptions();
            options.AddAdditionalCapability("platformName", "Android");
            options.AddAdditionalCapability("deviceName", "MemuPlay"); // Emulator name
            //options.AddAdditionalCapability("appPackage", "com.spotify.music"); // Package name of the app you want to install
            //options.AddAdditionalCapability("appActivity", "com.spotify.music.MainActivity"); // Activity name of the app
            options.AddAdditionalCapability("automationName", "UiAutomator2");
            //options.AddAdditionalCapability("app", "D:\\Saqib\\Downloads\\Spotify.apk");

            AndroidDriver<AndroidElement> driver = new AndroidDriver<AndroidElement>(new Uri("http://localhost:4723/wd/hub"), options);

            Thread.Sleep(3000);

            var contexts = ((IContextAware)driver).Contexts;
            string webViewContext = null;
            for (int i = 0; i < contexts.Count; i++)
            {
                Console.WriteLine(contexts[i]);
                if (contexts[i].Contains("WEBVIEW"))
                {
                    webViewContext = contexts[i];
                    break;
                }
            }

            ((IContextAware)driver).Context = webViewContext;

            //Settings Button Click
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.widget.TextView[3]")).Click();
            
            Thread.Sleep(1000);
            // Scroll down the screen
            int scrollDistance = 1; // You can adjust this value as needed
            driver.Scroll(scrollDistance);

            //Click on acocunts
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.support.v4.widget.DrawerLayout/android.widget.LinearLayout/android.widget.FrameLayout[2]/android.widget.FrameLayout/android.support.v4.view.ViewPager/android.support.v7.widget.RecyclerView/android.widget.LinearLayout[4]/android.widget.LinearLayout/android.widget.TextView")).Click();

            //Click on add Account
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.support.v4.widget.DrawerLayout/android.widget.LinearLayout/android.widget.FrameLayout[2]/android.widget.LinearLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.support.v7.widget.RecyclerView/android.widget.LinearLayout/android.widget.RelativeLayout/android.widget.TextView")).Click();

            //Enter Emails 
            foreach (var account in accounts)
            {
                Thread.Sleep(4000);
                driver.FindElement(By.XPath("//android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.webkit.WebView/android.webkit.WebView/android.view.View/android.view.View/android.view.View[2]/android.view.View/android.view.View[1]/android.view.View/android.widget.EditText")).SendKeys(account.Email);


                Thread.Sleep(2000);
                //next btn
                driver.FindElement(By.XPath("//android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.webkit.WebView/android.webkit.WebView/android.view.View/android.view.View/android.view.View[3]/android.view.View/android.widget.Button")).Click();

                Thread.Sleep(5000);
                //Enter Password
                driver.FindElement(By.XPath("//android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.webkit.WebView/android.webkit.WebView/android.view.View/android.view.View/android.view.View[2]/android.view.View/android.view.View[1]/android.view.View/android.view.View/android.view.View/android.widget.EditText")).SendKeys(account.Password);

                Thread.Sleep(1000);

                //Next btn
                driver.FindElement(By.XPath("//android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.webkit.WebView/android.webkit.WebView/android.view.View/android.view.View[1]/android.view.View[3]/android.view.View/android.widget.Button")).Click();

                Thread.Sleep(2000);
                //agree button
                driver.FindElement(By.XPath("//android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.webkit.WebView/android.webkit.WebView/android.view.View/android.view.View[1]/android.view.View[3]/android.view.View/android.widget.Button")).Click();


                Thread.Sleep(6000);
                //accept button
                driver.FindElement(By.XPath("//android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.LinearLayout/android.widget.Button")).Click();

            }


        }
        public static void Scroll(this AndroidDriver<AndroidElement> driver, int scrollDistance)
        {
            int startX = driver.Manage().Window.Size.Width / 2;
            int startY = driver.Manage().Window.Size.Height * 3 / 4; // Start from 3/4th down the screen
            int endY = driver.Manage().Window.Size.Height / 4; // Scroll to 1/4th up the screen

            TouchAction touchAction = new TouchAction(driver);
            for (int i = 0; i < scrollDistance; i++)
            {
                touchAction.Press(startX, startY).Wait(200).MoveTo(startX, endY).Release().Perform();
                Thread.Sleep(1000); // Adjust sleep duration as needed
            }
        }
    }
}
