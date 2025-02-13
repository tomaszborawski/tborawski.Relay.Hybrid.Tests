using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tborawski.Relay.Hybrid.Client
{
    public static class ConfigBootstrap
    {
        private static IConfigurationRoot? _config;

        public static IConfigurationRoot ConfigRoot =>
            _config ?? throw new InvalidOperationException("Configuration not initialized");

        //inits configuration, so that we can do it once and use in other services
        //we need to do it via static as those classes are created by ASP.NET middleware and not via any IoC
        //so we can't inject anything in them
        public static void Init()
        {
            var cfgBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false);
            _config = cfgBuilder.AddEnvironmentVariables().Build();
        }

        public static T? GetValue<T>(string key) => ConfigRoot.GetValue<T>(key);

        public static T Read<T>(string key) where T : class, new()
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
            var obj = new T();
            ConfigRoot.Bind(key, obj);
            return obj;
        }
    }
}
