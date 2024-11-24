using Microsoft.AspNetCore.Mvc;
using vlm721api.Models;
using vlm721api.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace vlm721api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _repository;
        public ItemsController(IItemRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<ItemsController>
        [HttpGet]
        public ActionResult<List<Item>> Get()
        {
            var response = _repository.GetAll();
            if (response.Count == 0)
            {
                return NotFound();
            }
            return Ok(response);

        }

        // GET api/<ItemsController>/5
        [HttpGet("{id}")]
        public ActionResult<Item> Get(int id)
        {
            var response = _repository.GetById(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);

        }

        // POST api/<ItemsController>
        [HttpPost]
        public ActionResult Post([FromBody] DTOItem item)
        {

            _repository.Add(item);
            return Created();

        }

        // PUT api/<ItemsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] DTOItem item)
        {

            if (_repository.GetById(id) == null)
            {
                return NotFound();
            }
            _repository.Update(item, id);
            return Ok();


        }

        // DELETE api/<ItemsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existingItem = _repository.GetById(id);
            if (existingItem == null)
            {
                return NotFound();
            }
            _repository.Delete(id);
            return Ok();
        }
    }
}
