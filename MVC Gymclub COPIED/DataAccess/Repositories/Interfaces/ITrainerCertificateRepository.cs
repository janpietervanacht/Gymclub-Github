
using TrainerCertificateDto = Model.Entities.TrainerCertificate;

namespace DataAccess.Repositories.Interfaces
{
    public interface ITrainerCertificateRepository 
        // We kennen alleen Add/Delete
        // Ophalen: via TrainerRepository
    {
        int Add(TrainerCertificateDto certificate);
        void Delete(int id);
    }
}
