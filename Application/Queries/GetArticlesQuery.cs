using System.Collections.Generic;
using Application.Responses;
using MediatR;

namespace Application.Queries;

public class GetArticlesQuery :  IRequest<ResponseBase<List<ArticleResponse>>>
{
    
}
