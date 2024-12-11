using Microsoft.AspNetCore.Mvc;
using OnlineFoodDeliverySystemAPI.Data;
using OnlineFoodDeliverySystemAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq.Expressions;

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
        public IActionResult GetRestaurants()
        {
            string query = "SELECT * FROM Restaurants";
            try
            {
                DataTable result = _databaseHelper.ExecuteQuery(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddRestaurant(Restaurant restaurant)
        {
            string query = $"insert into restaurants (name,location)values(@name,@location)";
            try
            {
                DataTable result;
                using (var conn = new SqlConnection(_databaseHelper._ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", restaurant.Name);
                        cmd.Parameters.AddWithValue("@location", restaurant.Location);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            result = new DataTable();
                            adapter.Fill(result);
                        }

                    }
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("Menus/{restaurantId}")]
        public IActionResult GetMenu(int restaurantId)
        {
            string query = "SELECT * FROM Menus WHERE RestaurantId = @RestaurantId";
            try
            {

                DataTable result;
                using (SqlConnection conn = new SqlConnection(_databaseHelper._ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@RestaurantId", restaurantId);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            result = new DataTable();
                            adapter.Fill(result);
                        }
                    }
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("AddOrder")]
        public ActionResult AddOrder(OrderRequest orderRequest)
        {
            string query = @"
                INSERT INTO Orders (CustomerName, RestaurantId, TotalPrice, OrderDetails) 
                VALUES (@CustomerName, @RestaurantId, @TotalPrice, @OrderDetails)";
            try
            {
                using (SqlConnection conn = new SqlConnection(_databaseHelper._ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerName", orderRequest.CustomerName);
                        cmd.Parameters.AddWithValue("@RestaurantId", orderRequest.RestaurantId);
                        cmd.Parameters.AddWithValue("@TotalPrice", orderRequest.TotalPrice);
                        cmd.Parameters.AddWithValue("@OrderDetails", orderRequest.OrderDetails);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            return Ok("Order added successfully.");
                        else
                            return BadRequest("Failed to add order.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
