using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TractorTracker.DAL
{
    public class ConfigurationManager
    {

        public static DbContextOptions GetDbContext(ConfigurationMode mode)
        {
            string environment = mode == ConfigurationMode.TEST ? "Test" : "Prod";
            var builder = new ConfigurationBuilder();

            builder.AddUserSecrets<AppDbContext>();

            var config = builder.Build();
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(config[$"ConnectionStrings:TractorTracker{environment}"])
                .Options;
            return options;
        }

        public enum ConfigurationMode
        { 
            TEST,
            PROD
        }
    }
}
