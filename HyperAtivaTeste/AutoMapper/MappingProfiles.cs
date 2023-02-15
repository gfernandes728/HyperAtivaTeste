using AutoMapper;
using HyperAtivaTeste.API.ViewModels;
using HyperAtivaTeste.Domains.Models;

namespace HyperAtivaTeste.API.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ResultModel, ResultViewModel>().ReverseMap();
            CreateMap<CreditCardInsertByUserModel, CreditCardInsertByUserViewModel>().ReverseMap();
            CreateMap<CreditCardInsertByFileModel, CreditCardInsertByFileViewModel>().ReverseMap();
            CreateMap<CreditCardFileModel, CreditCardFileViewModel>().ReverseMap();
            CreateMap<LogModel, LogViewModel>().ReverseMap();
        }
    }
}
