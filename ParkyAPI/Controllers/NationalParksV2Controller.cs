using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Models.Dtos;
using ParkyAPI.Repository.IRepository;

namespace ParkyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NationalParksV2Controller : Controller

    {
        private INationalParkRepository _nationalRepository;
        private readonly IMapper _mapper;

        public NationalParksV2Controller(INationalParkRepository nationalRepository, IMapper mapper)
        {
            _nationalRepository=nationalRepository;
            _mapper=mapper;
        }


        /// <summary>
        /// Get all national parks
        /// </summary>
        /// <returns> List of national park</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<NationalParkDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllNationalParks()
        {
            var nationalPasrks = _nationalRepository.GetNationalParks();
            var nationalParksDto = nationalPasrks.Select(x => _mapper.Map<NationalParkDto>(x));
            return nationalParksDto.Count()==0 ? Ok(null) : Ok(nationalPasrks);
        }
        /// <summary>
        /// Get individual national park
        /// </summary>
        /// <param name="nationalParkId">The id of national park</param>
        /// <returns>NationalParkDto</returns>
        [HttpGet("{nationalParkId:int}")]
        [ProducesResponseType(typeof(NationalParkDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetNationalPark(int nationalParkId)
        {
            var nationalPark = _nationalRepository.GetNationalPark(nationalParkId);

            return nationalPark is null ? Ok(null) : Ok(_mapper.Map<NationalParkDto>(nationalPark));
        }
    }
}
