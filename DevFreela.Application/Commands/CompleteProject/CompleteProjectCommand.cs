using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.CompleteProject;
public class CompleteProjectCommand : IRequest<ResultViewModel>
 // O IRequest é uma interface do MediatR e nós vamos passar um tipo para ele que vai ser o tipo de retorno.
 // Isso vai ser utilizado para que o próprio MediatR saiba para qual Handler ele vai delegar esse Command para ser
 // executado
{
    public CompleteProjectCommand(int id)
    {
        Id = id;
    }
    public int Id { get; set; }
}
