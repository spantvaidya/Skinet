using API.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // public class ProductsController(/*StoreContext context,*/ IProductRepository repository) : ControllerBase
    public class ProductsController(IGenericRepository<Product> productRepo,
                                     IGenericRepository<ProductBrand> productBrandRepo,
                                     IGenericRepository<ProductType> productTypeRepo,
                                     IMapper mapper
                                    ) : ControllerBase
    {
        //private readonly StoreContext _context = context;
        //private readonly IProductRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly IGenericRepository<Product> _productRepo = productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo = productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo = productTypeRepo;

        [HttpGet]
        // public async Task<ActionResult<List<Product>>> GetProducts()
        public async Task<ActionResult<IReadOnlyList<ProductDTO>>> GetProducts()
        {
            //var products = await _repository.GetProductsAsync();
            var spec = new ProductSpecwithBrandAndType();
            var products = await _productRepo.ListAsync(spec);

            // return products.Select(product => new ProductDTO
            // {
            //     Id = product.Id,
            //     Name = product.Name,
            //     Description = product.Description,
            //     Price = product.Price,
            //     PictureUrl = product.PictureUrl,
            //     ProductBrand = product.ProductBrand.Name,
            //     ProductType = product.ProductType.Name
            // }).ToList();

            return Ok(_mapper.Map<IReadOnlyList<ProductDTO>>(products));
            //return Ok(products);
        }

        [HttpGet("{id}")]
        // public async Task<ActionResult<Product>> GetProduct(int id)
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            //return await _productRepo.GetByIdAsync(id);
            var spec = new ProductSpecwithBrandAndType(id);
            var product = await _productRepo.GetEntityWithSpec(spec);

            // return new ProductDTO
            // {
            //     Id = product.Id,
            //     Name = product.Name,
            //     Description = product.Description,
            //     Price = product.Price,
            //     PictureUrl = product.PictureUrl,
            //     ProductBrand = product.ProductBrand.Name,
            //     ProductType = product.ProductType.Name
            // };

            return Ok(_mapper.Map<ProductDTO>(product));
            //return Ok(product);
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