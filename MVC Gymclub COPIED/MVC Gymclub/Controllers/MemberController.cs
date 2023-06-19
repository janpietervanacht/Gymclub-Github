using Framework.Errors;
using LogicMember.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model.AutoMapper;
using MVC_Gymclub.ViewModels.Member;
using System.Security.Claims;
using MemberDto = Framework.DtoModels.Member;
using MemberModel = Model.Entities.Member;

namespace MVC_Gymclub.Controllers
{
    public class MemberController : Controller
    {
        private readonly ILogger<MemberController> _logger;
        private readonly IMemberService _memberService;


        public MemberController(ILogger<MemberController> logger, IMemberService memberService)
        {
            _logger = logger;
            _memberService = memberService;
        }

        public IActionResult Index()
        {
            var membersDto = _memberService.GetList();

            var membersModel = MapperHelper.MapList<MemberDto, MemberModel>(membersDto);

            var result = new MemberListVM
            {
                NrOfMembers = membersModel.Count(),
                Members = membersModel.Select(mbr => new MemberVM
                {
                    FullName = mbr.Person.FirstName + " " + mbr.Person.MiddleName + " " + mbr.Person.LastName,
                    Member = mbr
                }
                ).ToList()
            };

            return View(result);
        }

        public IActionResult Delete(int id)
        {
            _memberService.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var memberDto = _memberService.Get(id);
            var memberModel = MapperHelper.Map<MemberDto, MemberModel>(memberDto);
            var result = new MemberVM
            {
                FullName = memberModel.Person.FirstName + " " + memberModel.Person.MiddleName + " " + memberModel.Person.LastName,
                Member = memberModel
            };
            return View(result);
        }

        public IActionResult Recover()
        {
            _memberService.Recover();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MemberVM memberVM)
        {
            var memberDto = MapperHelper.Map<MemberModel, MemberDto>(memberVM.Member);

            var errMsg = Validate(memberDto);
            if (errMsg != null)
            {
                ModelState.AddModelError(string.Empty, errMsg);
                return View(memberVM);
            }

            _memberService.Update(memberDto);
            return RedirectToAction("Index");
        }

        private string? Validate(MemberDto memberDto)
        {
            if (memberDto.EndDate.HasValue && DateChecks.StartDateGTEndDate(memberDto.StartDate, memberDto.EndDate.Value))
            {
                return "Startdatum is groter dan einddatum" ;
            }
            return null;
        }
    }
}