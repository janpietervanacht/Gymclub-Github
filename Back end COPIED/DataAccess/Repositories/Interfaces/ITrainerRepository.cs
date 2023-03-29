using TrainerDto = Framework.DtoModels.Trainer;

namespace DataAccess.Repositories.Interfaces
{
    public interface ITrainerRepository
    {
        List<TrainerDto> GetList();
        TrainerDto Get(int id);
        TrainerDto GetTrainerByPersonId(int personId);
        int Add(TrainerDto trainer);
        void Update(TrainerDto trainer);
        void Delete(int id);
        void Recover();

    }
}
