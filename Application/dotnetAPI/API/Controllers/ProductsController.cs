using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using Microsoft.AspNetCore.Mvc;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{   [ApiController]
    [Route("api/[controller]")]
   public class ProductsController : ControllerBase
    {   //MyComment: use the private readonly variable for Dependencies injection
        //MyComment: Create a readonly object of StoreCotext and use in the COntroller
        private readonly StoreContext context;

        //MyComment: create Constructor for the Product Controller (context)
        public ProductsController(StoreContext context)
        {
            this.context  = context;
        }
        
        [HttpGet]
        public async  Task<ActionResult<List<Product>>> GetProducts() //return Task for async request
        {
            var products = await context.Products.ToListAsync();
            return Ok(products);
        }

        [HttpGet("{id}")] //api/products/3
        public ActionResult<Product> GetProduct(int id) //example of sync request
        {
            var product = context.Products.Find(id);
            return product;
        }

    }
}