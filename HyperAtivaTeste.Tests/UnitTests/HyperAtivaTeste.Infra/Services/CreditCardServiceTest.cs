using AutoBogus;
using Moq;
using HyperAtivaTeste.Domains.Interfaces.Repository;
using Xunit;
using HyperAtivaTeste.Domains.Models;
using HyperAtivaTeste.Infra.Services;
using System.Text;

namespace HyperAtivaTeste.Tests.UnitTests.HyperAtivaTeste.Infra.Services
{
    public class CreditCardServiceTest
    {
        private readonly IAutoFaker _autoFaker;
        private readonly Mock<ICreditCardRepository> _creditCardRepository;

        public CreditCardServiceTest()
        {
            _autoFaker = AutoFaker.Create();
            _creditCardRepository = new Mock<ICreditCardRepository>();
        }

        [Fact]
        public async Task InsertCreditCardByUser_Test()
        {
            _creditCardRepository.Setup(x => x.InsertCreditCardByUser(It.IsAny<CreditCardInsertByUserModel>())).ReturnsAsync(_autoFaker.Generate<ResultModel>());
            Assert.IsType<ResultModel>(await new CreditCardService(_creditCardRepository.Object).InsertCreditCardByUser(_autoFaker.Generate<CreditCardInsertByUserModel>()));
        }

        [Fact]
        public void ReadCreditCardFile_Test()
            => Assert.IsType<CreditCardInsertByFileModel>(new CreditCardService(null).ReadCreditCardFile(FormFileMock(), _autoFaker.Generate<Guid>()));

        [Fact]
        public async Task InsertCreditCardByFile_Test()
        {
            _creditCardRepository.Setup(x => x.InsertFile(It.IsAny<CreditCardInsertByFileModel>())).ReturnsAsync(_autoFaker.Generate<Guid>());
            _creditCardRepository.Setup(x => x.InsertCreditCardByFile(It.IsAny<CreditCardInsertByFileModel>(), It.IsAny<Guid>())).ReturnsAsync(_autoFaker.Generate<ResultModel>(1));
            Assert.IsType<List<ResultModel>>(await new CreditCardService(_creditCardRepository.Object).InsertCreditCardByFile(_autoFaker.Generate<CreditCardInsertByFileModel>()));
        }

        [Fact]
        public async Task VerifyCreditCard_Test()
        {
            _creditCardRepository.Setup(x => x.VerifyCreditCard(It.IsAny<string>())).ReturnsAsync(_autoFaker.Generate<string>());
            Assert.IsType<string>(await new CreditCardService(_creditCardRepository.Object).VerifyCreditCard(_autoFaker.Generate<string>()));
        }

        private IFormFile FormFileMock()
        {
            var fileMock = new Mock<IFormFile>();

            var content = new StringBuilder();
            content.AppendLine("DESAFIO-HYPERATIVA           20180524LOTE0001000010                                        ");
            content.AppendLine("C2     4456897999999999                                                                    ");
            content.AppendLine("C1     4456897922969999                                                                    ");
            content.AppendLine("C3     4456897999999999                                                                    ");
            content.AppendLine("C4     4456897998199999                                                                    ");
            content.AppendLine("C5     4456897999999999124                                                                 ");
            content.AppendLine("C6     4456897912999999                                                                    ");
            content.AppendLine("C7     445689799999998                                                                     ");
            content.AppendLine("C8     4456897919999999                                                                    ");
            content.AppendLine("C9     4456897999099999                                                                    ");
            content.AppendLine("C10    4456897919999999                                                                    ");
            content.AppendLine("LOTE0001000010                                                                             ");

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
