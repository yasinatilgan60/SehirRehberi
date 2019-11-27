using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SehirRehberi_API.Data;
using SehirRehberi_API.Dtos;
using SehirRehberi_API.Models;

namespace SehirRehberi_API.Controllers
{
    [Produces("application/json")]
    [Route("api/Cities")]
    public class CitiesController : Controller
    {
        private IAppRepository _appRepository;
        private IMapper _mapper;
        public CitiesController(IAppRepository appRepository, IMapper mapper)
        {
            _appRepository = appRepository; // Injection
            _mapper = mapper;
        }
        public ActionResult GetCities()
        {
            //var cities = _appRepository.GetCities().Select(c => new CityForListDto { Description = c.Description, Name = c.Name, Id = c.Id, PhotoUrl = c.Photos.FirstOrDefault(p => p.IsMain == true).Url }).ToList();
            // Uzun yazmak yerine auto mapper ile..
            var cities = _appRepository.GetCities();
            var citiesToReturn = _mapper.Map<List<CityForListDto>>(cities); // cities'i cityforlist listesine map et.
            return Ok(citiesToReturn);
        }
        [HttpPost]
        [Route("add")]
        public ActionResult Add([FromBody] City city)
        {
            _appRepository.Add(city);
            _appRepository.SaveAll();
            return Ok(city); // Eklenen şehir geri döndürülür.
        }
        [HttpGet]
        [Route("detail")]
        public ActionResult GetCityById(int id)
        {
            var city = _appRepository.GetCityById(id);
            var cityToReturn = _mapper.Map<CityForDetailDto>(city);
            return Ok(cityToReturn);
        }
        [HttpGet]
        [Route("photos")]
        public ActionResult GetPhotosByCity(int cityId)
        {
            var photos = _appRepository.GetPhotosByCity(cityId);
            return Ok(photos);
        }
    }
}