using DataAccess.Repositories.Interfaces;
using Framework.DtoModels;
using Framework.Errors;
using LogicMember.Services.Interfaces;

namespace LogicMember.Services.Implementations
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IPersonRepository _personRepository;
        private readonly ITrainerRepository _trainerRepository;

        public MemberService(IMemberRepository memberRepository, IPersonRepository personRepository, ITrainerRepository trainerRepository)
        {
            _memberRepository = memberRepository;
            _personRepository = personRepository;
            _trainerRepository = trainerRepository;
        }

        public List<Member> GetList()
        {
            Thread.Sleep(2000); // om front end te testen
            return _memberRepository.GetList();
        }

        public Member Get(int id)
        {
            Thread.Sleep(2000); // om front end te testen
            return _memberRepository.Get(id);
        }

        public ResultMessage Add(Member member)
        {
            Thread.Sleep(2000); // om front end te testen
            var resultMessage = Validate(member);
            if (!resultMessage.Ok)
            {
                return resultMessage;
            }

            var personId = _personRepository.Add(member.Person);
            member.PersonId = personId;
            var _ = _memberRepository.Add(member);

            if (member.IsAlsoTrainer)
            {
                var trainer = new Trainer
                {
                    PersonId = personId,
                    IsCertified = false,
                };
                _trainerRepository.Add(trainer);
            }
            return resultMessage;
        }

        public ResultMessage Update(Member member)
        {
            Thread.Sleep(2000); // om front end te testen
            var resultMessage = Validate(member);
            if (member.Person.MiddleName == null)
            {
                member.Person.MiddleName = string.Empty;
            }
            if (!resultMessage.Ok)
            {
                return resultMessage;
            }

            _memberRepository.Update(member);
            member.Person.Id = member.PersonId;
            _personRepository.Update(member.Person);

            if (!member.IsAlsoTrainer)
            {
                // als trainer bestaat -> verwijder de trainer
                var trainer = _trainerRepository.GetTrainerByPersonId(member.PersonId);
                if (trainer != null)
                {
                    _trainerRepository.Delete(trainer.Id);
                }
            }
            else
            {
                var trainer = _trainerRepository.GetTrainerByPersonId(member.PersonId);
                if (trainer == null)
                {
                    trainer = new Trainer
                    {
                        PersonId = member.PersonId,
                        IsCertified = false,
                    };
                    _trainerRepository.Add(trainer);
                }
            }
            return resultMessage;
        }

        public void Delete(int id)
        {
            Thread.Sleep(2000); // om front end te testen
            _memberRepository.Delete(id);
        }

        public void Recover()
        {
            Thread.Sleep(2000); // om front end te testen
            _memberRepository.Recover();
        }

        private ResultMessage Validate(Member member)
        {
            var result = new ResultMessage();
            var dateLowerLimit = new DateTime(2000, 1, 1).Date;
            if (member.StartDate.Date <= dateLowerLimit.Date)
            {
                result.Ok = false;
                result.ErrorText = "Back end says: Begindatum kleiner dan 2000";
                return result;
            }

            if (member.EndDate.HasValue && member.EndDate.Value.Date <= dateLowerLimit.Date)
            {
                result.Ok = false;
                result.ErrorText = "Back end says: Einddatum kleiner dan 2000";
                return result;
            }

            var forbiddenName = (member.Person.FirstName + member.Person.MiddleName + member.Person.LastName).NameForbidden();
            if (forbiddenName != string.Empty)
            {
                result.Ok = false;
                result.ErrorText = $"Back end says: De naam \'{forbiddenName}\' is niet toegestaan";
                return result;
            }

            result.Ok = true;
            result.ErrorText = "";
            return result;
        }
    }

    internal static class Helper
    {
        public static string NameForbidden(this string text)  // extension method
        {
            var dictatorList = new List<string>() { "Hitler", "Hugo", "Sigrid", "Kaag", "Mark", "Rutte", "Paternotte" };
            var dictator = dictatorList.FirstOrDefault(dic => text.ToLower().Contains(dic.ToLower()));
            if (!string.IsNullOrEmpty(dictator))
            {
                return dictator;
            }
            return string.Empty;
        }
    }
}
