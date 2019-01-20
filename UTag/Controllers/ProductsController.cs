using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UTag.Helpers;
using UTag.Models;
using UTag.Services.Interfaces;
using UTag.ViewModels;

namespace UTag.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly UTagContext _context;
        private readonly IMLService _mLService;
        private readonly IMapper _mapper;

        public ProductsController(UTagContext context, IMLService mLService, IMapper mapper)
        {
            _context = context;
            _mLService = mLService;
            _mapper = mapper;
        }

        // GET: api/Products
        [HttpGet]
        public IEnumerable<ProductViewModel> GetProducts()
        {
            return _mapper.Map<List<ProductViewModel>>(_context.Products.ToList());
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<ProductViewModel>(product);

            return Ok(result);
        }

        // GET: api/Products/Filter/2
        [HttpGet("Filter/{filters}")]
        public async Task<IActionResult> GetProductsByFilters([FromRoute] List<int> filters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var products = await _context.Products.Include(el => el.FilterValues).Where(el => CheckFilters(el.FilterValues, filters)).ToListAsync();

            if (products == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<ProductViewModel>>(products));
        }

        private bool CheckFilters(IEnumerable<FilterValue> filterValuesOrigin, List<int> filterValuesNeeded)
        {
            foreach(var fv in filterValuesNeeded)
            {
                if (!filterValuesOrigin.Any(el => el.FilterId == fv)) return false;
            }
            return true;
        }

        // GET: api/Products/Filter/2
        [HttpGet("Tag/{tags}")]
        public async Task<IActionResult> GetProductsByTags([FromRoute] List<int> tags)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var products = await _context.Products.Include(el => el.ConnectedTags).Where(el => CheckTags(el.ConnectedTags, tags)).ToListAsync();

            if (products == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<ProductViewModel>>(products));
        }

        private bool CheckTags(IEnumerable<ProductTag> tagsOrigin, List<int> tagsNeeded)
        {
            foreach (var fv in tagsNeeded)
            {
                if (!tagsOrigin.Any(el => el.TagId == fv)) return false;
            }
            return true;
        }

        // GET: api/Products/Liked
        [HttpGet("Liked/{personId}")]
        public async Task<IActionResult> GetLikedProducts([FromRoute] int personId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var person = await _context.Persons.Include(el=>el.LikedProducts).FirstAsync(el=>el.Id == personId);
            var products = person.LikedProducts;
            //var products = await //_mLService.GetReccomendedProducts(person);

            if (products == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<ProductViewModel>>(products));
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute] int id, [FromBody] ProductViewModel product)
        {
            var orProduct = _mapper.Map<Product>(product);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orProduct.Id)
            {
                return BadRequest();
            }

            _context.Entry(orProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // PUT: api/Products/LikeProduct
        [HttpPost("LikeProduct")]
        public async Task<IActionResult> LikeProduct([FromBody] ProductConnectionViewModel like)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var newLike = new ProductConnection()
            {
                PersonFromId = like.PersonFromId,
                PersonToId = like.PersonToId,
                ProductId = like.ProductId
            };

            _context.ProductConnections.Add(newLike);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                
                    return NotFound();
                
            }

            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] ProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var orProduct = _mapper.Map<Product>(product);

            _context.Products.Add(orProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = orProduct.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}