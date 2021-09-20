namespace Application.Interfaces.DTO
{
    public class ArticleCachedDTO
    {
        public long Id { get; }

        public string Content { get; }
        
        public string Description { get; }
        
        public long CountLikes { get; }

        public ArticleCachedDTO(long id, string content, string description, long countLikes)
        {
            Id = id;
            Content = content;
            Description = description;
            CountLikes = countLikes;
        }
    }
}