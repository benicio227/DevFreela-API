﻿using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.Project.DeleteProject;
public class DeleteProjectCommand : IRequest<ResultViewModel>
{
    public DeleteProjectCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
