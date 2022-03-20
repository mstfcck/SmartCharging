using MediatR;

namespace SmartCharging.Application.ChargeStation.Commands.UpdateChargeStation;

public class UpdateChargeStationHandler : IRequestHandler<UpdateChargeStationCommand>
{
    public UpdateChargeStationHandler()
    {
    }
    
    public Task<Unit> Handle(UpdateChargeStationCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}