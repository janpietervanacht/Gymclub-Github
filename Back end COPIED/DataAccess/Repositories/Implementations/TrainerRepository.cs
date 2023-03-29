using DataAccess.ApplicDbContext;
using DataAccess.Repositories.Helpers;
using DataAccess.Repositories.Interfaces;
using Framework.Enums;
using Microsoft.EntityFrameworkCore;
using Trainer = DataAccess.Models.Trainer;
using TrainerDto = Framework.DtoModels.Trainer;
using TrainerCertificate = DataAccess.Models.TrainerCertificate;
using TrainerCertificateDto = Framework.DtoModels.TrainerCertificate;
using PersonDto = Framework.DtoModels.Person;

namespace DataAccess.Repositories.Implementations
{
    public class TrainerRepository : ITrainerRepository
    {
        readonly DatabaseContext _dbContext;

        public TrainerRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }


        private IQueryable<TrainerDto> ToDto(IQueryable<Trainer> qryDb)
        {
            var result = qryDb.Select(train => new TrainerDto
            {
                Id = train.Id,
                PersonId = train.PersonId,
                IsCertified = train.IsCertified,
                Certificates = train.Certificates.Where(cert => !cert.IsDeleted)
                        .Select(cert => new TrainerCertificateDto
                        {
                            Id = cert.Id,
                            TrainerId = train.Id,
                            Name = cert.Name,
                        }),
                Person = new PersonDto
                {
                    Id = train.PersonId,
                    FirstName = train.Person.FirstName,
                    MiddleName = train.Person.MiddleName,
                    LastName = train.Person.LastName,
                }
            });
            return result;
        }

        private Trainer ToDbo(TrainerDto trainerDto, Trainer entity, DbAction dbAction)
        {
            if (entity == null)
            {
                entity = new Trainer();
            }

            entity.PersonId = trainerDto.PersonId;
            entity.IsCertified = trainerDto.IsCertified;
            RepositoryHelper.SetAuditFields(entity, dbAction);

            return entity;
        }

        public List<TrainerDto> GetList()
        {
            var qryDb = _dbContext.Trainers.Where(tr => !tr.IsDeleted);
            var result = ToDto(qryDb);
            return result.ToList();
        }

        public TrainerDto Get(int id)
        {
            var qryDb = _dbContext.Trainers.Where(tr => !tr.IsDeleted && tr.Id == id);
            var result = ToDto(qryDb).SingleOrDefault();
            return result;
        }

        public int Add(TrainerDto trainer)
        {
            var itemDb = ToDbo(trainer, null, DbAction.Add);
            _dbContext.Trainers.Add(itemDb);
            _dbContext.SaveChanges();
            return itemDb.Id;
        }

        public void Delete(int id)
        {
            var trainer = _dbContext.Trainers.Find(id);
            trainer.IsDeleted = true;
            _dbContext.Entry(trainer).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Update(TrainerDto trainer)
        {
            var entity = _dbContext.Trainers.Find(trainer.Id);
            var itemDb = ToDbo(trainer, entity, DbAction.Update);
            _dbContext.Entry(itemDb).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Recover()
        {
            _dbContext.Database.ExecuteSqlRaw($"update trainers set isDeleted = 0");
        }

        public TrainerDto GetTrainerByPersonId(int personId)
        {
            var qryDb = _dbContext.Trainers.Where(tr => !tr.IsDeleted && tr.PersonId == personId);
            return ToDto(qryDb).SingleOrDefault();
        }
    }
}
