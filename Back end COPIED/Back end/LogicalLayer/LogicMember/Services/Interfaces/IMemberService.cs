using Framework.DtoModels;

namespace LogicMember.Services.Interfaces
{
    public interface IMemberService
    {
        List<Member> GetList();
        Member Get(int id);
        int Add(Member member);
        void Update(Member member);
        void Delete(int id);
        void Recover();
    }
}
