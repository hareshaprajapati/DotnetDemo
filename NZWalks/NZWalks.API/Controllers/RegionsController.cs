using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
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

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepository.GetAsync(id);
            if(region == null)
            {
                return NotFound();
            }
            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);

        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(AddRegionRequest addRegionRequest)
        {
            var region = new Models.Domain.Region()
            {
                Area = addRegionRequest.Area,
                Code = addRegionRequest.Code,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population,

            };
            region = await regionRepository.AddAsync(region);

            var regionDTO = mapper.Map<Models.DTO.Region>(region);
        // 201
        // in response header the Client can see
        // location: https://localhost:7259/Regions/a7c6c13e-0daf-432d-8df1-fb33bbe86e71 
            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.RegionId },
                regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            // get the region from db
            var region = await regionRepository.DeleteAsync(id);
            // if null return notfound
            if(region == null) { 
                return NotFound();
            }

            // convert response back to DTO
            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            // return Ok Response
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            // convert to domain region
            var region = new Models.Domain.Region()
            {
                Area = updateRegionRequest.Area,
                Code = updateRegionRequest.Code,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Name = updateRegionRequest.Name,
                Population = updateRegionRequest.Population,

            };
            // try to update it
            var updatedRegion = await regionRepository.UpdateAsync(id, region);
            // if not found 
            if(updatedRegion == null)
            {
                return NotFound();
            }
            // convert back to dto
            var updatedRegionDTO = mapper.Map<Models.DTO.Region>(updatedRegion);
            // return success
            return Ok(updatedRegionDTO);

        }

        
    }

    
}
