using Microsoft.AspNetCore.Http;
using HyperAtivaTeste.Domains.Interfaces.Repository;
using HyperAtivaTeste.Domains.Interfaces.Services;
using HyperAtivaTeste.Domains.Models;

namespace HyperAtivaTeste.Infra.Services
{
    public class CreditCardService : ICreditCardService
    {
        private readonly ICreditCardRepository _creditCardRepository;

        public CreditCardService(ICreditCardRepository creditCardRepository)
            => _creditCardRepository = creditCardRepository;

        public async Task<ResultModel> InsertCreditCardByUser(CreditCardInsertByUserModel insert)
            => await _creditCardRepository.InsertCreditCardByUser(insert);

        public CreditCardInsertByFileModel ReadCreditCardFile(IFormFile file, Guid createdUserId)
        {
            var isFirstLine = true;

            var model = new CreditCardInsertByFileModel();
            model.CreatedUserId = createdUserId;
            model.CreditCards = new List<CreditCardFileModel>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                {
                    var line = reader.ReadLine();

                    if (isFirstLine)
                    {
                        model.Name = line.Substring(0, 29);
                        model.DateFile = Convert.ToDateTime($"{line.Substring(29, 4)}-{line.Substring(33, 2)}-{line.Substring(35, 2)}");
                        model.Lote = line.Substring(37, 8);
                        model.TotalRegisters = int.Parse(line.Substring(45, 6));
                    }

                    if (!isFirstLine && !line.Contains("LOTE"))
                    {
                        model.CreditCards.Add(new CreditCardFileModel()
                        {
                            Identify = line.Substring(0, 1),
                            Number = line.Substring(1, 6),
                            CreditCard = line.Substring(7)
                        });
                    }

                    isFirstLine = false;
                }
            }

            return model;
        }

        public async Task<List<ResultModel>> InsertCreditCardByFile(CreditCardInsertByFileModel insert)
            => await _creditCardRepository.InsertCreditCardByFile(insert, await _creditCardRepository.InsertFile(insert));

        public async Task<string> VerifyCreditCard(string creditCard)
            => await _creditCardRepository.VerifyCreditCard(creditCard);
    }
}
