using MediatR;

namespace Application.Commands;

public class LikeArticleCommand : IRequest<bool>
{
    public long IdArticle { get; }

    public LikeArticleCommand(long idArticle)
    {
        IdArticle = idArticle;
    }
}
