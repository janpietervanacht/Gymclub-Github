namespace Framework.DtoModels
{
    public class Trainer
    {
        public int Id { get; set; } // Bij geen code-first: zet in SQL server de Prim Key + Identity specification (Is Identity)  = true
        public int PersonId { get; set; }
        public bool IsCertified { get; set; }
        public IEnumerable<TrainerCertificate> Certificates { get; set; }

        public virtual Person Person { get; set; }
    }
}