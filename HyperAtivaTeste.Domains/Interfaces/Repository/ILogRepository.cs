using HyperAtivaTeste.Domains.Models;

namespace HyperAtivaTeste.Domains.Interfaces.Repository
{
    public interface ILogRepository
    {
        Task<Guid> InsertLog(LogModel log);
    }
}
