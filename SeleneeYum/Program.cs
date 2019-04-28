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
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

                FillLostPetSectionA(driver);
                
                FillLostPetSectionB(driver);
                
                FillLostPetSectionC(driver);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            return lostPetUrl;
        }

        private static void FillLostPetSectionA(IWebDriver driver)
        {
            var lostButtonId = "MainContent_radBtnListStatus_0";
            var hiddenModalId = "hideModalPopupViaClientButton";

            driver.FindElement(By.Id(lostButtonId)).Click();
            driver.FindElement(By.Id(hiddenModalId)).Click();
        }

        private static void FillLostPetSectionB(IWebDriver driver)
        {
            var zipCodeLookup = "txtLocateMap";
            var zipCode = "55423";

            var zipCodeElement = driver.FindElement(By.Id(zipCodeLookup));
            zipCodeElement.SendKeys(zipCode);
            zipCodeElement.SendKeys(Keys.Return);
        }

        private static void FillLostPetSectionC(IWebDriver driver)
        {
            var crossStreetName = "ctl00$MainContent$txtClosestIntersection";
            var crossStreetID = "MainContent_txtClosestIntersection";
            var nameSectionName = "MainContent_txtName";
            var speciesId = "MainContent_ddlSpecies";
            var breedId = "MainContent_ddlBreed1";
            var mixedWithId = "MainContent_ddlBreed2";
            var sizeId = "MainContent_ddlSizes";
            var ageId = "MainContent_ddlAges";
            var genderId = "MainContent_radLstGender_0";
            var primaryColorId = "MainContent_ddlPetColor1";
            var secondaryColorId = "MainContent_ddlPetColor2";
            var contactEmailId = "MainContent_txtPosterContact";
            var contactPhoneAreaCodeId = "MainContent_txtPhoneArea";
            var contactPhone1Id = "MainContent_txtPhone1";
            var contactPhone2Id = "MainContent_txtPhone2";
            var firstNameId = "MainContent_txtFName";
            var LastNameId = "MainContent_txtLName";
            var emailId = "MainContent_txtUserEmail";
            var passwordId = "MainContent_txtPass";
            var passwordVerifyId = "MainContent_txtPassConfirm";

            //We Can Get these Elements by ID
            var crossStreetElement1 = driver.FindElement(By.Name(crossStreetName));
            //Or by Name
            var crossStreetElement2 = driver.FindElement(By.Id(crossStreetID));

            crossStreetElement2.SendKeys("Sibley & 4th St E");
            driver.FindElement(By.Id(nameSectionName)).SendKeys("Martin");
            var speciesSelect = new SelectElement(driver.FindElement(By.Id(speciesId)));
            speciesSelect.SelectByText("Dog");
            var breedSelect = new SelectElement(driver.FindElement(By.Id(breedId)));
            breedSelect.SelectByText("Doberman Pinscher");
            var mixedWithSelect = new SelectElement(driver.FindElement(By.Id(mixedWithId)));
            mixedWithSelect.SelectByText("Chihuahua Short Haired");
            var sizeSelect = new SelectElement(driver.FindElement(By.Id(sizeId)));
            sizeSelect.SelectByText("Small (Under 20 lbs)");
            var ageSelect = new SelectElement(driver.FindElement(By.Id(ageId)));
            ageSelect.SelectByText("Young");
            driver.FindElement(By.Id(genderId)).Click();
            var primaryColorSelect = new SelectElement(driver.FindElement(By.Id(primaryColorId)));
            primaryColorSelect.SelectByText("Black");
            var secondaryColorSelect = new SelectElement(driver.FindElement(By.Id(secondaryColorId)));
            secondaryColorSelect.SelectByText("Tan -Apricot, Beige, Coffee, Cream, Fawn");

            driver.FindElement(By.Id(contactEmailId)).SendKeys("brianMcLaffin@mailinator.com");
            driver.FindElement(By.Id(contactPhoneAreaCodeId)).SendKeys("612");
            driver.FindElement(By.Id(contactPhone1Id)).SendKeys("555");
            driver.FindElement(By.Id(contactPhone2Id)).SendKeys("4444");
            driver.FindElement(By.Id(firstNameId)).SendKeys("Brian");
            driver.FindElement(By.Id(LastNameId)).SendKeys("McLaffin");
            driver.FindElement(By.Id(emailId)).SendKeys("brianMcLaffin@mailinator.com");
            driver.FindElement(By.Id(passwordId)).SendKeys("1234");
            driver.FindElement(By.Id(passwordVerifyId)).SendKeys("1234");

        }

        private string FillInDetails()
        {
            return string.Empty;
        }
    }
}
