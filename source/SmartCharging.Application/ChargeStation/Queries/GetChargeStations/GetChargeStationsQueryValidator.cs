using FluentValidation;

namespace SmartCharging.Application.ChargeStation.Queries.GetChargeStationsQuery;

public class GetChargeStationsQueryValidator : AbstractValidator<GetChargeStationsQuery>
{
    public GetChargeStationsQueryValidator()
    {
    }
}