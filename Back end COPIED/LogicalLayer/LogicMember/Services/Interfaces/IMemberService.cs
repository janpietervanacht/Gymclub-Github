using Framework.DtoModels;
using Framework.Errors;

namespace LogicMember.Services.Interfaces
{
    public interface IMemberService
    {
        List<Member> GetList();
        Member Get(int id);
        ResultMessage Add(Member member);
        ResultMessage Update(Member member);
        void Delete(int id);
        void Recover();
    }
}
