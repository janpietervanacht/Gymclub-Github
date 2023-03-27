using DataAccess.ApplicDbContext;
using DataAccess.Repositories.Helpers;
using DataAccess.Repositories.Interfaces;
using Framework.Enums;
using Microsoft.EntityFrameworkCore;
using Person = DataAccess.Models.Person;
using PersonDto = Framework.DtoModels.Person;

namespace DataAccess.Repositories.Implementations
{
    public class PersonRepository: IPersonRepository
    {
        readonly DatabaseContext _dbContext;

        public PersonRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        private Person ToDbo(PersonDto personDto, Person entity, DbAction dbAction)
        {
            entity ??= new Person();

            entity.FirstName = personDto.FirstName;
            entity.MiddleName = personDto.MiddleName;
            entity.LastName  = personDto.LastName;
            RepositoryHelper.SetAuditFields(entity, dbAction);

            return entity;
        }

        public int Add(PersonDto person)
        {
            var itemDb = ToDbo(person, null, DbAction.Add);
            _dbContext.Persons.Add(itemDb);
            _dbContext.SaveChanges();
            return itemDb.Id;
        }

        public void Update(PersonDto person)
        {
            var entity = _dbContext.Persons.Find(person.Id);
            var itemDb = ToDbo(person, entity, DbAction.Update);
            _dbContext.Entry(itemDb).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        // JPJP  géén delete, een persoon kan zowel een trainer als member zijn
    }
}
