using AutoMapper;
using Queue.Application.WindowStatuses.Commands.CreateWindowStatus;
using Queue.Application.WindowStatuses.Commands.UpdateWIndowStatus;
using Queue.WebApi.Contracts.WindowStatusContracts;

namespace Queue.WebApi.Common.WindowStatusProfile
{
    public class WindowStatusProfile:Profile
    {
        public WindowStatusProfile()
        {
            CreateMap<CreateWindowStatusDto, CreateWindowStatusCommand>()
                .ForMember(x => x.NameRu,
           opt => opt.MapFrom(y => y.NameRu))
                .ForMember(x => x.NameKk,
           opt => opt.MapFrom(y => y.NameKk))
                .ForMember(x => x.NameEn,
           opt => opt.MapFrom(y => y.NameEn))
                .ForMember(x => x.DescriptionRu,
           opt => opt.MapFrom(y => y.DescriptionRu))
                .ForMember(x => x.DescriptionKk,
           opt => opt.MapFrom(y => y.DescriptionKk))
                .ForMember(x => x.DescriptionEn,
           opt => opt.MapFrom(y => y.DescriptionEn))
                .ForMember(x => x.CreatedBy,
           opt => opt.MapFrom(y => y.CreatedBy));
            CreateMap<UpdateWindowStatusDto, UpdateWindowStatusCommand>()
                .ForMember(x => x.WindowStatusId,
           opt => opt.MapFrom(y => y.WindowStatusId))
                .ForMember(x => x.NameRu,
           opt => opt.MapFrom(y => y.NameRu))
                .ForMember(x => x.NameKk,
           opt => opt.MapFrom(y => y.NameKk))
                .ForMember(x => x.NameEn,
           opt => opt.MapFrom(y => y.NameEn))
                .ForMember(x => x.DescriptionRu,
           opt => opt.MapFrom(y => y.DescriptionRu))
                .ForMember(x => x.DescriptionKk,
           opt => opt.MapFrom(y => y.DescriptionKk))
                .ForMember(x => x.DescriptionEn,
           opt => opt.MapFrom(y => y.DescriptionEn));
        }
    }
}
