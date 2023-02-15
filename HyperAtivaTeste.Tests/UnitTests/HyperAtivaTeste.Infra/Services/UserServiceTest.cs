using AutoBogus;
using Moq;
using HyperAtivaTeste.Domains.Interfaces.Repository;
using Xunit;
using HyperAtivaTeste.Domains.Models;
using HyperAtivaTeste.Infra.Services;

namespace HyperAtivaTeste.Tests.UnitTests.HyperAtivaTeste.Infra.Services
{
    public class UserServiceTest
    {
        private readonly IAutoFaker _autoFaker;
        private readonly Mock<IUserRepository> _userRepository;

        public UserServiceTest()
        {
            _autoFaker = AutoFaker.Create();
            _userRepository = new Mock<IUserRepository>();
        }

        [Fact]
        public async Task GetTokenByEmail_TokenNull_Test()
        {
            UserModel user = null;
            _userRepository.Setup(x => x.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(user);
            Assert.IsType<string>(await new UserService(_userRepository.Object, null).GetTokenByEmail(_autoFaker.Generate<string>()));
        }

        [Fact]
        public async Task GetTokenByEmail_GeneratedToken_Test()
        {
            _userRepository.Setup(x => x.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(_autoFaker.Generate<UserModel>());
            Assert.IsType<string>(await new UserService(_userRepository.Object, MockConfiguration()).GetTokenByEmail(_autoFaker.Generate<string>()));
        }

        private IConfiguration MockConfiguration()
            => new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string> { { "AuthJwt:Key", "b5ae3319cb2566e7938c4a459d351b75" } }).Build();
    }
}
