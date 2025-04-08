using DevFreela.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application;
public static class ApplicationModule // classe estatica, não pode ser instanciada
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    // O método AddApplication é um método de extensão para IServiceCoolection, o que significa que pode
    // ser chamado diretamente em services dentro de Program.cs
    {
        services.AddServices(); // Chamamos o método AddServices para registrar os serviços na injeção de
                                // dependência

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProjectService, ProjectService>();

        return services;
    }
}

// Isso facilita a manutenção do código e reduz o acoplamento, pois quem usa IProjectService não precisa saber
// como criar ProjectService, apenas recebe uma instancia pronta