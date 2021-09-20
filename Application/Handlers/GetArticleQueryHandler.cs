using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Interfaces.DTO;
using Application.Queries;
using Application.Responses;
using Domain.Interfaces;
using MediatR;

namespace Application.Handlers
{
    public class GetArticleQueryHandler : IRequestHandler<GetArticleQuery, ResponseBase<ArticleResponse>>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IRedisService _redisService;
        
        public GetArticleQueryHandler(IArticleRepository articleRepository, IRedisService redisService)
        {
            _articleRepository = articleRepository;
            _redisService = redisService;
        }
        
        public async Task<ResponseBase<ArticleResponse>> Handle(GetArticleQuery request, CancellationToken cancellationToken)
        {
            var article = await _redisService.ReadOne(request.ArticleId);

            if (article == null)
            {
                var dbArticle = _articleRepository.FindById(request.ArticleId);
                article = new ArticleCachedDTO(dbArticle.Id, dbArticle.Content, dbArticle.Description,
                    dbArticle.Likes.Count);
            }
            
            var response = new ArticleResponse(article.Id, article.Content, article.Description, article.CountLikes);
            
            return await Task.FromResult(new ResponseBase<ArticleResponse>(response));
        }
    }
}