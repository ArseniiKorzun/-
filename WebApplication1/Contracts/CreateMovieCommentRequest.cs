namespace WebApplication1.Contracts
{
    public class CreateMovieCommentRequest : IMovieCommentRequest
    {
        public string AuthorName { get; set; }

        public string Text { get; set; }
    }
}
