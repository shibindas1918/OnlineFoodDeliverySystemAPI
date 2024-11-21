using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineFoodDeliverySystemAPI.Data;

namespace OnlineFoodDeliverySystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly DatabaseHelper _databaseHelper;
        public RestaurantController(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }
        [HttpGet("ALL-Restaurants")]
        public IActionResult Get()
        {
            string query = "select *from Restaurants";
            try
            {
                var result = _databaseHelper.ExecuteQuery(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
