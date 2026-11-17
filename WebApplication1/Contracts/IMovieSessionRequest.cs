namespace WebApplication1.Contracts
{
    public interface IMovieSessionRequest
    {
        int MovieId { get; }
        DateTime StartsAt { get; }
        string HallName { get; }
        decimal Price { get; }
        int AvailableSeats { get; }
    }
}
