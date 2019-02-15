using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using Microsoft.Extensions.Configuration;

namespace GrowthWheel_AutoTests.Configuration
{
    public class DriverFactory
    {
        private readonly string browserType;

        public static DriverFactory Instance {get;}

        private static IConfiguration config;

        static DriverFactory()
        {
            // Read configs
            config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional:true, reloadOnChange:true)
                .Build();

            Instance = new DriverFactory(config["browser"].ToString());
        }

        public DriverFactory(string browserType)
        {
            this.browserType = browserType;
        }

        public RemoteWebDriver Create()
        {
            try {
                switch(browserType)
                {
                    case "chrome":
                    {
                        ChromeOptions options = new ChromeOptions();
                            // options.AddArguments("headless");

                        return new ChromeDriver(config["chrome_driver_path"].ToString(), options);
                    }
                                        
                }
            }
            catch(Exception e) {
                System.Console.WriteLine($"Exception occured while starting {browserType}: {e}");
            }

            throw new NotSupportedException($"Driver type {browserType} not supported");
        }
    }
}