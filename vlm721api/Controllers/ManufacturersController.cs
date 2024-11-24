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
        public ActionResult<List<Manufacturer>> Get()
        {
            var manufacturers = _repository.GetAll();
            return Ok(manufacturers);
        }

        // GET api/<ManufacturersController>/5
        [HttpGet("{id}")]
        public ActionResult<Manufacturer> Get(int id)
        {
            var manufacturer = _repository.GetById(id);
            if (manufacturer == null)
            {
                return NotFound();
            }
            return Ok(manufacturer);
        }

        // POST api/<ManufacturersController>
        [HttpPost]
        public ActionResult Post([FromBody] Manufacturer manufacturer)
        {
            _repository.Add(manufacturer);
            //return CreatedAtAction(nameof(Get), new { id = manufacturer.Id }, manufacturer);
            return Created();
        }

        // PUT api/<ManufacturersController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Manufacturer manufacturer)
        {
            if (_repository.GetById(id) == null)
            {
                return NotFound();
            }
            _repository.Update(manufacturer, id);
            return Ok();
        }

        // DELETE api/<ManufacturersController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existingManufacturer = _repository.GetById(id);
            if (existingManufacturer == null)
            {
                return NotFound();
            }
            _repository.Delete(id);
            return Ok();
        }
    }
}
