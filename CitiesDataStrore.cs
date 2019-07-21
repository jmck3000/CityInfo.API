using CityInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API
{
    public class CitiesDataStrore
    {
        public static CitiesDataStrore Current { get; } = new CitiesDataStrore();
        public List<CityDetail> Cities { get; set; }

        public CitiesDataStrore()
        {
            Cities = new List<CityDetail>()
            {
                new CityDetail()
                {
                    Id = 1,
                    Name = "New York City",
                    Description = "The one with the big park.",
                    PointsOfInterest = new List<PointOfInterestDetail>()
                    {
                        new PointOfInterestDetail()
                        {
                            Id = 1,
                            Name = "Central Park",
                            Description = "The most visited urban park in the United States."
                        },
                        new PointOfInterestDetail()
                        {
                            Id = 2,
                            Name = "Empire State Building",
                            Description = "A 102-story skyscraper located in Midtown Manhattan."
                        }
                    }
                    
                },
                 new CityDetail()
                {
                    Id = 2,
                    Name = "Antwerp",
                    Description = "The one with the cathedral that was never really finished.",
                    PointsOfInterest = new List<PointOfInterestDetail>()
                    {
                        new PointOfInterestDetail()
                        {
                            Id = 3,
                            Name = "Cathedral of Our Lady",
                            Description = "A Gothic style cathedral, conceived by architects Jan and Pieter Appelmans."
                        },
                        new PointOfInterestDetail()
                        {
                            Id = 4,
                            Name = "Antwerp Central Station",
                            Description = "The finest example of railway architecture in Belgium."
                        }
                    }
                },
                  new CityDetail()
                {
                    Id = 3,
                    Name = "Paris",
                    Description = "The one with the big tower.",
                     PointsOfInterest = new List<PointOfInterestDetail>()
                    {
                        new PointOfInterestDetail()
                        {
                            Id = 5,
                            Name = "Eiffel Tower",
                            Description = "A wrought iron lattice tower on the Champ de Mars, named after the enginner Gustave Eiffel."
                        },
                        new PointOfInterestDetail()
                        {
                            Id = 6,
                            Name = "The Louvre",
                            Description = "The world's largest museum."
                        }
                    }
                }
            };
        }

    }
}
