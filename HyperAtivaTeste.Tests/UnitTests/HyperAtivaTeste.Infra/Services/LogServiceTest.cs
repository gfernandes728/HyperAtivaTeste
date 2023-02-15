using AutoBogus;
using Moq;
using HyperAtivaTeste.Domains.Interfaces.Repository;
using Xunit;
using HyperAtivaTeste.Domains.Models;
using HyperAtivaTeste.Infra.Services;

namespace HyperAtivaTeste.Tests.UnitTests.HyperAtivaTeste.Infra.Services
{
    public class LogServiceTest
    {
        private readonly IAutoFaker _autoFaker;
        private readonly Mock<ILogRepository> _logRepository;

        public LogServiceTest()
        {
            _autoFaker = AutoFaker.Create();
            _logRepository = new Mock<ILogRepository>();
        }

        [Fact]
        public async Task InsertLog_Test()
        {
            _logRepository.Setup(x => x.InsertLog(It.IsAny<LogModel>())).ReturnsAsync(_autoFaker.Generate<Guid>());
            Assert.IsType<Guid>(await new LogService(_logRepository.Object).InsertLog(_autoFaker.Generate<LogModel>()));
        }
    }
}
