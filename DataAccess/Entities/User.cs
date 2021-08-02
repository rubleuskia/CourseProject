using Microsoft.AspNetCore.Identity;

namespace DataAccess.Entities
{
    public class User : IdentityUser
    {
        public byte[] RowVersion { get; set; }

        public int Age { get; set; }
    }
}