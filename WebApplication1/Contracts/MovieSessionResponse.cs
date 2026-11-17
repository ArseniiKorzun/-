namespace WebApplication1.Contracts
{
    public class MovieSessionResponse
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public DateTime StartsAt { get; set; }
        public string HallName { get; set; }
        public decimal Price { get; set; }
        public int AvailableSeats { get; set; }
    }
}
