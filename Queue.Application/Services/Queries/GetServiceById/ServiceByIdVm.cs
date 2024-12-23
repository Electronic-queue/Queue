using AutoMapper;
using Queue.Application.Common.Mappings;
using Queue.Domain.Entites;

namespace Queue.Application.Services.Queries.GetServiceById;

public class ServiceByIdVm : IMapWith<Service>
{
    public int ServiceId { get; set; }

    public string NameRu { get; set; } = null!;

    public string NameKk { get; set; } = null!;

    public string NameEn { get; set; } = null!;

    public string? DescriptionRu { get; set; }

    public string? DescriptionKk { get; set; }

    public string? DescriptionEn { get; set; }

    public int AverageExecutionTime { get; set; }

    public int? ParentserviceId { get; set; }

    public int QueueTypeId { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Service, ServiceByIdVm>()
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
            opt => opt.MapFrom(Service => Service.QueueTypeId))
            .ForMember(ServiceVm => ServiceVm.CreatedOn, 
            opt => opt.MapFrom(Service => Service.CreatedOn))
            .ForMember(ServiceVm => ServiceVm.CreatedBy,
            opt => opt.MapFrom(Service => Service.CreatedBy));

    }
}
