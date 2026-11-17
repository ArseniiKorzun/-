namespace WebApplication1.Contracts
{
    public class UpdateMovieSessionRequest : IMovieSessionRequest
    {
        public int MovieId { get; set; }

        public DateTime StartsAt { get; set; }

        public string HallName { get; set; }

        public decimal Price { get; set; }

        public int AvailableSeats { get; set; }
    }
}
