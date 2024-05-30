using API.DTOs;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // [ApiController]
    // [Route("api/[controller]")]
    // public class ProductsController(/*StoreContext context,*/ IProductRepository repository) : ControllerBase
    public class ProductsController(IGenericRepository<Product> productRepo,
                                     IGenericRepository<ProductBrand> productBrandRepo,
                                     IGenericRepository<ProductType> productTypeRepo,
                                     IMapper mapper
                                    ) : BaseApiController
    {
        //private readonly StoreContext _context = context;
        //private readonly IProductRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly IGenericRepository<Product> _productRepo = productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo = productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo = productTypeRepo;

        [Cache(600)]
        [HttpGet]
        // public async Task<ActionResult<List<Product>>> GetProducts()
        public async Task<ActionResult<Pagination<ProductDTO>>> GetProducts(
                        [FromQuery] ProductSpecParams productParams)
        {
            //var products = await _repository.GetProductsAsync();
            var spec = new ProductSpecwithBrandAndType(productParams);

            var countSpec = new ProductFilterForCountSpec(productParams);

            var totalItems = await _productRepo.CountAsync(countSpec);

            var products = await _productRepo.ListAsync(spec);

            #region //Commented Oldcode
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
            #endregion

            var data = _mapper.Map<IReadOnlyList<ProductDTO>>(products);
            return Ok(new Pagination<ProductDTO>(productParams.PageIndex,
            productParams.PageSize, totalItems, data));
            //return Ok(products);
        }

        [Cache(600)]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        // public async Task<ActionResult<Product>> GetProduct(int id)
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            //return await _productRepo.GetByIdAsync(id);
            var spec = new ProductSpecwithBrandAndType(id);
            var product = await _productRepo.GetEntityWithSpec(spec);

            if (product == null) return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<ProductDTO>(product));
            //return Ok(product);
        }

        [Cache(600)]
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productBrandRepo.ListAllAsync());
        }
        
        [Cache(600)]
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductTypes()
        {
            return Ok(await _productTypeRepo.ListAllAsync());
        }
    }
}