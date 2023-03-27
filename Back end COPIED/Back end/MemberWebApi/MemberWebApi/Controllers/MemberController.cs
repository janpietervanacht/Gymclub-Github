using Framework.DtoModels;
using LogicMember.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MemberWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class MemberController : ControllerBase
    {
        // private readonly WebApiDbContext _context;
        private readonly IMemberService _memberService; // jpjp service van maken

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet("getList")]
        public List<Member> GetList()
        {
            return _memberService.GetList();
        }

        [HttpGet("{id}")]
        public Member Get(int id)
        {
            return _memberService.Get(id);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
             _memberService.Delete(id);
        }

        [HttpPost]
        public int Add(Member member)
        {
            return _memberService.Add(member);
        }

        [HttpGet("recover")]
        public void Recover()
        {
            _memberService.Recover();
        }

        [HttpPut]
        public void Update(Member member)
        {
            _memberService.Update(member);
        }
    }
}