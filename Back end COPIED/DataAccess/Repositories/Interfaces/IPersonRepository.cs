using PersonDto = Framework.DtoModels.Person;

namespace DataAccess.Repositories.Interfaces
{
    public interface IPersonRepository
    {
        public int Add(PersonDto person);
        void Update(PersonDto person);

    }
}
