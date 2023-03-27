using DataAccess.Repositories.Interfaces;
using Framework.DtoModels;
using LogicTrainer.Services.Interfaces;

namespace LogicTrainer.Services.Implementations
{
    public class TrainerService: ITrainerService
    {
        private readonly ITrainerRepository _trainerRepository;
        private readonly ITrainerCertificateRepository _trainerCertificateRepository;
        private readonly IPersonRepository _personRepository;

        public TrainerService(ITrainerRepository trainerRepository, IPersonRepository personRepository, ITrainerCertificateRepository trainerCertificateRepository)
        {
            _trainerRepository = trainerRepository;
            _personRepository = personRepository;
            _trainerCertificateRepository = trainerCertificateRepository;
        }

        public List<Trainer> GetList()
        {
            return _trainerRepository.GetList();
        }

        public Trainer Get(int id)
        {
            return _trainerRepository.Get(id);
        }

        public int Add(Trainer trainer)
        {
            var personId = _personRepository.Add(trainer.Person);
            trainer.PersonId = personId;
            var trainerId = _trainerRepository.Add(trainer);
            foreach (var cert in trainer.Certificates)
            {
                cert.TrainerId = trainerId;
                _trainerCertificateRepository.Add(cert);
            }
            return trainerId;
        }

        public void Update(Trainer trainer)
        {
            _trainerRepository.Update(trainer);
            trainer.Person.Id = trainer.PersonId;
            _personRepository.Update(trainer.Person);
        }

        public void Delete(int id)
        {
            _trainerRepository.Delete(id);
        }
    }
}
