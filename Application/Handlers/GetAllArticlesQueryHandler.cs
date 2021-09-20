using System.Collections.Generic;
using System.Linq;
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
    public class GetAllArticlesQueryHandler : IRequestHandler<GetArticlesQuery, ResponseBase<List<ArticleResponse>>>
    {
        private readonly IRedisService _redisService;
        
        public GetAllArticlesQueryHandler(IRedisService redisService)
        {
            _redisService = redisService;
        }
        
        public async Task<ResponseBase<List<ArticleResponse>>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
        {
            var articles = await _redisService.Read();

            var response = articles
                .Select(x =>
                    new ArticleResponse(x.Id, x.Content, x.Description, x.CountLikes))
                .ToList();
            
            return await Task.FromResult(new ResponseBase<List<ArticleResponse>>(response));
        }
    }
}