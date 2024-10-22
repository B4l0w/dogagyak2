using dogagyak2.Model;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace dogagyak2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : Controller
    {
        Connect conn = new Connect();
        [HttpGet]
        public List<Blog> Get() 
        {
            List<Blog> blogok = new List<Blog>();

            conn.Connection.Open();
            string sql = "SELECT * FROM `blogs`";
            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            do
            {
                var result = new Blog
                {
                    Id = reader.GetGuid(0),
                    Title = reader.GetString(1),
                    Description = reader.GetString(2),
                    CreatedTime = reader.GetDateTime(3),
                    LastUpdated = reader.GetDateTime(4),
                };
                blogok.Add(result);
            } while (reader.Read());
            conn.Connection.Close();

            return blogok;
        }

        [HttpGet("{azon}")]
        public Blog GetBlog(Guid azon) 
        {
            conn.Connection.Open();
            string sql = $"SELECT * FROM `blogs` WHERE Id = '{azon}'";
            MySqlCommand cmd = new MySqlCommand( sql, conn.Connection );
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            var result = new Blog
            {
                Id = reader.GetGuid(0),
                Title = reader.GetString(1),
                Description = reader.GetString(2),
                CreatedTime = reader.GetDateTime(3),
                LastUpdated = reader.GetDateTime(4),
            };
            return result;
        }
        
    }
}
