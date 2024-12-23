using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.RecordStatus.Commands.CreateRecordStatus;

public record CreateRecordStatusCommand(
     string NameRu="",
     string NameKk = "",
     string NameEn = "",
     string? DescriptionRu=null,
     string? DescriptionKk= null,
     string? DescriptionEn = null,
     int? CreatedBy=null
) :IRequest<Result>;

