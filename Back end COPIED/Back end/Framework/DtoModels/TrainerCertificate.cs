namespace Framework.DtoModels
{
    public class TrainerCertificate
    {
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public string Name { get; set; }
        // public Trainer Trainer { get; set; } // mag weg, FK is al geregeld 
    }
}