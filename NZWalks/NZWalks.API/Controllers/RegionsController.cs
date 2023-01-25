using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            /*var regions = new List<Region>()
            {

                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Adelaide",
                    Code = "ADL",
                    Area = 2222,
                    Lat = -1.8878,
                    Long = 299.88,
                    Population= 108980,
                },
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Melbourne",
                    Code = "MEL",
                    Area = 2222,
                    Lat = -1.8878,
                    Long = 299.88,
                    Population= 108980,
                }
            };*/
            var regions = await regionRepository.GetAllAsync();
            /*var regionsDTO = new List<Models.DTO.Region>();
            regions.ToList().ForEach(domainRegion =>
            {
                var dtoRegion = new Models.DTO.Region
                {
                    Area = domainRegion.Area,
                    Code = domainRegion.Code,
                    RegionId = domainRegion.Id,
                    Lat = domainRegion.Lat,
                    Long = domainRegion.Long,
                    Name = domainRegion.Name,
                    Population = domainRegion.Population,
                };
                regionsDTO.Add(dtoRegion);
            });*/

            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);

            return Ok(regionsDTO);
        }
    }
}
