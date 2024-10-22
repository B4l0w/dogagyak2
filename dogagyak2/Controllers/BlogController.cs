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

        [HttpPost]
        public Blog Post(string Title, string Description) 
        {
            Guid Id = Guid.NewGuid();
            conn.Connection.Open();
            DateTime CreatedTime = DateTime.Now;
            DateTime LastUpdated = DateTime.Now;
            string sql = $"INSERT INTO `blogs`(`Id`, `Title`, `Description`, `CreatedTime`, `LastUpdated`) VALUES ('{Id}','{Title}','{Description}','{CreatedTime}','{LastUpdated}')";
            MySqlCommand cmd = new MySqlCommand ( sql, conn.Connection );
            cmd.ExecuteNonQuery();

            conn.Connection.Close();

            var result = new Blog
            {
                Id = Id,
                Title = Title,
                Description = Description,
                CreatedTime = DateTime.Now,
                LastUpdated = DateTime.Now
            };
            return result;
        }
        [HttpPut]
        public Blog Put(Guid Id, string NewTitle, string NewDescription)
        {
            conn.Connection.Open();
            DateTime LastUpdated = DateTime.Now;
            DateTime CreatedTime = DateTime.Now;
            string sql = $"UPDATE `blogs` SET `Title`='{NewTitle}',`Description`='{NewDescription}',`LastUpdated`='{LastUpdated}' WHERE Id = '{Id}'";
            MySqlCommand cmd = new MySqlCommand (sql, conn.Connection );
            cmd.ExecuteNonQuery();
            conn.Connection.Close();

            var result = new Blog
            {
                Id = Id,
                Title = NewTitle,
                Description = NewDescription,
                CreatedTime = DateTime.Now,
                LastUpdated = DateTime.Now,
            };

            return result;
        }

        [HttpDelete]
        public void Delete(Guid Id) 
        {
            conn.Connection.Open();
            string sql = $"DELETE FROM `blogs` WHERE Id = '{Id}'";
            MySqlCommand cmd = new MySqlCommand (sql , conn.Connection );
            cmd.ExecuteNonQuery();
        }
    }
}
