using AutoMapper;
using Queue.Application.Common.Mappings;
using Queue.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.Windows.Queries.GetWindowList;

public  class WindowLookupDto:IMapWith<Window>
{
    public int WindowId { get; set; }

    public int WindowNumber { get; set; }

    public DateTime CreatedOn { get; set; }


    public int? CreatedBy { get; set; }

    public void Mappinf(Profile profile)
    {
        profile.CreateMap<Window, WindowLookupDto>()
            .ForMember(windowVm => windowVm.WindowId,
                opt => opt.MapFrom(window => window.WindowId))
            .ForMember(windowVm => windowVm.WindowNumber,
                opt => opt.MapFrom(window => window.WindowNumber))
            .ForMember(windowVm => windowVm.CreatedOn,
                opt => opt.MapFrom(window => window.CreatedOn))
            .ForMember(windowVm => windowVm.CreatedBy,
                opt => opt.MapFrom(window => window.CreatedBy));
    }
}
