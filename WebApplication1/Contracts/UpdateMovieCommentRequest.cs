namespace WebApplication1.Contracts
{
    public class UpdateMovieCommentRequest : IMovieCommentRequest
    {
        public string AuthorName { get; set; }

        public string Text { get; set; }
    }
}
