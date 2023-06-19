using AutoMapper;
using MemberDto = Framework.DtoModels.Member;
using MemberModel = Model.Entities.Member;
using PersonDto = Framework.DtoModels.Person;
using PersonModel = Model.Entities.Person;

namespace Model.AutoMapper
{
    public static class MapperHelper
    {
        private static Mapper _mapper;

        public static void InitMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MemberDto, MemberModel>();
                cfg.CreateMap<MemberModel, MemberDto>();
                cfg.CreateMap<PersonDto, PersonModel>();
                cfg.CreateMap<PersonModel, PersonDto>();

            });

            _mapper = new Mapper(config);
        }

        public static TDestination Map<TSource, TDestination>(TSource source)
        {
            var result = _mapper.Map<TSource, TDestination>(source);
            return result;
        }

        public static List<TDestination> MapList<TSource, TDestination>(List<TSource> sourceList)
        {
            var result = _mapper.Map<List<TSource>, List<TDestination>>(sourceList);
            return result;
        }

    }
}
