using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain.Services;

public class ArticleService
{
    private readonly IArticleRepository _articleRepository;

    public ArticleService(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public void AddArticle(string content, string description)
    {
        if (content.Length > 30 || description.Length > 30)
        {
            throw new ValidateDomainException(
                "The fields Content and Description cannot be larger than 30 characters.");
        }
        var article = Article.NewArticle(content, description);
        _articleRepository.Add(article);
    }

    public void LikeArticle(long articleId)
    {
        var article =  _articleRepository.FindById(articleId);

        if (article == null)
        {
            throw new ElementNotFoundException("No article founded.");
        }
        
        article.RegisterLike();
        _articleRepository.Update(article);
    }
}
