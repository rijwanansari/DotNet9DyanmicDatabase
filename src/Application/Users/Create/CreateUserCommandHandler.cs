using System;
using Application.Common.Interfaces;
using Domain;
using MediatR;

namespace Application.Users.Create;

public class CreateUserCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<CreateUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email
        };

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync(cancellationToken);

        return user.Id;
        
    }
}
