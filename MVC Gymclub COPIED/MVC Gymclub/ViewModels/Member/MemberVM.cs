using Model.Entities;

namespace MVC_Gymclub.ViewModels.Member
{
    public class MemberVM
    {
        // Afgeleide velden: ------------------------------------------ 

        public string FullName { get; set; }

        // Basisvelden: 

        public Model.Entities.Member Member { get; set; }
    }
}