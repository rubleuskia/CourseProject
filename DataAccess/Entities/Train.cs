using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DatabaseAccess
{
    public class Train
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public TrainType Type { get; set; }

        public List<Wagon> Wagons { get; set; }

        public List<Ticket> Tickets { get; set; }

        public List<Stop> Stops { get; set; }  
    }
}
