﻿using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetProjectById;

public class GetProjectByIdHanlder : IRequestHandler<GetProjectByIdQuery, ResultViewModel<ProjectViewModel>>
{
    private readonly IProjectRepository _repository;
    public GetProjectByIdHanlder(IProjectRepository repository)
    {
        _repository = repository;
    }
    public async Task<ResultViewModel<ProjectViewModel>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetDeatilsById(request.Id);

        if (project is null)
        {
            return ResultViewModel<ProjectViewModel>.Error("Projeto não existe");
        }

        var model = ProjectViewModel.FromEntity(project);

        return ResultViewModel<ProjectViewModel>.Success(model);
    }
}
