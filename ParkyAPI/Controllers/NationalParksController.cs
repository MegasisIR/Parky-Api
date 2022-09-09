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
    [ApiExplorerSettings(GroupName = "ParkyApiNP")]
    public class NationalParksController : Controller

    {
        private INationalParkRepository _nationalRepository;
        private readonly IMapper _mapper;

        public NationalParksController(INationalParkRepository nationalRepository, IMapper mapper)
        {
            _nationalRepository=nationalRepository;
            _mapper=mapper;
        }

        [HttpPost]
        public IActionResult AddNationalPark([FromBody] NationalParkCommand command)
        {
            var isExistsPark = _nationalRepository.NationalParkExists(command.Name);
            if (isExistsPark) return StatusCode(409, new { Message = $"National Park {command.Name} Exists ! " });
            var nationalPark = new NationalPark(AddNationalParkId(_nationalRepository), command.Name, command.State);
            _nationalRepository.CreateNationalPark(nationalPark);
            return CreatedAtRoute("GetNationalPark", new { NationalParkId = nationalPark.Id }, new { Message = $"National Park {nationalPark.Name} with Id = {nationalPark.Id} Added" });
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
        /// <summary>
        /// Remove individual national park
        /// </summary>
        /// <param name="nationalParkId">The id of national park</param>
        /// <returns>National park</returns>
        [HttpDelete("{nationalParkId:int}")]

        [ProducesResponseType(typeof(NationalParkDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteNationalPark(int nationalParkId)
        {
            var nationalPark = _nationalRepository.GetNationalPark(nationalParkId);

            if (nationalPark is null) return StatusCode(204, new
            {
                Message = "پارک مورد نظر یافت نشد",
            });
            _nationalRepository.DeleteNationalPark(nationalPark);
            return Ok(new
            {
                Message = "پارک با موفقیت حذف شد",
                Content = _mapper.Map<NationalParkDto>(nationalPark)
            });
        }
        [HttpPatch("{nationalParkId:int}")]
        public IActionResult UpdateNationalPark(int nationalParkId, NationalParkCommand command)
        {
            var nationalPark = _nationalRepository.GetNationalPark(nationalParkId);

            if (nationalPark is null) return StatusCode(204, new
            {
                Message = "پارک مورد نظر یافت نشد",
            });
            nationalPark.UpdateInfo(command.Name, command.State);
            _nationalRepository.UpdateNationalPark(nationalPark);
            return Ok(new
            {
                Message = "پارک با موفقیت روز شد",
                Content = _mapper.Map<NationalParkDto>(nationalPark)
            });
        }


        #region Private Methods
        private int AddNationalParkId(INationalParkRepository nationalRepository, int id = 1)
        {
            if (nationalRepository.NationalParkExists(id)) return AddNationalParkId(nationalRepository, id+1);
            return id;
        }
        #endregion
    }
}
