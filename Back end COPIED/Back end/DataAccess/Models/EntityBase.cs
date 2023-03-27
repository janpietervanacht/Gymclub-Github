namespace DataAccess.Models
{
    public class EntityBase
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
