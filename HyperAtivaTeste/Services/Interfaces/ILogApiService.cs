using HyperAtivaTeste.API.ViewModels;

namespace HyperAtivaTeste.API.Services.Interfaces
{
    public interface ILogApiService
    {
        Task<Guid> InsertLog(LogViewModel log);
    }
}
