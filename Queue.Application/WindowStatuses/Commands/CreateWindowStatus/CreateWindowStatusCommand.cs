using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.WindowStatuses.Commands.CreateWindowStatus;

public record CreateWindowStatusCommand(
    string NameRu="",
    string NameKk="",
    string NameEn="",
    string? DescriptionRu=null,
    string? DescriptionKk = null,
    string? DescriptionEn = null,
    DateTime? CreatedOn=null,
    int? CreatedBy=null

    ) :IRequest<Result>;
