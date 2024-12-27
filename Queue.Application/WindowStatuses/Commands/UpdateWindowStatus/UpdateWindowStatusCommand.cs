using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.WindowStatuses.Commands.UpdateWIndowStatus;

public record UpdateWindowStatusCommand(
    int WindowStatusId,
    string NameRu,
    string NameKk,
    string NameEn,
    string? DescriptionRu,
    string? DescriptionKk,
    string? DescriptionEn
   ):IRequest<Result>;

