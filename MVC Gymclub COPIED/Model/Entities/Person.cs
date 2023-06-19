using System.ComponentModel;

namespace Model.Entities
{
    public class Person: EntityBase
    {
        [DisplayName("Voornaam:")]
        public string? FirstName { get; set; }

        [DisplayName("Tussenvoegsel:")]
        public string? MiddleName { get; set; }

        [DisplayName("Achternaam:")]
        public string? LastName { get; set; }
    }
}
