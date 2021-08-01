using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Entities
{
    [Table("AspNetUsers")]
    public class User : IdentityUser
    {
        public int Age { get; set; }
    }
}
