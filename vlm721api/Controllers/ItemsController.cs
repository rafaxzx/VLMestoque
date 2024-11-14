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
        public List<Item> Get()
        {
            return _repository.GetAll();
        }

        // GET api/<ItemsController>/5
        [HttpGet("{id}")]
        public Item Get(int id)
        {
            return _repository.GetById(id);
        }

        // POST api/<ItemsController>
        [HttpPost]
        public void Post([FromBody] DTOItem value)
        {
            _repository.Add(value);
        }

        // PUT api/<ItemsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ItemsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
