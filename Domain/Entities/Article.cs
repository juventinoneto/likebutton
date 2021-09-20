
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Article
    {
        public long Id { get; }

        public string Content { get; set; }

        public string Description { get; set; }
        
        public DateTime Date { get; set; }
        
        public IList<Like> Likes { get; }
        
        protected Article()
        {
            Likes = new List<Like>();
        }

        public void RegisterLike()
        {
            var like = Like.NewLike(this);
            Likes.Add(like);
        }
        
        public static Article NewArticle(string content, string description)
        {
            return new Article()
            {
                Content = content,
                Description = description,
                Date = DateTime.Now
            };
        }
    }
}