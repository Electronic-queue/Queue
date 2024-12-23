using FluentValidation;

namespace Queue.Application.Services.Queries.GetServiceList;

public class GetServiceListQueryValidator : AbstractValidator<GetServiceListQuery>
{
    public GetServiceListQueryValidator()
    {
    }
}
