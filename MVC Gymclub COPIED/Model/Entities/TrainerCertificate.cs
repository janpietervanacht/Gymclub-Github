namespace Model.Entities
{
    public class TrainerCertificate: EntityBase
    {
        public int TrainerId { get; set; }
        public string Name { get; set; }
        public virtual Trainer Trainer { get; set; }
    }
}
