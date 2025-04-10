﻿using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace DevFreela.Application.Commands.ProjectFolder.InsertProject;
public class ValidateInsertProjectCommandBehavior :
    IPipelineBehavior<InsertProjectCommand, ResultViewModel<int>>
{
    private readonly DevFreelaDbContext _context;
    public ValidateInsertProjectCommandBehavior(DevFreelaDbContext context)
    {
        _context = context;
    }
    public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, RequestHandlerDelegate<ResultViewModel<int>> next, CancellationToken cancellationToken)
    {
        var clientExists = _context.Users.Any(u => u.Id == request.IdClient);
        var freelaExists = _context.Users.Any(u => u.Id == request.IdFreelancer);

        if (!clientExists || !freelaExists)
        {
            return ResultViewModel<int>.Error("Cliente ou Freelancer inválidos");
        }

        return await next();
    }
}
