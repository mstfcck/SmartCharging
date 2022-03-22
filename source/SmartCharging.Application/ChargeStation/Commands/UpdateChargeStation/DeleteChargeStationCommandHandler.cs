using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartCharging.Core.Exceptions;
using SmartCharging.Domain.Repositories;

namespace SmartCharging.Application.ChargeStation.Commands.UpdateChargeStation;

public class DeleteChargeStationCommandHandler : IRequestHandler<DeleteChargeStationCommand>
{
    private readonly IEntityFrameworkCoreUnitOfWork _unitOfWork;

    public DeleteChargeStationCommandHandler(IEntityFrameworkCoreUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Unit> Handle(DeleteChargeStationCommand request, CancellationToken cancellationToken)
    {
        var chargeStation = await _unitOfWork.Repository<Domain.Entities.ChargeStation>().Read()
            .Where(x => x.Id == request.ByChargeStationId && x.GroupId == request.ByGroupId)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (chargeStation == null)
        {
            throw new BusinessException(ExceptionMessages.ChargeStationCouldNotBeFound);
        }
        
        chargeStation.Update(request.Name);

        // PS: The transaction isn't worked while using a memory database.

        await _unitOfWork.BeginTransactionAsync();

        await _unitOfWork.Repository<Domain.Entities.ChargeStation>().UpdateAsync(chargeStation, request.ByChargeStationId);

        await _unitOfWork.SaveChangesAsync();

        await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}