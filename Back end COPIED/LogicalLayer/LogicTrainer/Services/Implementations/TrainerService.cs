using DataAccess.Repositories.Interfaces;
using Framework.DtoModels;
using LogicTrainer.Services.Interfaces;

namespace LogicTrainer.Services.Implementations
{
    public class TrainerService: ITrainerService
    {
        private readonly ITrainerRepository _trainerRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly ITrainerCertificateRepository _trainerCertificateRepository;
        private readonly IPersonRepository _personRepository;

        public TrainerService(ITrainerRepository trainerRepository, IPersonRepository personRepository, ITrainerCertificateRepository trainerCertificateRepository, IMemberRepository memberRepository)
        {
            _trainerRepository = trainerRepository;
            _personRepository = personRepository;
            _trainerCertificateRepository = trainerCertificateRepository;
            _memberRepository = memberRepository;
        }

        public List<Trainer> GetList()
        {
            // oefening anonymous object
            var jpjp = new
            {
                Id = 0,
                Name = "japie",
                Titels = new
                {
                    Diploma = "TUE",
                    Sport = "Gymclub"
                }
            };
            Thread.Sleep(2000); // om front end te testen
            return _trainerRepository.GetList();
        }

        public Trainer Get(int id)
        {
            Thread.Sleep(2000); // om front end te testen
            return _trainerRepository.Get(id);
        }

        public int Add(Trainer trainer)
        {
            Thread.Sleep(2000); // om front end te testen

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
            Thread.Sleep(2000); // om front end te testen
            _trainerRepository.Update(trainer);
            trainer.Person.Id = trainer.PersonId;
            _personRepository.Update(trainer.Person);
        }

        public void Delete(int id)
        {
            var personId = _trainerRepository.Get(id).PersonId;
            _trainerRepository.Delete(id);
            var member = _memberRepository.GetMemberByPersonId(personId);
            if (member != null)
            {
                member.IsAlsoTrainer = false;
                _memberRepository.Update(member);
            }
        }

        public void Recover()
        {
            Thread.Sleep(2000); // om front end te testen
            _trainerRepository.Recover();
        }
    }
}
