using DataAccess.ApplicDbContext;
using DataAccess.Models;
using DataAccess.Repositories.Helpers;
using DataAccess.Repositories.Interfaces;
using Framework.Enums;
using Microsoft.EntityFrameworkCore;
using PersonDto = Model.Entities.Person;
using TrainerCertificateDto = Model.Entities.TrainerCertificate;

namespace DataAccess.Repositories.Implementations
{
    public class TrainerCertificateRepository : ITrainerCertificateRepository
    {
        readonly DatabaseContext _dbContext;

        public TrainerCertificateRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        private TrainerCertificate ToDbo(TrainerCertificateDto trainCertDto, TrainerCertificate entity, DbAction dbAction)
        {
            if (entity == null)
            {
                entity = new TrainerCertificate();
            }

            entity.TrainerId = trainCertDto.TrainerId;
            entity.Name = trainCertDto.Name;
            RepositoryHelper.SetAuditFields(entity, dbAction);
            return entity;
        }

        public int Add(TrainerCertificateDto trainerCert)
        {
            var itemDb = ToDbo(trainerCert, null, DbAction.Add);
            _dbContext.TrainerCertificates.Add(itemDb);
            _dbContext.SaveChanges();
            return itemDb.Id;
        }

        public void Delete(int id)
        {
            var trainerCert = _dbContext.TrainerCertificates.Find(id);
            trainerCert.IsDeleted = true;
            _dbContext.Entry(trainerCert).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
