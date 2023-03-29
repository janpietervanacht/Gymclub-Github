namespace DataAccess.Models
{
    public class Trainer: EntityBase
    {
        public int PersonId { get; set; }
        public bool IsCertified { get; set; }
        public ICollection<TrainerCertificate> Certificates { get; set; }
        public virtual Person Person { get; set; }
    }
}
