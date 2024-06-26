﻿using System.Reflection;
using DbUp;

namespace p8_dbup
{
    class Program
    {
        static int Main(string[] args)
        {
            var connectionString = "Server=127.0.0.1;Port=3308;Database=p8-mobility;Uid=root;Pwd=password;";

            var sqlUpgrader =
                DeployChanges.To
                    .MySqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

            sqlUpgrader.PerformUpgrade();

            return 0;
        }
    }
}