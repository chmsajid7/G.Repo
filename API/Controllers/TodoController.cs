using Domain.Entities;
using Domain.Seedwork;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // fluent validations////////////////////////////////////////////////////////////////////
    public interface IProductRepository : IBaseRepository<TodoModel>
    {
    }

    public class ProductRepository : BaseRepository<TodoModel>, IProductRepository
    {
        public ProductRepository(IMongoDbContext context) : base(context)
        {
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _uow;

        public TodoController(IProductRepository productRepository, IUnitOfWork uow)
        {
            _uow = uow;
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoModel>>> Get()
        {
            var products = await _productRepository.GetAll();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoModel>> Get(Guid id)
        {
            var product = await _productRepository.GetById(id);

            return Ok(product);
        }

        [HttpPost, Route("PostSimulatingError")]
        public IActionResult PostSimulatingError([FromBody] TodoViewModel value)
        {
            var product = new TodoModel(value.Description);
            _productRepository.Add(product);

            // The product will not be added
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<TodoModel>> Post([FromBody] TodoViewModel value)
        {
            var product = new TodoModel(value.Description);
            _productRepository.Add(product);

            // it will be null
            var testProduct = await _productRepository.GetById(product.Id);

            // If everything is ok then:
            await _uow.Commit();

            // The product will be added only after commit
            testProduct = await _productRepository.GetById(product.Id);

            return Ok(testProduct);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TodoModel>> Put(Guid id, [FromBody] TodoViewModel value)
        {
            var product = new TodoModel(id, value.Description);

            _productRepository.Update(product);

            await _uow.Commit();

            return Ok(await _productRepository.GetById(id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            _productRepository.Remove(id);

            // it won't be null
            var testProduct = await _productRepository.GetById(id);

            // If everything is ok then:
            await _uow.Commit();

            // not it must by null
            testProduct = await _productRepository.GetById(id);

            return Ok();
        }
    }
}
