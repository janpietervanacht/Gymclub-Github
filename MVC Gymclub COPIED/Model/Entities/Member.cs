using Framework.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Model.Entities
{
    public class Member: EntityBase
    {
        [Required(ErrorMessage = "Startdatum is verplicht")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Startdatum:")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Einddatum:")]
        public DateTime? EndDate { get; set; }

        [DisplayName("Is ook trainer:")]
        public bool IsAlsoTrainer { get; set; }

        public int PersonId { get; set; }
        
        public Person Person { get; set;}

        [DisplayName("Niveau:")]
        public MemberLevel Level { get; set; }
    }
}
