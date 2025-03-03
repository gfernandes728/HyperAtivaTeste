using HyperAtivaTeste.Infra.Services;
using Xunit;

namespace HyperAtivaTeste.Tests.UnitTests.HyperAtivaTeste.Infra.Services
{
    public class LinkedInCSharpServiceTest
    {
        public LinkedInCSharpServiceTest() { }

        [Fact]
        public void Question16_Test()
            => Assert.IsType<List<string>>(new LinkedInCSharpService().Question16());
    }
}
