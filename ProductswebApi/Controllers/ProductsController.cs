using System;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Dapper;
using System.Collections.Generic;
using ProductWebApi.Models;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;



namespace ProductWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        IDbConnection _conn = null;

        public ProductsController(IDbConnection conn)
        {
            _conn = conn;
        }
         [HttpGet]
   /*     public IEnumerable<Product> Get()
        {
            string querry = @"select * from Product";
            var products = _conn.Query<Product>(querry);
            return products.AsList();
        }*/
          [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            string querry = "SELECT * FROM Product";
            var products = await _conn.QueryAsync<Product>(querry);
            return products.AsList<Product>();
            
            /*Varijanta sa vracanjem kao Json*/
          /*  var podaciJSON=Json(new {data=products.AsList<Product>()});
            return podaciJSON;*/


        }
          [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            string querry = "SELECT * FROM Product WHERE ProductID="+id;
            var product = await _conn.QuerySingleOrDefaultAsync<Product>(querry);
            return product;


        }
        [HttpPost("Create")]
        public int Post(Product product)
        {

            string querry = @"insert into Product( Name, Color, Size, Price, Quantity) values(@Name,@Color,@Size,@Price,@Quantity)";
            SqlCommand command = new SqlCommand(querry, (SqlConnection)_conn);
           
            var pr = _conn.Execute(querry, product);
            return pr;
        }
        [HttpPost("CreateAsync")]
        public async Task<ActionResult<int>> PostAsync(Product product)
        {
            string querry = @"insert into Product( Name, Color, Size, Price, Quantity) values(@Name,@Color,@Size,@Price,@Quantity)";
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
        [HttpPatch("Update/{id}")]
        public async Task<ActionResult<int>> PatchAsync(int id,Product product)
        {
            string querry = @"update Product set Price=@Price, Quantity=@Quantity where ProductID=@ProductId";
            SqlCommand command = new SqlCommand(querry, (SqlConnection)_conn);
            var pr = await _conn.ExecuteAsync(querry, product);
            if(pr==0){
                return NotFound();
            }
            
            return NoContent();
        }

         [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<int>> DeleteAsync(int id)
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