using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class Account : BaseVersionedEntity
    {
        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }
    }
}
