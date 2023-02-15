using HyperAtivaTeste.Domains.Interfaces.Repository;
using HyperAtivaTeste.Domains.Interfaces.Services;
using HyperAtivaTeste.Domains.Models;

namespace HyperAtivaTeste.Infra.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository)
            => _logRepository = logRepository;

        public async Task<Guid> InsertLog(LogModel log)
            => await _logRepository.InsertLog(log);
    }
}
