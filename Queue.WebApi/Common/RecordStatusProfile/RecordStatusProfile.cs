using AutoMapper;
using Queue.Application.RecordStatus.Commands.CreateRecordStatus;
using Queue.Application.RecordStatus.Commands.UpdateRecordStatus;
using Queue.WebApi.Contracts.RecordStatusContracts;

namespace Queue.WebApi.Common.RecordStatusProfile;

public class RecordStatusProfile:Profile
{
    public RecordStatusProfile()
    {
        CreateMap<CreateRecordStatusDto, CreateRecordStatusCommand>()
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
        CreateMap<UpdateRecordStatusDto,UpdateRecordStatusCommand>()
            .ForMember(x => x.RecordStatusId,
           opt => opt.MapFrom(y => y.RecordStatusId))
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

    }

}
