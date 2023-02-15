using AutoBogus;
using Microsoft.AspNetCore.Mvc;
using Moq;
using HyperAtivaTeste.API.Controllers;
using HyperAtivaTeste.API.Services.Interfaces;
using HyperAtivaTeste.API.ViewModels;
using Xunit;

namespace HyperAtivaTeste.Tests.UnitTests.HyperAtivaTeste.API.Controllers
{
    public class CreditCardControllerTest
    {
        private readonly IAutoFaker _autoFaker;
        private readonly Mock<ICreditCardApiService> _creditCardApiService;
        private readonly Mock<IAuthorizationApiService> _authorizationApiService;
        private readonly Mock<ILogApiService> _logApiService;

        public CreditCardControllerTest()
        {
            _autoFaker = AutoFaker.Create();
            _creditCardApiService = new Mock<ICreditCardApiService>();
            _authorizationApiService = new Mock<IAuthorizationApiService>();
            _logApiService = new Mock<ILogApiService>();
        }

        [Fact]
        public async Task InsertCreditCardByUser_UserLoggedNotFound_Test()
        {
            Guid? guid = null;
            _authorizationApiService.Setup(x => x.GetUserIdLogged(It.IsAny<string>())).Returns(guid);

            var actionResult = Assert.IsType<BadRequestObjectResult>((await new CreditCardController(null, _authorizationApiService.Object, null).InsertCreditCardByUser("")).Result);
            Assert.Equal(400, actionResult.StatusCode);
        }

        [Fact]
        public async Task InsertCreditCardByUser_CreditCardNotExists_Test()
        {
            _authorizationApiService.Setup(x => x.GetUserIdLogged(It.IsAny<string>())).Returns(_autoFaker.Generate<Guid>());
            _logApiService.Setup(x => x.InsertLog(It.IsAny<LogViewModel>())).ReturnsAsync(_autoFaker.Generate<Guid>());

            var actionResult = Assert.IsType<BadRequestObjectResult>((await new CreditCardController(null, _authorizationApiService.Object, _logApiService.Object).InsertCreditCardByUser("")).Result);
            Assert.Equal(400, actionResult.StatusCode);
        }

        [Fact]
        public async Task InsertCreditCardByUser_CreditCardNotInserted_Test()
        {
            _authorizationApiService.Setup(x => x.GetUserIdLogged(It.IsAny<string>())).Returns(_autoFaker.Generate<Guid>());
            _logApiService.Setup(x => x.InsertLog(It.IsAny<LogViewModel>())).ReturnsAsync(_autoFaker.Generate<Guid>());

            ResultViewModel result = null;
            _creditCardApiService.Setup(x => x.InsertCreditCardByUser(It.IsAny<CreditCardInsertByUserViewModel>())).ReturnsAsync(result);

            var actionResult = Assert.IsType<BadRequestObjectResult>((await new CreditCardController(_creditCardApiService.Object, _authorizationApiService.Object, _logApiService.Object).InsertCreditCardByUser(_autoFaker.Generate<string>())).Result);
            Assert.Equal(400, actionResult.StatusCode);
        }

        [Fact]
        public async Task InsertCreditCardByUser_Success_Test()
        {
            _authorizationApiService.Setup(x => x.GetUserIdLogged(It.IsAny<string>())).Returns(_autoFaker.Generate<Guid>());
            _logApiService.Setup(x => x.InsertLog(It.IsAny<LogViewModel>())).ReturnsAsync(_autoFaker.Generate<Guid>());
            _creditCardApiService.Setup(x => x.InsertCreditCardByUser(It.IsAny<CreditCardInsertByUserViewModel>())).ReturnsAsync(_autoFaker.Generate<ResultViewModel>());

            var actionResult = Assert.IsType<OkObjectResult>((await new CreditCardController(_creditCardApiService.Object, _authorizationApiService.Object, _logApiService.Object).InsertCreditCardByUser(_autoFaker.Generate<string>())).Result);
            Assert.Equal(200, actionResult.StatusCode);
        }

