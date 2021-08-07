//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using TechTalk.SpecFlow;

//namespace CodatExam.Hooks
//{
//    public sealed class Hooks
//    {
//        public Hooks()
//        {

//        }

//        private static IConfiguration config;

//        [BeforeScenario]
//        public void CreateConfig()
//        {
//            if (config == null)
//            {
//                config = new ConfigurationBuilder().
//                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//                    .Build();
//            }

//            container.RegisterInstanceAs<IConfiguration>(config);
//        }
//    }
//}
