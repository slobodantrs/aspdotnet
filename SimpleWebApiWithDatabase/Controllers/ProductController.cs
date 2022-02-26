using System;
using System.Data;
using Dapper;

using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using SimpleWebApiWithDatabase;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApiWithDatabase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        IDbConnection _conn = null;

        public ProductsController(IDbConnection conn)
        {
            _conn = conn;
        }

[HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts(){
            string querry="SELECT  * FROM Product";
            var products=await _conn.QueryAsync<Product>(querry);
           return products.AsList<Product>();
        }

        /*https://localhost:5001/products/15*/
        [HttpGet("{id}")]     
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            string querry = "SELECT * FROM Product WHERE ProductID="+id;
            var product = await _conn.QuerySingleOrDefaultAsync<Product>(querry);
            return product;


        }
        [HttpPost("Create")]
        public async Task<ActionResult<int>> PostAsync(Product product)
        {
            string querry = @"insert into Product( Name, Color, Size, Price, Quantity) 
            values(@Name,@Color,@Size,@Price,@Quantity)";
            SqlCommand command = new SqlCommand(querry, (SqlConnection)_conn);
            var pr = await _conn.ExecuteAsync(querry, product);
            
            return pr;
        }
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<int>> PutAsync(int id,Product product)
        {
            string querry = @"update Product set  Name=@Name,
             Color=@Color, Size=@Size, Price=@Price, Quantity=@Quantity where ProductID="+id;
            SqlCommand command = new SqlCommand(querry, (SqlConnection)_conn);
            var pr = await _conn.ExecuteAsync(querry, product);
            if(pr==0){
                return NotFound();
            }
            
            return NoContent();
        }
         [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<int>> DeleteProduct(int id)
        {
            string querry = @"delete from Product  where ProductID="+id;
            SqlCommand command = new SqlCommand(querry, (SqlConnection)_conn);
            var pr = await _conn.ExecuteAsync(querry, new{id});
            if(pr==0){
                return NotFound();
            }
            
            //return NoContent();
            return Json(new {success=true,message="Delete successful"});
        }

         [HttpDelete("Delete/All")]
        public async Task<ActionResult<int>> DeleteAllAsync()
        {
            string querry = @"TRUNCATE TABLE  Product";
            SqlCommand command = new SqlCommand(querry, (SqlConnection)_conn);
            var pr = await _conn.ExecuteAsync(querry);
            if(pr==0){
                return NotFound();
            }
            
            return NoContent();
        }

      
    }
}
        