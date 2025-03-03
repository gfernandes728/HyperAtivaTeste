using HyperAtivaTeste.Infra;
using Xunit;

namespace HyperAtivaTeste.Tests.UnitTests.HyperAtivaTeste.Infra
{
    public class DbDapperContextTest
    {
        [Fact]
        public void DbDapperContext_Test()
        {
            var db = new DbDapperContext(MockConfiguration());
            Assert.NotNull(db.DapperConnection);
        }

        private IConfiguration MockConfiguration()
            => new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string> { { "ConnectionStrings:DefaultConnection", "Server=localhost;DataBase=dbProjetoTeste;Integrated Security=SSPI;TrustServerCertificate=True" } }).Build();
    }
}
