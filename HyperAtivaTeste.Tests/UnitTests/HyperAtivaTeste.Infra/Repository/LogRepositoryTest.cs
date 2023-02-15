using AutoBogus;
using Moq;
using HyperAtivaTeste.Infra;
using HyperAtivaTeste.Infra.Repository;
using Xunit;
using HyperAtivaTeste.Domains.Models;
using System.Data.Common;
using Moq.Dapper;
using Dapper;

namespace HyperAtivaTeste.Tests.UnitTests.HyperAtivaTeste.Infra.Repository
{
    public class LogRepositoryTest
    {
        private readonly IAutoFaker _autoFaker;
        private readonly Mock<DbDapperContext> _dapperMock;
        private readonly LogRepository _logRepository;

        public LogRepositoryTest()
        {
            _autoFaker = AutoFaker.Create();
            _dapperMock = new Mock<DbDapperContext>(new Mock<IConfiguration>().Object);
            _logRepository = new LogRepository(_dapperMock.Object);
        }

        [Fact]
        public async Task InsertLog_Test()
        {
            var dbConnection = new Mock<DbConnection>();
            dbConnection.SetupDapperAsync(x => x.QueryFirstOrDefaultAsync<Guid>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(_autoFaker.Generate<Guid>());

            _dapperMock.Setup(x => x.DapperConnection).Returns(dbConnection.Object);
            Assert.IsType<Guid>(await _logRepository.InsertLog(_autoFaker.Generate<LogModel>()));
        }

    }
}
