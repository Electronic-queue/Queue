using AutoMapper;
using Queue.Application.Windows.Commands.CreateWindow;
using Queue.WebApi.Contracts.WIndowContracts;

namespace Queue.WebApi.Common.WindowProfile;

public class WindowProfile:Profile
{
    public WindowProfile()
    {
        CreateMap<CreateWindowDto, CreateWindowCommand>()
       .ForMember(windowVm => windowVm.WindowNumber,
           opt => opt.MapFrom(window => window.WindowNumber))
       .ForMember(windowVm => windowVm.WindowStatusId,
           opt => opt.MapFrom(window => window.WindowStatusId))
       .ForMember(windowVm => windowVm.CreatedBy,
           opt => opt.MapFrom(window => window.CreatedBy));
    }
}
