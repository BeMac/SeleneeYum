using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace SeleneeYum
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; set; }
        
        static void Main(string[] args)
        {
            var driverDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            
            using (var driver = new ChromeDriver(driverDirectory))
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
                GetConfigs();
                GoToGoogle(driver);
                //driver.Manage().Window.FullScreen();
                //FindLostPet(driver);
            }
        }

        private static string GoToGoogle(IWebDriver driver)
        {
            string googleUrl = "https://www.google.com/";
            driver.Navigate().GoToUrl(googleUrl);
            return googleUrl;
        }

        private static void GetConfigs()
        {
            //!!!NOTE in order to make this work, set appsettings.json properties to build to output directory!!!!
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
        }

        private static string FindLostPet(IWebDriver driver)
        {
            try
            {
                driver.Navigate().GoToUrl(Configuration["lostPetUrl"]);
                driver.FindElement(By.Id(Configuration["agreeCheckboxId"])).Click();
                driver.FindElement(By.Id(Configuration["startButtonId"])).Click();
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                //driver.Manage().Window.FullScreen();
                FillLostPetSectionA(driver);
                
                FillLostPetSectionB(driver);
                
                FillLostPetSectionC(driver);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            return Configuration["lostPetUrl"];
        }

        private static void FillLostPetSectionA(IWebDriver driver)
        {
            driver.FindElement(By.Id(Configuration["lostButtonId"])).Click();
            driver.FindElement(By.Id(Configuration["hiddenModalId"])).Click();
        }

        private static void FillLostPetSectionB(IWebDriver driver)
        {
            var zipCodeElement = driver.FindElement(By.Id(Configuration["zipCodeLookup"]));
            zipCodeElement.SendKeys(Configuration["zipCode"]);
            zipCodeElement.SendKeys(Keys.Return);
        }

        private static void FillLostPetSectionC(IWebDriver driver)
        {
            //We Can Get these Elements by ID
            var crossStreetElement1 = driver.FindElement(By.Name(Configuration["crossStreetName"]));
            //Or by Name
            var crossStreetElement2 = driver.FindElement(By.Id(Configuration["crossStreetID"]));

            crossStreetElement2.SendKeys("Sibley & 4th St E");
            driver.FindElement(By.Id(Configuration["nameSectionName"])).SendKeys("Martin");
            var speciesSelect = new SelectElement(driver.FindElement(By.Id(Configuration["speciesId"])));
            speciesSelect.SelectByText("Dog");
            var breedSelect = new SelectElement(driver.FindElement(By.Id(Configuration["breedId"])));
            breedSelect.SelectByText("Doberman Pinscher");
            var mixedWithSelect = new SelectElement(driver.FindElement(By.Id(Configuration["mixedWithId"])));
            mixedWithSelect.SelectByText("Chihuahua Short Haired");
            var sizeSelect = new SelectElement(driver.FindElement(By.Id(Configuration["sizeId"])));
            sizeSelect.SelectByText("Small (Under 20 lbs)");
            var ageSelect = new SelectElement(driver.FindElement(By.Id(Configuration["ageId"])));
            ageSelect.SelectByText("Young");
            driver.FindElement(By.Id(Configuration["genderId"])).Click();
            var primaryColorSelect = new SelectElement(driver.FindElement(By.Id(Configuration["primaryColorId"])));
            primaryColorSelect.SelectByText("Black");
            var secondaryColorSelect = new SelectElement(driver.FindElement(By.Id(Configuration["secondaryColorId"])));
            secondaryColorSelect.SelectByText("Tan -Apricot, Beige, Coffee, Cream, Fawn");

            driver.FindElement(By.Id(Configuration["contactEmailId"])).SendKeys("brianMcLaffin@mailinator.com");
            driver.FindElement(By.Id(Configuration["contactPhoneAreaCodeId"])).SendKeys("612");
            driver.FindElement(By.Id(Configuration["contactPhone1Id"])).SendKeys("555");
            driver.FindElement(By.Id(Configuration["contactPhone2Id"])).SendKeys("4444");
            driver.FindElement(By.Id(Configuration["firstNameId"])).SendKeys("Brian");
            driver.FindElement(By.Id(Configuration["LastNameId"])).SendKeys("McLaffin");
            driver.FindElement(By.Id(Configuration["emailId"])).SendKeys("brianMcLaffin@mailinator.com");
            driver.FindElement(By.Id(Configuration["passwordId"])).SendKeys("1234");
            driver.FindElement(By.Id(Configuration["passwordVerifyId"])).SendKeys("1234");

        }
    }
}