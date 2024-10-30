using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project_NZWalks.API.CustomActionFilters;
using Project_NZWalks.API.Models.Domain;
using Project_NZWalks.API.Models.DTO;
using Project_NZWalks.API.Repositories;

namespace Project_NZWalks.API.Controllers
{
    // https://localhost:7192/api/Regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController(
        IRegionRepository regionRepository,
        IMapper mapper)
        : ControllerBase
    {
        //POST to Create a new region
        //POST: https://localhost:7192/api/Regions
        [HttpPost]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {

            //Convert DTO to Domain model
            var regionDomain = mapper.Map<Region>(addRegionRequestDto);

            //Use a Domain model to create Region
            regionDomain = await regionRepository.CreateAsync(regionDomain);

            //Map Domain model Back to Dto
            var regionDto = mapper.Map<RegionDto>(regionDomain);

            //Return Dto
            return CreatedAtAction(nameof(GetById), new { regionDto.Id }, regionDto);

        }

        //GET ALL REGIONS
        //GET: https://localhost:7192/api/Regions
        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            //Get Data From Database as Domain models
            var regionsDomain = await regionRepository.GetAllAsync();

            //Map Domain Models to DTOs
            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

            //Return DTOs
            return Ok(regionsDto);
        }

        //GET SINGLE REGION (Get Region By ID)
        //GET: https://localhost:7192/api/Regions/ID
        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //Check if Domain Exist
            var regionDomain = await regionRepository.GetByIDAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }

            //Map Domain Model to Dto
            var regionDto = mapper.Map<RegionDto>(regionDomain);

            //Return Dto
            return Ok(regionDto);
        }

        //Update Region
        //PUT: https://localhost:7192/api/Regions/ID
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {

            //Map to Domain Model
            var regionDomain = mapper.Map<Region>(updateRegionRequestDto);

            //Check if Region Exist & Update Region
            var updatedRegionDomain = await regionRepository.UpdateAsync(id, regionDomain);
            if (updatedRegionDomain == null)
            {
                return NotFound();
            }

            //Map Domain model Back to Dto
            var updatedRegionDto = mapper.Map<RegionDto>(updatedRegionDomain);

            //Return Dto
            return Ok(updatedRegionDto);

        }

        //Delete Region
        //DELETE: https://localhost:7192/api/Regions/ID
        [HttpDelete]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //Check if Dto Exist & Use Domain model to Delete Region
            var deletedRegionDomain = await regionRepository.DeleteAsync(id);
            if (deletedRegionDomain == null)
            {
                return NotFound();
            }

            //Map Domain model Back to Dto
            var deletedRegionDto = mapper.Map<RegionDto>(deletedRegionDomain);

            //Return Dto
            return Ok(deletedRegionDto);
        }
    }
}
