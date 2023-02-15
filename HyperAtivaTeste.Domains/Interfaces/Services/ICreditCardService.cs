using Microsoft.AspNetCore.Http;
using HyperAtivaTeste.Domains.Models;

namespace HyperAtivaTeste.Domains.Interfaces.Services
{
    public interface ICreditCardService
    {
        Task<ResultModel> InsertCreditCardByUser(CreditCardInsertByUserModel insert);
        CreditCardInsertByFileModel ReadCreditCardFile(IFormFile file, Guid createdUserId);
        Task<List<ResultModel>> InsertCreditCardByFile(CreditCardInsertByFileModel insert);
        Task<string> VerifyCreditCard(string creditCard);
    }
}
