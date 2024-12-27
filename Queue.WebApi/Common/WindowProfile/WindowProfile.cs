using AutoMapper;
using Queue.Application.Windows.Commands.CreateWindow;
using Queue.Application.Windows.Commands.UpdateWindow;
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
        CreateMap<UpdateWindowDto,UpdateWindowCommand>()
            .ForMember(windowVm => windowVm.WindowId,
           opt => opt.MapFrom(window => window.WindowId))
            .ForMember(windowVm => windowVm.WindowNumber,
           opt => opt.MapFrom(window => window.WindowNumber))
       .ForMember(windowVm => windowVm.WindowStatusId,
           opt => opt.MapFrom(window => window.WindowStatusId))
       .ForMember(windowVm => windowVm.CreatedBy,
           opt => opt.MapFrom(window => window.CreatedBy));
    }
}
