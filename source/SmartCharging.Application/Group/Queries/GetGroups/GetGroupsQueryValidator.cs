using FluentValidation;

namespace SmartCharging.Application.Group.Queries.GetGroups;

public class GetGroupsQueryValidator : AbstractValidator<GetGroupQuery>
{
    public GetGroupsQueryValidator()
    {
    }
}