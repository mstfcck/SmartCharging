using MediatR;

namespace SmartCharging.Application.ChargeStation.Commands.CreateChargeStation;

public class CreateChargeStationHandler : IRequestHandler<CreateChargeStationCommand>
{
    public CreateChargeStationHandler()
    {
    }
    
    public Task<Unit> Handle(CreateChargeStationCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}