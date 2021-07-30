using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DatabaseAccess
{
    public class Station
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }

        public List<Stop> Stops { get; set; }
    }
}
