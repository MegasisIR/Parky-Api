using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Models;
using ParkyAPI.Models.Commands;
using ParkyAPI.Models.Dtos;
using ParkyAPI.Repository.IRepository;

namespace ParkyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class TrailsController : Controller

    {
        private ITrailRepository _trailRepository;
        private INationalParkRepository _nationalParkRepository;
        private readonly IMapper _mapper;

        public TrailsController(ITrailRepository trailRepository, IMapper mapper, INationalParkRepository nationalParkRepository)
        {
            _trailRepository=trailRepository;
            _mapper=mapper;
            _nationalParkRepository=nationalParkRepository;
        }

        [HttpPost]
        public IActionResult AddTrail([FromBody] TrailCommand command)
        {
            var isExistsTrail = _trailRepository.TrailExists(command.Name);
            var nationalPark = _nationalParkRepository.GetNationalPark(command.NationalParkId);
            if (nationalPark is null|| isExistsTrail) return StatusCode(409, new { Message = $"Trail {command.Name} Exists ! " });
            var trail = new Trail(AddTrailId(_trailRepository), command.Name, command.Distance,command.Difficulty, nationalPark);
            _trailRepository.CreateTrail(trail);
            return CreatedAtRoute("GetTrail", new { TrailId = trail.Id }, new { Message = $"Trail {trail.Name} with Id = {trail.Id} Added" });
        }

        /// <summary>
        /// Get all trails
        /// </summary>
        /// <returns> List of trails</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<TrailDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetAllTrails()
        {
            var nationalPasrks = _trailRepository.GetTrails();
            var nationalParksDto = nationalPasrks.Select(x => _mapper.Map<TrailDto>(x));
            return nationalParksDto.Count()==0 ? Ok(null) : Ok(nationalPasrks);
        }
        /// <summary>
        /// Get individual trail
        /// </summary>
        /// <param name="trailId">The id of trail</param>
        /// <returns>TrailDto</returns>
        [HttpGet("{trailId:int}")]
        [ProducesResponseType(typeof(TrailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public IActionResult GetTrail(int trailId)
        {
            var trail = _trailRepository.GetTrailById(trailId);

            return trail is null ? Ok(null) : Ok(_mapper.Map<TrailDto>(trail));
        }
        /// <summary>
        /// Remove individual trail
        /// </summary>
        /// <param name="trailId">The id of trail</param>
        /// <returns>trail</returns>
        [HttpDelete("{trailId:int}")]

        [ProducesResponseType(typeof(TrailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteTrail(int trailId)
        {
            var nationalPark = _trailRepository.GetTrailById(trailId);

            if (nationalPark is null) return StatusCode(204, new
            {
                Message = "پارک مورد نظر یافت نشد",
            });
            _trailRepository.DeleteTrail(nationalPark);
            return Ok(new
            {
                Message = "پارک با موفقیت حذف شد",
                Content = _mapper.Map<TrailDto>(nationalPark)
            });
        }
        [HttpPatch("{trailId:int}")]
        public IActionResult UpdateTrail(int trailId, TrailCommand command)
        {
            var nationalPark = _trailRepository.GetTrailById(trailId);

            if (nationalPark is null) return StatusCode(204, new
            {
                Message = "پارک مورد نظر یافت نشد",
            });
            nationalPark.UpdateInfo(command.Name, command.Distance,command.Difficulty);
            _trailRepository.UpdateTrail(nationalPark);
            return Ok(new
            {
                Message = "پارک با موفقیت روز شد",
                Content = _mapper.Map<TrailDto>(nationalPark)
            });
        }


        #region Private Methods
        private int AddTrailId(ITrailRepository trailRepository, int id = 1)
        {
            if (trailRepository.TrailExists(id)) return AddTrailId(trailRepository, id+1);
            return id;
        }
        #endregion
    }
}
