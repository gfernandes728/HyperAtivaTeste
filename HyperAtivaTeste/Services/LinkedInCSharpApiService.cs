using HyperAtivaTeste.API.Services.Interfaces;
using HyperAtivaTeste.Domains.Interfaces.Services;

namespace HyperAtivaTeste.API.Services
{
    public class LinkedInCSharpApiService : ILinkedInCSharpApiService
    {
        private readonly ILinkedInCSharpService _linkedInCSharpService;

        public LinkedInCSharpApiService(ILinkedInCSharpService linkedInCSharpService)
            => _linkedInCSharpService = linkedInCSharpService;

        public List<string> Question16()
            => _linkedInCSharpService.Question16();
    }
}
