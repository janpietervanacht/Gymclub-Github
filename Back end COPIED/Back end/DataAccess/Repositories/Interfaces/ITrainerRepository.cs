using TrainerDto = Framework.DtoModels.Trainer;

namespace DataAccess.Repositories.Interfaces
{
    public interface ITrainerRepository
    {
        List<TrainerDto> GetList();
        TrainerDto Get(int id);
        int Add(TrainerDto trainer);
        void Update(TrainerDto trainer);
        void Delete(int id);
    }
}
