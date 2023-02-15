using HyperAtivaTeste.Domains.Models;

namespace HyperAtivaTeste.Domains.Interfaces.Repository
{
    public interface ICreditCardRepository
    {
        Task<ResultModel> InsertCreditCardByUser(CreditCardInsertByUserModel insert);
        Task<Guid> InsertFile(CreditCardInsertByFileModel insert);
        Task<List<ResultModel>> InsertCreditCardByFile(CreditCardInsertByFileModel insert, Guid createdFileId);
        Task<string> VerifyCreditCard(string creditCard);
    }
}
