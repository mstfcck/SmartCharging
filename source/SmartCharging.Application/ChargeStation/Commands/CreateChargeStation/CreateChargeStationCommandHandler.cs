using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartCharging.Core.Exceptions;
using SmartCharging.Domain.Repositories;

namespace SmartCharging.Application.ChargeStation.Commands.CreateChargeStation;

public class CreateChargeStationCommandHandler : IRequestHandler<CreateChargeStationCommand>
{
    private readonly IEntityFrameworkCoreUnitOfWork _unitOfWork;

    public CreateChargeStationCommandHandler(IEntityFrameworkCoreUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreateChargeStationCommand request, CancellationToken cancellationToken)
    {
        var group = await _unitOfWork.Repository<Domain.Entities.Group>().Read()
            .Where(x => x.Id == request.ByGroupId)
            .FirstOrDefaultAsync(cancellationToken);

        if (group == null)
        {
            throw new BusinessException("Charge Station could not be found.");
        }

        var chargeStation = new Domain.Entities.ChargeStation(request.Name, request.ByGroupId);

        // PS: The transaction isn't worked while using a InMemory database.

        await _unitOfWork.BeginTransactionAsync();

        await _unitOfWork.Repository<Domain.Entities.ChargeStation>().CreateAsync(chargeStation);

        await _unitOfWork.SaveChangesAsync();

        await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}