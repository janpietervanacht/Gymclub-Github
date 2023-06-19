using Framework.Enums;

namespace DataAccess.Models
{
    public class Member: EntityBase
    {
        public int PersonId { get; set; }
        public MemberLevel Level { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsAlsoTrainer { get; set; }
        public virtual Person Person { get; set; }
    }
}
