using MediatR;

namespace SmartCharging.Application.ChargeStation.Queries.GetChargeStations;

public class GetChargeStationsQueryHandler : IRequestHandler<GetChargeStationsQuery, GetChargeStationsDTO>
{
    public GetChargeStationsQueryHandler()
    {
    }
    
    public Task<GetChargeStationsDTO> Handle(GetChargeStationsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}