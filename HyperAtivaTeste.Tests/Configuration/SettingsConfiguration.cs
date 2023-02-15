namespace HyperAtivaTeste.Tests.Configuration
{
    public class SettingsConfiguration
    {
        public static IConfigurationRoot GetConfigurationRoot =>
          new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.Test.json",
                  optional: true,
                  reloadOnChange: true)
              .Build();
    }
}
