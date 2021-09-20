using System.Threading;
using System.Threading.Tasks;
using Application.Commands;
using Application.Interfaces;
using MediatR;

namespace Application.Handlers
{
    public class LikeCommandHandler : IRequestHandler<LikeArticleCommand, bool>
    {
        private readonly IPublisherService _publisherService;
        
        public LikeCommandHandler(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }
        
        public async Task<bool> Handle(LikeArticleCommand request, CancellationToken cancellationToken)
        {
            _publisherService.Publish(request.IdArticle);
            return await Task.FromResult(true);
        }
    }
}