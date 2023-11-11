namespace Application.Responses;

public class ResponseBase<T> where T : class
{
    public T Content { get; }

    public ResponseBase(T content)
    {
        Content = content;
    }
}
