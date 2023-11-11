namespace Application.Responses;

public class ArticleResponse
{
    public long Id { get; set; }
    
    public string ArticleContent { get; }
    
    public string ArticleDescription { get; }

    public long CountLikes { get; }

    public ArticleResponse(long id, string articleContent, string articleDescription, long countLikes)
    {
        Id = id;
        ArticleContent = articleContent;
        ArticleDescription = articleDescription;
        CountLikes = countLikes;
    }
}
