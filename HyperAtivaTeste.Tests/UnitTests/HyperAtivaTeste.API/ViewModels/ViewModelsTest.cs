using AutoBogus;
using Moq;
using HyperAtivaTeste.API.ViewModels;
using Xunit;

namespace HyperAtivaTeste.Tests.UnitTests.HyperAtivaTeste.API.ViewModels
{
    public class ViewModelsTest
    {
        private readonly IAutoFaker _autoFaker;

        public ViewModelsTest()
            => _autoFaker = AutoFaker.Create();

        [Fact]
        public void CreditCardFileViewModel_Test()
        {
            var model = _autoFaker.Generate<CreditCardFileViewModel>();
            Assert.IsType<CreditCardFileViewModel>(model);
            Assert.IsType<string>(model.Identify);
            Assert.IsType<string>(model.Number);
            Assert.IsType<string>(model.CreditCard);
        }

        [Fact]
        public void CreditCardInsertByFileViewModel_Test()
        {
            var model = _autoFaker.Generate<CreditCardInsertByFileViewModel>();
            model.CreditCards = _autoFaker.Generate<CreditCardFileViewModel>(1);

            Assert.IsType<CreditCardInsertByFileViewModel>(model);
            Assert.IsType<Guid>(model.CreatedUserId);
            Assert.IsType<string>(model.Name);
            Assert.IsType<DateTime>(model.DateFile);
            Assert.IsType<string>(model.Lote);
            Assert.IsType<int>(model.TotalRegisters);
            Assert.IsType<List<CreditCardFileViewModel>>(model.CreditCards);
        }

        [Fact]
        public void CreditCardInsertByUserViewModel_Test()
        {
            var model = _autoFaker.Generate<CreditCardInsertByUserViewModel>();
            Assert.IsType<CreditCardInsertByUserViewModel>(model);
            Assert.IsType<Guid>(model.CreatedUserId);
            Assert.IsType<string>(model.CreditCard);
        }

        [Fact]
        public void DefaultViewModel_Test()
        {
            var model = _autoFaker.Generate<DefaultViewModel>();
            Assert.IsType<DefaultViewModel>(model);
            Assert.IsType<Guid>(model.Id);
        }

        [Fact]
        public void FileViewModel_Test()
        {
            var model = _autoFaker.Generate<FileViewModel>();
            model.File = FormFileMock();

            Assert.IsType<FileViewModel>(model);
            Assert.NotNull(model.File);
        }

        [Fact]
        public void LogViewModel_Test()
        {
            var model = _autoFaker.Generate<LogViewModel>();
            Assert.IsType<LogViewModel>(model);
            Assert.IsType<Guid>(model.UserId);
            Assert.IsType<string>(model.Action);
            Assert.IsType<string>(model.Result);
        }

        [Fact]
        public void ResultViewModel_Test()
        {
            var model = _autoFaker.Generate<ResultViewModel>();
            Assert.IsType<ResultViewModel>(model);
            Assert.IsType<Guid>(model.Id);
            Assert.IsType<string>(model.Result);
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
