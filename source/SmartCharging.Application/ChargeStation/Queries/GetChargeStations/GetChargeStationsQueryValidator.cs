using FluentValidation;

namespace SmartCharging.Application.ChargeStation.Queries.GetChargeStations;

public class GetChargeStationsQueryValidator : AbstractValidator<GetChargeStationsQuery>
{
    public GetChargeStationsQueryValidator()
    {
    }
}