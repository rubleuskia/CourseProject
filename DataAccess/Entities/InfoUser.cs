using System;
using System.ComponentModel.DataAnnotations;

namespace DatabaseAccess
{
    public class InfoUser
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Surname { get; set; }
        [MaxLength(50)]
        public string Patronymic { get; set; }
        [MaxLength(50)]
        public string Phone { get; set; }
        public Sex Sex { get; set; }
        public DateTime DateOfBirth { get; set; }

        public User User { get; set; }

        public int PassengerId { get; set; }
        public Passenger Passenger { get; set; }

    }
}
