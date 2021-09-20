using System.Drawing;
using Application.Responses;
using MediatR;

namespace Application.Queries
{
    public class GetArticleQuery : IRequest<ResponseBase<ArticleResponse>>
    {
        public long ArticleId { get; }

        public GetArticleQuery(long articleId)
        {
            ArticleId = articleId;
        }
    }
}