using TrainerCertificateDto = Model.Entities.TrainerCertificate;
namespace Model.Entities
{
    public class Trainer : EntityBase
    {
        public bool IsCertified { get; set; }
        
        public IEnumerable<TrainerCertificateDto> Certificates { get; set; }
        
        public Person Person { get; set; }
    }
}
