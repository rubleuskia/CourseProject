using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DatabaseAccess
{
    public class User
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string Password { get; set; }

        public List<Passenger> Passengers { get; set; }

        public int InfoUserId { get; set; }
        public InfoUser InfoUser { get; set; }
    }
}
