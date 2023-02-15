using HyperAtivaTeste.Domains.Interfaces.Repository;
using HyperAtivaTeste.Domains.Models;
using Dapper;

namespace HyperAtivaTeste.Infra.Repository
{
    public class LogRepository : ILogRepository
    {
        private readonly DbDapperContext _db;

        public LogRepository(DbDapperContext db)
            => _db = db;

        public async Task<Guid> InsertLog(LogModel log)
            => await _db.DapperConnection.QueryFirstOrDefaultAsync<Guid>($"exec dbo.spInsertLog '{log.UserId}', '{log.Action.Trim()}', '{log.Result.Trim()}';");
    }
}
