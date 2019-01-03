using System;
using System.Configuration;
using System.Reflection;
using DbUp;
using DbUp.Engine;

namespace Database
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.AppSetting["ConnectionStrings:DBConnectionString"];
            EnsureDatabase.For.SqlDatabase(connectionString); //Creates database if not exist

            var upgradeEngineBuilder = DeployChanges.To
                .SqlDatabase(connectionString, null)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .WithTransaction()
                .LogToConsole();

            var upgrader = upgradeEngineBuilder.Build();
            if (upgrader.IsUpgradeRequired())
            {
                DatabaseUpgradeResult result = upgrader.PerformUpgrade();
            }
            Console.ReadLine();
        }
    }
}
