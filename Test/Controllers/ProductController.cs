using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductDB;
using ProductDB.Entitys;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDbcontext prdouctDbcontext;
        public ProductController(ProductDbcontext dbcontext)
        {
            this.prdouctDbcontext = dbcontext;
        }

        //Create
        [HttpPost]

        public async Task<ActionResult<IEnumerable<ProductEntitys>>> AddProduct(ProductEntitys product)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            if (product != null)
            {
                prdouctDbcontext.Products.Add(product);
                await prdouctDbcontext.SaveChangesAsync();
                return Ok(await prdouctDbcontext.Products.ToListAsync());

            }
            return BadRequest("Object instance not set");
        }

        #region Read

        [HttpGet]

        public async Task<ActionResult<IEnumerable<ProductEntitys>>> GetAllProduct() => await prdouctDbcontext.Products.AsNoTracking().ToListAsync();

        [HttpGet("{id}")]

        public async Task<ActionResult<IEnumerable<ProductEntitys>>>GetId(int id)
        {
            var product = prdouctDbcontext.Products.SingleOrDefaultAsync(p => p.Id == id);
            
            if(product!= null)
            {
                return Ok(product);
            }
            return NotFound("Product is not available");
        }

        #endregion

        #region Update
        [HttpPut]
        public async Task<ActionResult<IEnumerable<ProductEntitys>>> UpdateProduct(ProductEntitys updateProduct)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var pr = await prdouctDbcontext.Products.FirstOrDefaultAsync(p => p.Id == updateProduct.Id);
            if (updateProduct != null)
            {

                pr.Title = updateProduct.Title;
                pr.Description = updateProduct.Description;
                pr.Price = updateProduct.Price;
                await prdouctDbcontext.SaveChangesAsync();
                return Ok(pr);

            }
            return NotFound("User not found");
        }

        #endregion
        #region Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<ProductEntitys>>> DeleteProduct(int id)
        {
            var product = await prdouctDbcontext.Products.FirstOrDefaultAsync(p => p.Id == id);
            if(product != null)
            {
                prdouctDbcontext.Remove(product);
                await prdouctDbcontext.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }


        #endregion



    }
}
