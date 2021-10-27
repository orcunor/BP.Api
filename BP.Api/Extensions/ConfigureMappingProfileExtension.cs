using AutoMapper;
using BP.Api.Data.Models;
using BP.Api.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BP.Api.Extensions
{
    public static class ConfigureMappingProfileExtension
    {
        public static IServiceCollection ConfigureMapping(this IServiceCollection service) // startup da çağır
        {
            var mappingConfig = new MapperConfiguration(i => i.AddProfile(new AutoMapperMappigProfile()));

            IMapper mapper = mappingConfig.CreateMapper();

            service.AddSingleton(mapper);

            return service;
        }
    }

    public class AutoMapperMappigProfile : Profile   // Mapping işlemi
    {
        public AutoMapperMappigProfile()
        {

            //var config = new MapperConfiguration(cfg => {
            //    cfg.CreateMap<Contact, ContactDTO>()
            //        //OrderId is different so map them using For Member
            //        .ForMember(dest => dest.FullName, act => act.MapFrom(src => src.FirstName + " "+ src.LastName))
            //        //Customer is a Complex type, so Map Customer to Simple type using For Member

            //        .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            //        .ReverseMap();
            //});

            CreateMap<Contact, ContactDTO>()
                .ForMember(x => x.FullName, y => y.MapFrom(z => z.FirstName + " " + z.LastName))
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ReverseMap()
                ;

        }
    }
}
