using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Interfaces;

namespace Queue.Application.RecordStatus.Commands.UpdateRecordStatus;

public class UpdateRecordStatusCommandHandler(IRecordStatusRepository recordStatusRepository) : IRequestHandler<UpdateRecordStatusCommand, Result>
{
    public async Task<Result> Handle(UpdateRecordStatusCommand request, CancellationToken cancellationToken)
    {
        var recordStatus = await recordStatusRepository.UpdateAsync(
            recordStatusId: request.RecordStatusId,
            nameRu: request.NameRu,
            nameKk: request.NameKk,
            nameEn: request.NameEn,
            descriptionRu: request.DescriptionRu,
            descriptionKk: request.DescriptionKk,
            descriptionEn: request.DescriptionEn,
            createdBy: request.CreatedBy
            ); ;
        if (recordStatus is null)
        {
            return Result.Failure(new Error(Errors.BadRequest, "UpdateError"));
        }
        return Result.Success();
    }
}
