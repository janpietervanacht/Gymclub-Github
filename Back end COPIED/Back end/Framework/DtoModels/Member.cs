using Framework.Enums;
using System.Text.Json.Serialization;

namespace Framework.DtoModels
{
    public class Member
    {
        public int Id { get; set; } // Bij geen code-first: zet in SQL server de Prim Key + Identity specification (Is Identity)  = true
        public int PersonId { get; set; }
        public MemberLevel Level { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsAlsoTrainer { get; set; }
        public virtual Person Person { get; set; }
    }
}
