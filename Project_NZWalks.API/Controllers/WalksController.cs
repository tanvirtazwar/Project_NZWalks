using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project_NZWalks.API.CustomActionFilters;
using Project_NZWalks.API.Models.Domain;
using Project_NZWalks.API.Models.DTO;
using Project_NZWalks.API.Querying;
using Project_NZWalks.API.Repositories;

namespace Project_NZWalks.API.Controllers;

// /api/walks
[Route("api/[controller]")]
[ApiController]

public class WalksController(
    IWalkRepository walkRepository,
    IMapper mapper) : ControllerBase
{
    //Create Walks
    //Post: https://localhost:7192/api/Walks
    [HttpPost]
    [ValidateModel]
    public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
    {
        // Convert DTO to Domain model
        var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);
        //Use a Domain model to create Walk
        await walkRepository.CreateAsync(walkDomainModel);

        //Map Domain model Back to Dto
        var walkDto = mapper.Map<WalkDto>(walkDomainModel);

        //Return Dto
        return CreatedAtAction(nameof(GetById), new { walkDto.Id }, walkDto);
    }

    //Get All Walks
    //Get: https://localhost:7192/api/Walks?filterOn_Or_sort_Or_pagination
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] QueryWalks query)
    {
        //Get Data From Database as Domain models
        var walksDomainModel = await walkRepository.GetAllAsync(query);

        //Map Domain Models to DTOs
        var walksDto = mapper.Map<List<WalkDto>>(walksDomainModel);

        //Return DTOs
        return Ok(walksDto);
    }


    //Get Walk by ID
    //Get: https://localhost:7192/api/Walks/ID
    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        //Check if Domain Exist
        var walkDomainModel = await walkRepository.GetByIDAsync(id);
        if (walkDomainModel == null)
        {
            return NotFound();
        }

        //Convert Domain Model to Dto
        var walkDto = mapper.Map<WalkDto>(walkDomainModel);

        //Return Dto
        return Ok(walkDto);
    }

    //Update Walk by ID
    //Put: https://localhost:7192/api/Walks/ID
    [HttpPut]
    [Route("{id:Guid}")]
    [ValidateModel]
    public async Task<IActionResult> Update([FromRoute] Guid id,
        [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
    {

        //Convert Dto to Domain Model
        var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

        //Update Walk & Return if Walk Doesn't Exist
        var updatedWalkDomain = await walkRepository.UpdateAsync(id, walkDomainModel);
        if (updatedWalkDomain == null)
        {
            return NotFound();
        }

        //Map Domain Model Back To Dto
        var updatedWalkDto = mapper.Map<WalkDto>(updatedWalkDomain);

        //Return Dto
        return Ok(updatedWalkDto);
    }

    //Delete Walk by ID
    //Delete: https://localhost:7192/api/Walks/ID
    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        //Delete Walk & Return if Walk Doesn't Exist
        var deletedWalkDomain = await walkRepository.DeleteAsync(id);
        if (deletedWalkDomain == null)
        {
            return NotFound();
        }

        //Map Domain Model Back to Dto
        var deletedWalkDto = mapper.Map<WalkDto>(deletedWalkDomain);

        //Return Dto
        return Ok(deletedWalkDto);
    }

}