        [Fact]
        public async Task InsertCreditCardByFile_UserLoggedNotFound_Test()
        {
            Guid? guid = null;
            _authorizationApiService.Setup(x => x.GetUserIdLogged(It.IsAny<string>())).Returns(guid);

            var actionResult = Assert.IsType<BadRequestObjectResult>((await new CreditCardController(null, _authorizationApiService.Object, null).InsertCreditCardByFile(null)).Result);
            Assert.Equal(400, actionResult.StatusCode);
        }

        [Fact]
        public async Task InsertCreditCardByFile_DataFileNotExists_Test()
        {
            _authorizationApiService.Setup(x => x.GetUserIdLogged(It.IsAny<string>())).Returns(_autoFaker.Generate<Guid>());
            _logApiService.Setup(x => x.InsertLog(It.IsAny<LogViewModel>())).ReturnsAsync(_autoFaker.Generate<Guid>());

            var actionResult = Assert.IsType<BadRequestObjectResult>((await new CreditCardController(null, _authorizationApiService.Object, _logApiService.Object).InsertCreditCardByFile(null)).Result);
            Assert.Equal(400, actionResult.StatusCode);
        }

        [Fact]
        public async Task InsertCreditCardByFile_FileNotExists_Test()
        {
            _authorizationApiService.Setup(x => x.GetUserIdLogged(It.IsAny<string>())).Returns(_autoFaker.Generate<Guid>());
            _logApiService.Setup(x => x.InsertLog(It.IsAny<LogViewModel>())).ReturnsAsync(_autoFaker.Generate<Guid>());

            var actionResult = Assert.IsType<BadRequestObjectResult>((await new CreditCardController(null, _authorizationApiService.Object, _logApiService.Object).InsertCreditCardByFile(new FileViewModel() { File = null })).Result);
            Assert.Equal(400, actionResult.StatusCode);
        }

        [Fact]
        public async Task InsertCreditCardByFile_FileNotInserted_Test()
        {
            _authorizationApiService.Setup(x => x.GetUserIdLogged(It.IsAny<string>())).Returns(_autoFaker.Generate<Guid>());
            _logApiService.Setup(x => x.InsertLog(It.IsAny<LogViewModel>())).ReturnsAsync(_autoFaker.Generate<Guid>());
            _creditCardApiService.Setup(x => x.ReadCreditCardFile(It.IsAny<IFormFile>(), It.IsAny<Guid>())).Returns(_autoFaker.Generate<CreditCardInsertByFileViewModel>());

            List<ResultViewModel> result = null;
            _creditCardApiService.Setup(x => x.InsertCreditCardByFile(It.IsAny<CreditCardInsertByFileViewModel>())).ReturnsAsync(result);

            var actionResult = Assert.IsType<BadRequestObjectResult>((await new CreditCardController(_creditCardApiService.Object, _authorizationApiService.Object, _logApiService.Object).InsertCreditCardByFile(new FileViewModel() { File = FormFileMock() })).Result);
            Assert.Equal(400, actionResult.StatusCode);
        }

        [Fact]
        public async Task InsertCreditCardByFile_Success_Test()
        {
            _authorizationApiService.Setup(x => x.GetUserIdLogged(It.IsAny<string>())).Returns(_autoFaker.Generate<Guid>());
            _logApiService.Setup(x => x.InsertLog(It.IsAny<LogViewModel>())).ReturnsAsync(_autoFaker.Generate<Guid>());
            _creditCardApiService.Setup(x => x.ReadCreditCardFile(It.IsAny<IFormFile>(), It.IsAny<Guid>())).Returns(_autoFaker.Generate<CreditCardInsertByFileViewModel>());
            _creditCardApiService.Setup(x => x.InsertCreditCardByFile(It.IsAny<CreditCardInsertByFileViewModel>())).ReturnsAsync(_autoFaker.Generate<ResultViewModel>(1));

            var actionResult = Assert.IsType<OkObjectResult>((await new CreditCardController(_creditCardApiService.Object, _authorizationApiService.Object, _logApiService.Object).InsertCreditCardByFile(new FileViewModel() { File = FormFileMock() })).Result);
            Assert.Equal(200, actionResult.StatusCode);
        }

