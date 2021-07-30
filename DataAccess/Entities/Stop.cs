using System;

namespace DatabaseAccess
{
    public class Stop
    {
        public int Id { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }

        public int StationId { get; set; }
        public Station Station { get; set; }

        public int TrainId { get; set; }
        public Train Train { get; set; }
    }
}
