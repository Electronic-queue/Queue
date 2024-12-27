using AutoMapper;
using Queue.Application.ReasonsForCancellations.Commands.CreateReasonsForCancellation;
using Queue.Application.ReasonsForCancellations.Commands.UpdateReasonsForCancellation;
using Queue.WebApi.Contracts.ReasonsForCancellationContracts;

namespace Queue.WebApi.Common.ReasonsForCancellationProfile;

public class ReasonsForCancellationProfile:Profile
{
    public ReasonsForCancellationProfile()
    {
        CreateMap<CreateReasonsForCancellationDto, CreateReasonsForCancellationCommand>()
             .ForMember(x => x.RecordId,
           opt => opt.MapFrom(y => y.RecordId))
             .ForMember(x => x.Explantation,
           opt => opt.MapFrom(y => y.Explantation));
        CreateMap<UpdateReasonsForCancellationDto, UpdateReasonsForCancellationCommand>()
             .ForMember(x => x.ReasonId,
           opt => opt.MapFrom(y => y.ReasonId))
             .ForMember(x => x.RecordId,
           opt => opt.MapFrom(y => y.RecordId))
              .ForMember(x => x.Explantation,
           opt => opt.MapFrom(y => y.Explantation));
    }
}
