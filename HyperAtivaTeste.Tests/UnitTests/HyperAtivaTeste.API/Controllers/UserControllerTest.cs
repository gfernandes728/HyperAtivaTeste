using AutoBogus;
using Microsoft.AspNetCore.Mvc;
using Moq;
using HyperAtivaTeste.API.Controllers;
using HyperAtivaTeste.API.Services.Interfaces;
using Xunit;

namespace HyperAtivaTeste.Tests.UnitTests.HyperAtivaTeste.API.Controllers
{
    public class UserControllerTest
    {
        private readonly IAutoFaker _autoFaker;
        private readonly Mock<IUserApiService> _userApiService;

        public UserControllerTest()
        {
            _autoFaker = AutoFaker.Create();
            _userApiService = new Mock<IUserApiService>();
        }

        [Fact]
        public async Task GetTokenByEmail_NotInsertedEmail_Test()
        {
            var actionResult = Assert.IsType<BadRequestObjectResult>((await new UserController(null).GetTokenByEmail("")).Result);
            Assert.Equal(400, actionResult.StatusCode);
        }

        [Fact]
        public async Task GetTokenByEmail_TokenNotGenerated_Test()
        {
            _userApiService.Setup(x => x.GetTokenByEmail(It.IsAny<string>())).ReturnsAsync("");
            var actionResult = Assert.IsType<NotFoundObjectResult>((await new UserController(_userApiService.Object).GetTokenByEmail(_autoFaker.Generate<string>())).Result);
            Assert.Equal(404, actionResult.StatusCode);
        }

        [Fact]
        public async Task GetTokenByEmail_TokenGenerated_Test()
        {
            _userApiService.Setup(x => x.GetTokenByEmail(It.IsAny<string>())).ReturnsAsync(_autoFaker.Generate<string>());
            var actionResult = Assert.IsType<OkObjectResult>((await new UserController(_userApiService.Object).GetTokenByEmail(_autoFaker.Generate<string>())).Result);
            Assert.Equal(200, actionResult.StatusCode);
        }
    }
}
