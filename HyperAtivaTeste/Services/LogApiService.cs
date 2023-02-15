using AutoMapper;
using HyperAtivaTeste.API.Services.Interfaces;
using HyperAtivaTeste.API.ViewModels;
using HyperAtivaTeste.Domains.Interfaces.Services;
using HyperAtivaTeste.Domains.Models;

namespace HyperAtivaTeste.API.Services
{
    public class LogApiService : ILogApiService
    {
        private readonly IMapper _mapper;
        private readonly ILogService _logService;

        public LogApiService(IMapper mapper, ILogService logService)
        {
            _mapper = mapper;
            _logService = logService;
        }

        public async Task<Guid> InsertLog(LogViewModel log)
            => await _logService.InsertLog(_mapper.Map<LogModel>(log));
    }
}
