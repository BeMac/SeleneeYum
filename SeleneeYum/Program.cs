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

                GoToGoogle(driver);

                FindLostPet(driver);
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

            //var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("Config.json", true, true);
            //Configuration = builder.Build();
            //var happy = Configuration.GetSection("ConnectionStrings");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            var happy = config["name"];

        }
        private static string FindLostPet(IWebDriver driver)
        {
            var lostPetUrl = "https://www.helpinglostpets.com/petdetail/PostPetMStart.aspx";
            var agreeCheckboxId = "MainContent_chkAgree";
            var startButtonId = "MainContent_UpdatePanel_SaveButton";
            try
            {
                driver.Navigate().GoToUrl(lostPetUrl);
                driver.FindElement(By.Id(agreeCheckboxId)).Click();
                driver.FindElement(By.Id(startButtonId)).Click();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            return lostPetUrl;
        }

        private string FillInDetails()
        {
            return string.Empty;
        }
    }
}
