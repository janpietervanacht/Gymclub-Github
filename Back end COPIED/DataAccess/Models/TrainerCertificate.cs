namespace DataAccess.Models
{
    public class TrainerCertificate: EntityBase
    {
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public string Name { get; set; }
        public virtual Trainer Trainer { get; set; }
    }
}
