using System.Collections.Generic;

namespace DatabaseAccess
{
    public class Wagon
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public WagonType Type { get; set; }
        public double Price { get; set; }

        public int TrainId { get; set; }
        public Train Train { get; set; }

        public List<Place> Places { get; set; }
    }
}
