using AutoMapper;
using Queue.Application.ExceedingsTimes.Commands.CreateExceedingsTime;
using Queue.Application.ExceedingsTimes.Commands.UpdateExceedingsTime;
using Queue.WebApi.Contracts.ExceedingsTimeContracts;

namespace Queue.WebApi.Common.ExceedingsTimeProfile;

public class ExceedingsTimeProfile:Profile
{
    public ExceedingsTimeProfile()
    {
        CreateMap<CreateExceedingsTimeDto, CreateExceedingsTimeCommand>()
            .ForMember(x => x.WindowId,
           opt => opt.MapFrom(y => y.WindowId))
            .ForMember(x => x.TimeForExcommunication,
           opt => opt.MapFrom(y => y.TimeForExcommunication));
        CreateMap<UpdateExceedingsTimeDto, UpdateExceedingsTimeCommand>()
            .ForMember(x => x.ExceedingsTimeId,
           opt => opt.MapFrom(y => y.ExceedingsTimeId))
            .ForMember(x => x.WindowId,
           opt => opt.MapFrom(y => y.WindowId))
            .ForMember(x => x.TimeForExcommunication,
           opt => opt.MapFrom(y => y.TimeForExcommunication))
            .ForMember(x => x.CanceledOn,
           opt => opt.MapFrom(y => y.CanceledOn));
    }
}
