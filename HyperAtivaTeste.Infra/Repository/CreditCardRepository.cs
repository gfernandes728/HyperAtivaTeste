using HyperAtivaTeste.Domains.Interfaces.Repository;
using HyperAtivaTeste.Domains.Models;
using Dapper;

namespace HyperAtivaTeste.Infra.Repository
{
    public class CreditCardRepository : ICreditCardRepository
    {
        private readonly DbDapperContext _db;

        public CreditCardRepository(DbDapperContext db)
            => _db = db;

        public async Task<ResultModel> InsertCreditCardByUser(CreditCardInsertByUserModel insert)
            => await _db.DapperConnection.QueryFirstOrDefaultAsync<ResultModel>($"exec dbo.spInsertCreditCardByUser '{insert.CreatedUserId}', '{insert.CreditCard.Trim()}';");

        public async Task<Guid> InsertFile(CreditCardInsertByFileModel insert)
        {
            return await _db.DapperConnection.QueryFirstOrDefaultAsync<Guid>($"exec dbo.spInsertFile @createdUserId, @name, @lote, @dateFile, @totalRegisters;",
                new { createdUserId = insert.CreatedUserId, name = insert.Name.Trim(), lote = insert.Lote.Trim(), dateFile = insert.DateFile, totalRegisters = insert.TotalRegisters });
        }

        public async Task<List<ResultModel>> InsertCreditCardByFile(CreditCardInsertByFileModel insert, Guid createdFileId)
        {
            var results = new List<ResultModel>();
            var creditCards = insert.CreditCards;
            foreach (var creditCard in creditCards)
                results.Add(await _db.DapperConnection.QueryFirstOrDefaultAsync<ResultModel>($"exec dbo.spInsertCreditCardByFile '{createdFileId}', '{creditCard.Identify.Trim()}', '{creditCard.Number.Trim()}', '{creditCard.CreditCard.Trim()}';"));

            return results;
        }

        public async Task<string> VerifyCreditCard(string creditCard)
            => (await _db.DapperConnection.QueryFirstOrDefaultAsync<ResultModel>($"exec dbo.spGetCreditCard '{creditCard}';"))?.Result;
    }
}
