using AutoBogus;
using HyperAtivaTeste.API.Settings;
using Xunit;

namespace HyperAtivaTeste.Tests.UnitTests.HyperAtivaTeste.API.Settings
{
    public class ApplicationInsightsSettingsTest
    {
        private readonly IAutoFaker _autoFaker;

        public ApplicationInsightsSettingsTest()
            => _autoFaker = AutoFaker.Create();

        [Fact]
        public void ApplicationInsightsSettings_Test()
        {
            var applicationInsights = new ApplicationInsightsSettings();
            Assert.IsType<ApplicationInsightsSettings>(applicationInsights);
        }

        [Fact]
        public void ApplicationInsightsSettings_InstrumentationKey_Test()
        {
            var applicationInsights = new ApplicationInsightsSettings(_autoFaker.Generate<string>());
            Assert.IsType<ApplicationInsightsSettings>(applicationInsights);
            Assert.IsType<string>(applicationInsights.InstrumentationKey);
        }
    }
}
