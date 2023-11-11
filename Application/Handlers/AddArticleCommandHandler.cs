using System.Threading;
using System.Threading.Tasks;
using Application.Commands;
using Domain.Services;
using MediatR;

namespace Application.Handlers;

public class AddArticleCommandHandler : IRequestHandler<AddArticleCommand, bool>
{
    private readonly ArticleService _service;
    
    public AddArticleCommandHandler(ArticleService service)
    {
        _service = service;
    }
    
    public async Task<bool> Handle(AddArticleCommand request, CancellationToken cancellationToken)
    {
        _service.AddArticle(request.Content, request.Description);

        return await Task.FromResult(true);
    }
}
