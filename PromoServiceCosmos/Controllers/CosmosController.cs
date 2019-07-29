using Microsoft.AspNetCore.Mvc;
using PromoServiceCosmos.DataAccess;
using PromoServiceCosmos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromoServiceCosmos.Controllers
{
    [Route("/v1/promotions")]
    [ApiController]
    public class CosmosController : ControllerBase
    {
        ICosmosDataAdapter _adapter;
        public CosmosController(ICosmosDataAdapter adapter)
        {
            _adapter = adapter;
        }

        [HttpGet("createdb")]
        public async Task<IActionResult> CreateDatabase()
        {
            var result = await _adapter.CreateDatabase("PromoDatabase");
            return Ok(result);
        }

        [HttpGet("createcollection")]
        public async Task<IActionResult> CreateCollection()
        {
            var result = await _adapter.CreateCollection("PromoDatabase", "PromoCollection");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDocument([FromBody] ProductPromo productpromo)
        {
            var result = await _adapter.CreateDocument("PromoDatabase", "PromoCollection", productpromo);
            return Ok(result);
        }

        [HttpPost]
        [Route("action")]
        public async Task<IActionResult> CreateAction([FromBody] ProductPromoAction productpromoaction)
        {

            var result = await _adapter.CreateAction("PromoDatabase", "PromoCollection", productpromoaction);
            return Ok(result);
        }

       /* [HttpPut("{id}/action")]
        public async Task<IActionResult> UpdateAction([FromBody] ProductPromo productpromo)
        {
            var result = await _adapter.updateDocumentAsyncAction("PromoDatabase", "PromoCollection", productpromo);
            return Ok(result);
        }*/


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _adapter.DeleteUserAsync("PromoDatabase", "PromoCollection", id);
            return Ok("deleted");
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _adapter.GetData("PromoDatabase", "PromoCollection");
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
           
                // ProductPromo productpromo = new ProductPromo() ;
                
                var result = await _adapter.GetDataById("PromoDatabase", "PromoCollection", id);
            Console.WriteLine(result);
                return Ok(result);
            
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] ProductPromo productpromo)
        {
            var result = await _adapter.updateDocumentAsync("PromoDatabase", "PromoCollection", productpromo);
            return Ok(result);
        }


    }
}
