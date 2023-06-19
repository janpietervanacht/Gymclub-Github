using Model.Entities;

namespace MVC_Gymclub.ViewModels.Member
{
    public class MemberListVM
    {
        // Afgeleide velden: ------------------------------------------ 

        public int NrOfMembers { get; set; }

        // Basisvelden: 

        public List<MemberVM> Members { get; set; }
    }
}