using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // public class ProductsController(/*StoreContext context,*/ IProductRepository repository) : ControllerBase
    public class ProductsController(IGenericRepository<Product> productRepo,
                                     IGenericRepository<ProductBrand> productBrandRepo,
                                     IGenericRepository<ProductType> productTypeRepo
                                    ) : ControllerBase
    {
        //private readonly StoreContext _context = context;
        //private readonly IProductRepository _repository = repository;

        private readonly IGenericRepository<Product> _productRepo = productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo = productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo = productTypeRepo;

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            //var products = await _repository.GetProductsAsync();
            var spec = new ProductSpecwithBrandAndType();
            var products = await _productRepo.ListAsync(spec);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            //return await _productRepo.GetByIdAsync(id);
            var spec = new ProductSpecwithBrandAndType(id);
            var product = await _productRepo.GetEntityWithSpec(spec);
            return Ok(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productBrandRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductTypes()
        {
            return Ok(await _productTypeRepo.ListAllAsync());
        }
    }
}