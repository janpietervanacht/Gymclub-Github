using DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ApplicDbContext
{
    // Best practice: noem onderstaande class niet DbContext
    // want er is een EF component die ook deze class gebruikt, dus er is een ambiguiteit
    // (het kan wel, maar dan moet je de namespace "DataAccess.ApplicDbContext" ook gebruiken)
    public class DatabaseContext : IdentityDbContext
    // DatabaseContext  / DbSet: gebruiken als je tabellen vanuit de code
    // creëert (CODE FIRST). Let op: Database wel eerst
    // aanmaken in SQL Server. 
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        //public DbSet<Member> Members { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<TrainerCertificate> TrainerCertificates { get; set; }
        public DbSet<Person> Persons { get; set; }
    }
}
