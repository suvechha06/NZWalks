using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Mappings;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NZWalksDbContext dbContext, 
            IRegionRepository regionRepository, 
            IMapper mapper,
            ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;      
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            logger.LogInformation("GetAll method invoked");

            // GET data from db - domain model
            var regions = await regionRepository.GetAllAsync();

            logger.LogInformation($"All data retrieved: {JsonSerializer.Serialize(regions)}");

            //map domain model to DTO
            //var regionsDTO = new List<RegionDTO>();
            //foreach(var region in regions)
            //{
            //    regionsDTO.Add(new RegionDTO()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Code = region.Code,
            //        RegionImageUrl = region.RegionImageUrl,
            //    });
            //}
            //map domain model to DTO
            var regionsDTO = mapper.Map<List<RegionDTO>>(regions);

            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id);
            var region = await regionRepository.GetByIdAsync(id);
            if(region == null)
            {
                return NotFound();
            }

            //var regionDTO = new RegionDTO()
            //{
            //    Id = region.Id,
            //    Name = region.Name,
            //    Code = region.Code,
            //    RegionImageUrl = region.RegionImageUrl,
            //};

            //map domain model to DTO
            var regionDTO = mapper.Map<RegionDTO>(region);

            return Ok(regionDTO);
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionDTO addRegionDTO)
        {
                //Converting DTO to domain model
                //var regionDomainModel = new Region
                //{
                //    Code = addRegionDTO.Code,
                //    Name = addRegionDTO.Name,
                //    RegionImageUrl = addRegionDTO.RegionImageUrl,
                //};

                //Mapping DTO to domain model
                var regionDomainModel = mapper.Map<Region>(addRegionDTO);

                //create Region from domain model
                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

                //map domain model back to DTO
                //var regionDTO = new RegionDTO
                //{
                //    Id = regionDomainModel.Id,
                //    Code = regionDomainModel.Code,
                //    Name = regionDomainModel.Name,
                //    RegionImageUrl = regionDomainModel.RegionImageUrl,
                //};

                //map domain model back to DTO
                var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = regionDTO.Id }, regionDTO);            
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDTO updateRegionDTO)
        {
                //Map DTO to domain model
                //var regionDomainModel = new Region
                //{
                //    Code = updateRegionDTO.Code,
                //    Name = updateRegionDTO.Name,
                //    RegionImageUrl = updateRegionDTO.RegionImageUrl,
                //};

                //Map DTO to domain model
                var regionDomainModel = mapper.Map<Region>(updateRegionDTO);

                //Check if region exists
                regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                //convert domain model to DTO
                //var regionDTO = new RegionDTO
                //{
                //    Id = regionDomainModel.Id,
                //    Code = regionDomainModel.Code,
                //    Name = regionDomainModel.Name,
                //    RegionImageUrl = regionDomainModel.RegionImageUrl,
                //};

                //convert domain model to DTO
                var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);

                return Ok(regionDTO);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if(regionDomainModel == null)
            {
                return NotFound();
            }

            //convert domain model to DTO
            //var regionDTO = new RegionDTO
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl,
            //};

            //convert domain model to DTO
            var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);

            return Ok(regionDTO);
        }
    }
}
