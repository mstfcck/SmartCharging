using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartCharging.Core.Exceptions;
using SmartCharging.Domain.Repositories;

namespace SmartCharging.Application.ChargeStation.Commands.DeleteChargeStation;

public class DeleteChargeStationHandler : IRequestHandler<DeleteChargeStationCommand>
{
    private readonly IEntityFrameworkCoreUnitOfWork _unitOfWork;

    public DeleteChargeStationHandler(IEntityFrameworkCoreUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Unit> Handle(DeleteChargeStationCommand request, CancellationToken cancellationToken)
    {
        var chargeStation = await _unitOfWork.Repository<Domain.Entities.ChargeStation>().Read()
            .Where(x => x.Id == request.ChargeStationId && x.GroupId == request.GroupId)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (chargeStation == null)
        {
            throw new BusinessException("Charge Station could not be found.");
        }

        // PS: The transaction isn't worked while using a memory database.

        await _unitOfWork.BeginTransactionAsync();

        _unitOfWork.Repository<Domain.Entities.ChargeStation>().Delete(chargeStation);

        await _unitOfWork.SaveChangesAsync();

        await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}