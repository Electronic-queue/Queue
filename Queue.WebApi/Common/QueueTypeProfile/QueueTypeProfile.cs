using AutoMapper;
using Queue.Application.QueueTypes.Commands.CreateQueueType;
using Queue.Application.QueueTypes.Commands.UpdateQueueType;
using Queue.WebApi.Contracts.QueueTypeContracts;

namespace Queue.WebApi.Common.QueueTypeProfile;

public class QueueTypeProfile:Profile
{
    public QueueTypeProfile()
    {
     CreateMap<CreateQueueTypeDto,CreateQueueTypeCommand>()
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
        CreateMap<UpdateQueueTypeDto, UpdateQueueTypeCommand>()
            .ForMember(x => x.QueueTypeId,
           opt => opt.MapFrom(y => y.QueueTypeId))
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
