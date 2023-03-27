﻿using MemberDto = Framework.DtoModels.Member;

namespace DataAccess.Repositories.Interfaces
{
    public interface IMemberRepository
    {
        List<MemberDto> GetList();
        MemberDto Get(int id);
        int Add(MemberDto member);
        void Update(MemberDto member);
        void Delete(int id);
        void Recover();
    }
}
