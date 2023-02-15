using AutoBogus;
using AutoMapper;
using Moq;
using HyperAtivaTeste.API.AutoMapper;
using HyperAtivaTeste.API.Services;
using HyperAtivaTeste.API.ViewModels;
using HyperAtivaTeste.Domains.Interfaces.Services;
using HyperAtivaTeste.Domains.Models;
using Xunit;

namespace HyperAtivaTeste.Tests.UnitTests.HyperAtivaTeste.API.Services
{
    public class LogApiServiceTest
    {
        private readonly Mapper _mapper;
        private readonly IAutoFaker _autoFaker;
        private readonly Mock<ILogService> _logService;

        public LogApiServiceTest()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfiles())));
            _autoFaker = AutoFaker.Create();
            _logService = new Mock<ILogService>();
        }

        [Fact]
        public async Task InsertLog_Test()
        {
            _logService.Setup(x => x.InsertLog(It.IsAny<LogModel>())).ReturnsAsync(_autoFaker.Generate<Guid>());
            Assert.IsType<Guid>(await new LogApiService(_mapper, _logService.Object).InsertLog(_autoFaker.Generate<LogViewModel>()));
        }
    }
}
