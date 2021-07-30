namespace DatabaseAccess
{
    public class Place
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public PlaceStatus Status { get; set; }

        public int WagonId { get; set; }
        public Wagon Wagon { get; set; }

        public Ticket Ticket { get; set; }
    }
}
