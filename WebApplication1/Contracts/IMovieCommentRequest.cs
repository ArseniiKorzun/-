namespace WebApplication1.Contracts
{
    public interface IMovieCommentRequest
    {
        string AuthorName { get; }
        string Text { get; }
    }
}
