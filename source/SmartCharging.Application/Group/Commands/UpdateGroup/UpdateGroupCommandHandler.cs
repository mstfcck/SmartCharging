using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartCharging.Core.Exceptions;
using SmartCharging.Domain.Repositories;

namespace SmartCharging.Application.Group.Commands.UpdateGroup;

public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand>
{
    private readonly IEntityFrameworkCoreUnitOfWork _unitOfWork;

    public UpdateGroupCommandHandler(IEntityFrameworkCoreUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _unitOfWork.Repository<Domain.Entities.Group>().Read()
            .Where(x => x.Id == request.ByGroupId)
            .Include(x => x.ChargeStations)
            .ThenInclude(x => x.Connectors)
            .FirstOrDefaultAsync(cancellationToken);

        if (group == null)
        {
            throw new BusinessException("Group could not be found.");
        }

        var currentCapacityInConnectors = group.ChargeStations.Sum(x => x.Connectors.Sum(y => y.MaxCurrentInAmps));

        if (request.CapacityInAmps < currentCapacityInConnectors)
        {
            throw new BusinessException(
                $"The capacity in Amps of a Group should always be great or equal to the sum of the Max current in Amps " +
                $"of the Connector of all Charge Stations in the Group.'");
        }

        group.Update(request.Name, request.CapacityInAmps);

        // PS: The transaction isn't worked while using a memory database.

        await _unitOfWork.BeginTransactionAsync();

        await _unitOfWork.Repository<Domain.Entities.Group>().UpdateAsync(group, request.ByGroupId);

        await _unitOfWork.SaveChangesAsync();

        await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}