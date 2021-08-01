namespace DataAccess.Entities
{
    public class BaseVersionedEntity : BaseEntity
    {
        public byte[] RowVersion { get; set; }
    }
}
