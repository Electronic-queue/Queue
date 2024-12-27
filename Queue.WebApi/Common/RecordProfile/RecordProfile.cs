using AutoMapper;
using Queue.Application.Record.Commands.CreateRecord;
using Queue.Application.Record.Commands.UpdateRecord;
using Queue.WebApi.Contracts.RecordContracts;

namespace Queue.WebApi.Common.RecordProfile
{
    public class RecordProfile:Profile
    {
        public RecordProfile()
        {
            CreateMap<CreateRecordDto, CreateRecordCommand>()
                 .ForMember(x => x.FirstName,
           opt => opt.MapFrom(y => y.FirstName))
                 .ForMember(x => x.LastName,
           opt => opt.MapFrom(y => y.LastName))
                 .ForMember(x => x.Surname,
           opt => opt.MapFrom(y => y.Surname))
                 .ForMember(x => x.Iin,
           opt => opt.MapFrom(y => y.Iin))
                  .ForMember(x => x.RecordStatusId,
           opt => opt.MapFrom(y => y.RecordStatusId))
                  .ForMember(x => x.ServiceId,
           opt => opt.MapFrom(y => y.ServiceId))
                  .ForMember(x => x.IsCreatedByEmployee,
           opt => opt.MapFrom(y => y.IsCreatedByEmployee))
                  .ForMember(x => x.CreatedBy,
           opt => opt.MapFrom(y => y.CreatedBy))
                  .ForMember(x => x.TicketNumber,
           opt => opt.MapFrom(y => y.TicketNumber));

            CreateMap<UpdateRecordDto,UpdateRecordCommand>()
                .ForMember(x => x.RecordId,
           opt => opt.MapFrom(y => y.RecordId))
                .ForMember(x => x.FirstName,
           opt => opt.MapFrom(y => y.FirstName))
                 .ForMember(x => x.LastName,
           opt => opt.MapFrom(y => y.LastName))
                 .ForMember(x => x.Surname,
           opt => opt.MapFrom(y => y.Surname))
                 .ForMember(x => x.Iin,
           opt => opt.MapFrom(y => y.Iin))
                  .ForMember(x => x.RecordStatusId,
           opt => opt.MapFrom(y => y.RecordStatusId))
                  .ForMember(x => x.ServiceId,
           opt => opt.MapFrom(y => y.ServiceId))
                  .ForMember(x => x.IsCreatedByEmployee,
           opt => opt.MapFrom(y => y.IsCreatedByEmployee))
                  .ForMember(x => x.CreatedBy,
           opt => opt.MapFrom(y => y.CreatedBy))
                  .ForMember(x => x.TicketNumber,
           opt => opt.MapFrom(y => y.TicketNumber));

        }
    }
}
