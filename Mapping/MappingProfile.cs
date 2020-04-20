using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // CreateMap<Project, ProjectCreate>();
            CreateMap<Entities.City, Models.CityWithoutPointsOfInterestDetail>();
            CreateMap<Entities.City, Models.CityDetail>();
            CreateMap<Entities.PointOfInterest, Models.PointOfInterestDetail>();
            CreateMap<Models.PointOfInterestForCreationDetail, Entities.PointOfInterest>();
            CreateMap<Models.PointOfInterestForUpdateDetail, Entities.PointOfInterest>();
            CreateMap<Entities.PointOfInterest, Models.PointOfInterestForUpdateDetail>();
        }

    }
}
