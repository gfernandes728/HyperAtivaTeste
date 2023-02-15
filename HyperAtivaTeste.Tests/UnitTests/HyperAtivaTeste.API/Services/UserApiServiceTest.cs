using AutoBogus;
using Moq;
using HyperAtivaTeste.API.Services;
using HyperAtivaTeste.Domains.Interfaces.Services;
using Xunit;

namespace HyperAtivaTeste.Tests.UnitTests.HyperAtivaTeste.API.Services
{
    public class UserApiServiceTest
    {
        private readonly IAutoFaker _autoFaker;
        private readonly Mock<IUserService> _userService;

        public UserApiServiceTest()
        {
            _autoFaker = AutoFaker.Create();
            _userService = new Mock<IUserService>();
        }

        [Fact]
        public async Task GetTokenByEmail_Test()
        {
            _userService.Setup(x => x.GetTokenByEmail(It.IsAny<string>())).ReturnsAsync(_autoFaker.Generate<string>());
            Assert.IsType<string>(await new UserApiService(_userService.Object).GetTokenByEmail(_autoFaker.Generate<string>()));
        }
    }
}
