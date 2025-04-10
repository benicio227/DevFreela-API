﻿using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.UserFolder.InsertSkill;
public class InsertSkillCommand : IRequest<ResultViewModel<UserSkillsInputModel>>
{
    public int Id { get; set; }
    public int[] SkillsIds { get; set; } = Array.Empty<int>();
}
