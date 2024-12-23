using AutoMapper;
using Queue.Application.Services.Commands.CreateService;
using Queue.Application.Services.Commands.UpdateService;
using Queue.WebApi.Contracts.ServiceContracts;

namespace Queue.WebApi.Common.ServiceProfile;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<CreateServiceDto, CreateServiceCommand>()
                .ForMember(ServiceVm => ServiceVm.NameRu,
                opt => opt.MapFrom(Service => Service.NameRu))
                .ForMember(ServiceVm => ServiceVm.NameKk,
                opt => opt.MapFrom(Service => Service.NameKk))
                .ForMember(ServiceVm => ServiceVm.NameEn,
                opt => opt.MapFrom(Service => Service.NameEn))
                .ForMember(ServiceVm => ServiceVm.DescriptionRu,
                opt => opt.MapFrom(Service => Service.DescriptionRu))
                .ForMember(ServiceVm => ServiceVm.DescriptionKk,
                opt => opt.MapFrom(Service => Service.DescriptionKk))
                .ForMember(ServiceVm => ServiceVm.DescriptionEn,
                opt => opt.MapFrom(Service => Service.DescriptionEn))
            .ForMember(ServiceVm => ServiceVm.AverageExecutionTime,
            opt => opt.MapFrom(Service => Service.AverageExecutionTime))
            .ForMember(ServiceVm => ServiceVm.ParentserviceId,
            opt => opt.MapFrom(Service => Service.ParentserviceId))
            .ForMember(ServiceVm => ServiceVm.QueueTypeId,
            opt => opt.MapFrom(Service => Service.QueueTypeId))
            .ForMember(ServiceVm => ServiceVm.CreatedBy,
            opt => opt.MapFrom(Service => Service.CreatedBy));

        CreateMap<UpdateServiceDto, UpdateServiceCommand>()
                .ForMember(ServiceVm => ServiceVm.ServiceId,
                opt => opt.MapFrom(Service => Service.ServiceId))
                .ForMember(ServiceVm => ServiceVm.NameRu,
                opt => opt.MapFrom(Service => Service.NameRu))
                .ForMember(ServiceVm => ServiceVm.NameKk,
                opt => opt.MapFrom(Service => Service.NameKk))
                .ForMember(ServiceVm => ServiceVm.NameEn,
                opt => opt.MapFrom(Service => Service.NameEn))
                .ForMember(ServiceVm => ServiceVm.DescriptionRu,
                opt => opt.MapFrom(Service => Service.DescriptionRu))
                .ForMember(ServiceVm => ServiceVm.DescriptionKk,
                opt => opt.MapFrom(Service => Service.DescriptionKk))
                .ForMember(ServiceVm => ServiceVm.DescriptionEn,
                opt => opt.MapFrom(Service => Service.DescriptionEn))
                .ForMember(ServiceVm => ServiceVm.AverageExecutionTime,
            opt => opt.MapFrom(Service => Service.AverageExecutionTime))
            .ForMember(ServiceVm => ServiceVm.ParentserviceId,
            opt => opt.MapFrom(Service => Service.ParentserviceId))
            .ForMember(ServiceVm => ServiceVm.QueueTypeId,
            opt => opt.MapFrom(Service => Service.QueueTypeId));
    }
}
