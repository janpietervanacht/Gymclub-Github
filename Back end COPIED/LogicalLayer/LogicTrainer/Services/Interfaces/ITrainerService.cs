using Framework.DtoModels;

namespace LogicTrainer.Services.Interfaces
{
    public interface ITrainerService
    {
        List<Trainer> GetList();
        Trainer Get(int id);
        int Add(Trainer trainer);
        void Update(Trainer trainer);
        void Delete(int id);
        void Recover();

    }
}
