using HyperAtivaTeste.Domains.Models;

namespace HyperAtivaTeste.Domains.Interfaces.Services
{
    public interface ILogService
    {
        Task<Guid> InsertLog(LogModel log);
    }
}
