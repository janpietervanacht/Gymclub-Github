using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DtoModels
{
    public class Person
    {
        public int Id { get; set; } // Bij geen code-first: zet in SQL server de Prim Key + Identity specification (Is Identity)  = true
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}
