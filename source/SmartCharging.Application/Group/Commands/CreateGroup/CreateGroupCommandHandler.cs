using MediatR;
using SmartCharging.Domain.Repositories;

namespace SmartCharging.Application.Group.Commands.CreateGroup;

public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand>
{
    private readonly IEntityFrameworkCoreUnitOfWork _unitOfWork;

    public CreateGroupCommandHandler(IEntityFrameworkCoreUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = new Domain.Entities.Group(request.Name, request.CapacityInAmps);

        // PS: The transaction isn't worked while using a memory database.
        
        await _unitOfWork.BeginTransactionAsync();
        
        await _unitOfWork.Repository<Domain.Entities.Group>().CreateAsync(group);
        
        await _unitOfWork.SaveChangesAsync();
        
        await _unitOfWork.CommitAsync();
        
        return Unit.Value;
    }
}