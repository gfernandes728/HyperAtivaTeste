using AutoBogus;
using Moq;
using HyperAtivaTeste.API.Services;
using HyperAtivaTeste.Domains.Interfaces.Services;
using Xunit;

namespace HyperAtivaTeste.Tests.UnitTests.HyperAtivaTeste.API.Services
{
    public class AuthorizationApiServiceTest
    {
        private readonly IAutoFaker _autoFaker;
        private readonly Mock<IAuthorizationService> _authorizationService;

        public AuthorizationApiServiceTest()
        {
            _autoFaker = AutoFaker.Create();
            _authorizationService = new Mock<IAuthorizationService>();
        }

        [Fact]
        public void GetUserIdLogged_Test()
        {
            _authorizationService.Setup(x => x.GetUserIdLogged(It.IsAny<string>())).Returns(_autoFaker.Generate<Guid>());
            Assert.IsType<Guid>(new AuthorizationApiService(_authorizationService.Object).GetUserIdLogged(_autoFaker.Generate<string>()));
        }
    }
}
