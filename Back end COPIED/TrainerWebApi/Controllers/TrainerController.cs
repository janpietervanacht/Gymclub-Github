using Framework.DtoModels;
using LogicTrainer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace TrainerWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class TrainerController : ControllerBase
    {
        // private readonly WebApiDbContext _context;
        private readonly ITrainerService _trainerService; // jpjp service van maken

        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        [HttpGet("getList")]
        public List<Trainer> GetList()
        {
            return _trainerService.GetList();

        }

        [HttpGet("{id}")]
        public Trainer Get(int id)
        {
            return _trainerService.Get(id);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _trainerService.Delete(id);
        }

        [HttpPost]
        public int Add(Trainer trainer)
        {
            return _trainerService.Add(trainer);
        }

        [HttpPut]
        public void Update(Trainer trainer)
        {
            _trainerService.Update(trainer);
        }

        [HttpGet("recover")]
        public void Recover()
        {
            _trainerService.Recover();
        }
    }
}