using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Services;
using Moq;
using Xunit;

namespace Test;

public class ArticleServiceTest
{
    private readonly ArticleService _service;
    private readonly Mock<IArticleRepository> _repository;

    public ArticleServiceTest()
    {
        _repository = new Mock<IArticleRepository>();
        _service = new ArticleService(_repository.Object);
    }
    
    [Fact]
    public void Test_LikeArticle_ThrowingException()
    {
        // arrange
        var article = Article.NewArticle("This content can be used", "This description can be used");
        
        // act and assert
        Assert.Throws<ElementNotFoundException>(() => _service.LikeArticle(It.IsAny<long>()));
    }
    
    [Fact]
    public void Test_LikeArticle_NotThrowingException()
    {
        // arrange
        var article = Article.NewArticle("This content can be used", "This description can be used");
        _repository.Setup(x => x.FindById(It.IsAny<long>()))
            .Returns(article);
        
        // act
        _service.LikeArticle(It.IsAny<long>());
        
        // assert
        _repository.Verify(x => x.Update(It.IsAny<Article>()));
    }
    
    [Fact]
    public void Test_AddArticle_ThrowingException()
    {
        // arrange
        var article = Article.NewArticle("This content can be used", "This description cannot be used because it's too much long....................");
        
        // act and assert
        Assert.Throws<ValidateDomainException>(() => _service.AddArticle(article.Content, article.Description));
    }
    
    [Fact]
    public void Test_AddArticle_NotThrowingException()
    {
        // arrange
        var article = Article.NewArticle("This content can be used", "This description can be used");
        
        // act
        _service.AddArticle(article.Content, article.Description);

        // assert
        _repository.Verify(x => x.Add(It.IsAny<Article>()));
    }
}
