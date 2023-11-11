using MediatR;

namespace Application.Commands;

public class AddArticleCommand : IRequest<bool>
{
    public string Content { get; }
    
    public string Description { get; }

    public AddArticleCommand(string content, string description)
    {
        Content = content;
        Description = description;
    }
}
