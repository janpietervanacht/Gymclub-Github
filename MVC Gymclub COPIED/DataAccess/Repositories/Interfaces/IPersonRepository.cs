using PersonDto = Model.Entities.Person;

namespace DataAccess.Repositories.Interfaces
{
    public interface IPersonRepository
    {
        public int Add(PersonDto person);
        void Update(PersonDto person);
    }
}
