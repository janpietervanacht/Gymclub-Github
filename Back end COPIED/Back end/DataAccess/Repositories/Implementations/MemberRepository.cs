using DataAccess.ApplicDbContext;
using DataAccess.Repositories.Helpers;
using DataAccess.Repositories.Interfaces;
using Framework.Enums;
using Microsoft.EntityFrameworkCore;
using Member = DataAccess.Models.Member;
using MemberDto = Framework.DtoModels.Member;
using PersonDto = Framework.DtoModels.Person;

namespace DataAccess.Repositories.Implementations
{
    public class MemberRepository : IMemberRepository
    {
        readonly DatabaseContext _dbContext;

        public MemberRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }


        private IQueryable<MemberDto> ToDto(IQueryable<Member> qryDb)
        {
            var result = qryDb.Select(mbr => new MemberDto
            {
                Id = mbr.Id,
                PersonId = mbr.PersonId,
                Level = mbr.Level,
                StartDate = mbr.StartDate.Date,
                EndDate = mbr.EndDate.HasValue ? mbr.EndDate.Value.Date : null,
                IsAlsoTrainer = mbr.IsAlsoTrainer,
                Person = new PersonDto
                {
                    Id = mbr.PersonId,
                    FirstName = mbr.Person.FirstName,
                    MiddleName = mbr.Person.MiddleName,
                    LastName = mbr.Person.LastName,
                }
            });
            return result;
        }

        private Member ToDbo(MemberDto memberDto, Member entity, DbAction dbAction)
        {
            if (entity == null)
            {
                entity = new Member();
            }

            entity.PersonId = memberDto.PersonId;
            entity.Level = memberDto.Level;
            entity.StartDate = memberDto.StartDate.Date;
            entity.EndDate = memberDto.EndDate.HasValue ? memberDto.EndDate.Value.Date : null;

            entity.IsAlsoTrainer = memberDto.IsAlsoTrainer;
            RepositoryHelper.SetAuditFields(entity, dbAction);

            return entity;
        }

        public List<MemberDto> GetList()
        {

            var qryDb = _dbContext.Members.Where(mbr => !mbr.IsDeleted);
            var result = ToDto(qryDb);
            return result.ToList();
        }

        public MemberDto Get(int id)
        {

            var qryDb = _dbContext.Members.Where(mbr => mbr.Id == id);
            var result = ToDto(qryDb).SingleOrDefault();
            return result;
        }

        public int Add(MemberDto member)
        {
            var itemDb = ToDbo(member, null, DbAction.Add);
            _dbContext.Members.Add(itemDb);
            _dbContext.SaveChanges();
            return itemDb.Id;
        }

        public void Delete(int id)
        {
            var member = _dbContext.Members.Find(id);
            member.IsDeleted = true;
            _dbContext.Entry(member).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Update(MemberDto member)
        {
            var entity = _dbContext.Members.Find(member.Id);
            var itemDb = ToDbo(member, entity, DbAction.Update);
            _dbContext.Entry(itemDb).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Recover()
        {
            _dbContext.Database.ExecuteSqlRaw($"update members set isDeleted = 0");
        }
    }
}