        [Fact]
        public async Task VerifyCreditCard_UserLoggedNotFound_Test()
        {
            Guid? guid = null;
            _authorizationApiService.Setup(x => x.GetUserIdLogged(It.IsAny<string>())).Returns(guid);

            var actionResult = Assert.IsType<BadRequestObjectResult>((await new CreditCardController(null, _authorizationApiService.Object, null).VerifyCreditCard("")).Result);
            Assert.Equal(400, actionResult.StatusCode);
        }

        [Fact]
        public async Task VerifyCreditCard_CreditCardNotExists_Test()
        {
            _authorizationApiService.Setup(x => x.GetUserIdLogged(It.IsAny<string>())).Returns(_autoFaker.Generate<Guid>());
            _logApiService.Setup(x => x.InsertLog(It.IsAny<LogViewModel>())).ReturnsAsync(_autoFaker.Generate<Guid>());

            var actionResult = Assert.IsType<BadRequestObjectResult>((await new CreditCardController(null, _authorizationApiService.Object, _logApiService.Object).VerifyCreditCard("")).Result);
            Assert.Equal(400, actionResult.StatusCode);
        }

        [Fact]
        public async Task VerifyCreditCard_CreditCardNotFound_Test()
        {
            _authorizationApiService.Setup(x => x.GetUserIdLogged(It.IsAny<string>())).Returns(_autoFaker.Generate<Guid>());
            _logApiService.Setup(x => x.InsertLog(It.IsAny<LogViewModel>())).ReturnsAsync(_autoFaker.Generate<Guid>());
            _creditCardApiService.Setup(x => x.VerifyCreditCard(It.IsAny<string>())).ReturnsAsync("");

            var actionResult = Assert.IsType<NotFoundObjectResult>((await new CreditCardController(_creditCardApiService.Object, _authorizationApiService.Object, _logApiService.Object).VerifyCreditCard(_autoFaker.Generate<string>())).Result);
            Assert.Equal(404, actionResult.StatusCode);
        }

        [Fact]
        public async Task VerifyCreditCard_CreditCardNotInserted_Test()
        {
            _authorizationApiService.Setup(x => x.GetUserIdLogged(It.IsAny<string>())).Returns(_autoFaker.Generate<Guid>());
            _logApiService.Setup(x => x.InsertLog(It.IsAny<LogViewModel>())).ReturnsAsync(_autoFaker.Generate<Guid>());
            _creditCardApiService.Setup(x => x.VerifyCreditCard(It.IsAny<string>())).ReturnsAsync("not exists");

            var actionResult = Assert.IsType<NotFoundObjectResult>((await new CreditCardController(_creditCardApiService.Object, _authorizationApiService.Object, _logApiService.Object).VerifyCreditCard(_autoFaker.Generate<string>())).Result);
            Assert.Equal(404, actionResult.StatusCode);
        }

        [Fact]
        public async Task VerifyCreditCard_Success_Test()
        {
            _authorizationApiService.Setup(x => x.GetUserIdLogged(It.IsAny<string>())).Returns(_autoFaker.Generate<Guid>());
            _logApiService.Setup(x => x.InsertLog(It.IsAny<LogViewModel>())).ReturnsAsync(_autoFaker.Generate<Guid>());
            _creditCardApiService.Setup(x => x.VerifyCreditCard(It.IsAny<string>())).ReturnsAsync("exists");

            var actionResult = Assert.IsType<OkObjectResult>((await new CreditCardController(_creditCardApiService.Object, _authorizationApiService.Object, _logApiService.Object).VerifyCreditCard(_autoFaker.Generate<string>())).Result);
            Assert.Equal(200, actionResult.StatusCode);
        }

        private IFormFile FormFileMock()
        {
            var fileMock = new Mock<IFormFile>();
            var content = "Hello World from a Fake File";
            var fileName = "teste.txt";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            return fileMock.Object;
        }
    }
}
