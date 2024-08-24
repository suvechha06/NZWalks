using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;
        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkDTO addWalkDTO)
        {
            //Map DTO to Domain Model
            var walkDomainModel = mapper.Map<Walk>(addWalkDTO);

            await walkRepository.CreateAsync(walkDomainModel);

            //Map Domain Model to DTO
            var walkDTO = mapper.Map<WalkDTO>(walkDomainModel);
            return Ok(walkDTO);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksDomainModel = await walkRepository.GetAllAsync();

            //Map Domain Model to DTO
            var walksDTO = mapper.Map<List<WalkDTO>>(walksDomainModel);
            return Ok(walksDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);

            if(walkDomainModel == null)
            {
                return NotFound();
            }

            //Map Domain Model to DTO
            var walkDTO = mapper.Map<WalkDTO>(walkDomainModel);
            return Ok(walkDTO);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkDTO updateWalkDTO)
        {
            //Map DTO to Domain Model
            var walkDomainModel = mapper.Map<Walk>(updateWalkDTO);

            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

            if(walkDomainModel == null)
            {
                return NotFound();
            }

            //Map Domain Model to DTO
            var walkDTO = mapper.Map<WalkDTO>(walkDomainModel);
            return Ok(walkDTO);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalkDomain = await walkRepository.DeleteAsync(id);

            if(deletedWalkDomain == null)
            {
                return NotFound();
            }

            //Map Domain Model to DTO
            var deletedWalkDTO = mapper.Map<WalkDTO>(deletedWalkDomain);
            return Ok(deletedWalkDTO);
        }
    }
}
