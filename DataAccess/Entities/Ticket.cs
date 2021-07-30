namespace DatabaseAccess
{
    public class Ticket
    {
        public int Id { get; set; }
        public TicketStatus Status { get; set; }

        public int PassengerId { get; set; }
        public Passenger Passenger { get; set; }

        public int TrainId { get; set; }
        public Train Train { get; set; }

        public int PlaceId { get; set; }
        public Place Place { get; set; }

    }
}
