using Microsoft.AspNetCore.Mvc;
using vlm721api.Models;
using vlm721api.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace vlm721api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly IManufacturerRepository _repository;

        public ManufacturersController(IManufacturerRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<ManufacturersController>
        [HttpGet]
        public List<Manufacturer> Get()
        {
            return _repository.GetAll();
        }

        // GET api/<ManufacturersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ManufacturersController>
        [HttpPost]
        public void Post([FromBody] Manufacturer value)
        {
            _repository.Add(value);
        }

        // PUT api/<ManufacturersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ManufacturersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
