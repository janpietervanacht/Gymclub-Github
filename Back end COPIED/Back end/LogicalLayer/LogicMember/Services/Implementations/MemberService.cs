using DataAccess.Repositories.Interfaces;
using Framework.DtoModels;
using LogicMember.Services.Interfaces;

namespace LogicMember.Services.Implementations
{
    public class MemberService: IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IPersonRepository _personRepository;

        public MemberService(IMemberRepository memberRepository, IPersonRepository personRepository)
        {
            _memberRepository = memberRepository;
            _personRepository = personRepository;
        }

        public List<Member> GetList()
        {
            Thread.Sleep(2000); // om front end te testen
            return _memberRepository.GetList();
        }

        public Member Get(int id)
        {
            Thread.Sleep(2000); // om front end te testen
            return _memberRepository.Get(id);
        }

        public int Add(Member member)
        {
            Thread.Sleep(2000); // om front end te testen
            var personId = _personRepository.Add(member.Person);
            member.PersonId = personId;
            return _memberRepository.Add(member);
        }

        public void Update(Member member)
        {
            Thread.Sleep(2000); // om front end te testen
            _memberRepository.Update(member);
            member.Person.Id = member.PersonId;
            _personRepository.Update(member.Person);
        }

        public void Delete(int id)
        {
            Thread.Sleep(2000); // om front end te testen
            _memberRepository.Delete(id);
        }

        public void Recover()
        {
            Thread.Sleep(4000); // om front end te testen
            _memberRepository.Recover();
        }
    }
}
