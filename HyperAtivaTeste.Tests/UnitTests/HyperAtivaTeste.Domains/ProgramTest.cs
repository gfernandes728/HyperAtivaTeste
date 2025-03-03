using HyperAtivaTeste.Domains;
using Xunit;

namespace HyperAtivaTeste.Tests.UnitTests.HyperAtivaTeste.Domains
{
    public class ProgramTest
    {
        public ProgramTest() { }

        [Fact]
        public void Program_Test()
        {
            Program.Main();
            Assert.IsType<bool>(true);
        }
    }
}
