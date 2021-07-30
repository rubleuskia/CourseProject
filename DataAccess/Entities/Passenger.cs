using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DatabaseAccess
{
    public class Passenger
    {
        public int Id { get; set; }
        public DocumentType DocumentType { get; set; }
        [MaxLength(50)]
        public string DocumentNumber { get; set; }
        public Citizenship Citizenship { get; set; }

        public List<Ticket> Tickets { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int InfoUserId { get; set; }
        public InfoUser InfoUser { get; set; }

    }
}
