using System;

namespace Domain.Entities;

public class Like
{
    public long Id { get; set; }
    
    public DateTime ActionDate { get; set; }
    
    public long ArticleId { get; set; }
    
    public Article Article { get; set; }

    public static Like NewLike(Article article)
    {
        return new Like()
        {
            ArticleId = article.Id,
            Article = article,
            ActionDate = DateTime.Now
        };
    }    
}
