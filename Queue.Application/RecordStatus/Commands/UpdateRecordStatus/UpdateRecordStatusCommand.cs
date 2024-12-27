using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.RecordStatus.Commands.UpdateRecordStatus;

public record UpdateRecordStatusCommand(
     int RecordStatusId,
     string NameRu,
    string NameKk,
    string NameEn,
    string? DescriptionRu,
    string? DescriptionKk,
    string? DescriptionEn,
    int? CreatedBy
     ) :IRequest<Result>;

