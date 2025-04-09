﻿using DevFreela.Application.Models;

namespace DevFreela.Application.Services;
public interface ISkillService
{
    ResultViewModel<List<CreateSkillInputModel>> GetAll();
    ResultViewModel<int> Insert(CreateSkillInputModel model);
}
