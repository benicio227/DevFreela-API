﻿using DevFreela.Application.Commands.ProjectFolder.InsertProject;
using DevFreela.Application.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application;
public static class ApplicationModule // classe estatica, não pode ser instanciada
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    // O método AddApplication é um método de extensão para IServiceCoolection, o que significa que pode
    // ser chamado diretamente em services dentro de Program.cs
    {
        services.AddHandlers();

        return services;
    }


    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<InsertProjectCommand>());

        services.AddTransient<IPipelineBehavior<InsertProjectCommand, ResultViewModel<int>>, ValidateInsertProjectCommandBehavior>();

        return services;
    }
}

// Isso facilita a manutenção do código e reduz o acoplamento, pois quem usa IProjectService não precisa saber
// como criar ProjectService, apenas recebe uma instancia pronta