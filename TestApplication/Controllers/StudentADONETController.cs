using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApplication.Models;
using Npgsql;
using System.Xml.Linq;
namespace TestApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentADONETController : ControllerBase
    {
        private readonly string _conString = "Server=127.0.0.1;Port=5432;Database=TestDb;User Id=postgres;Password=root;";

        [HttpGet]
        [Route("ADO/Get")]
        public List<Student> Get()
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_conString))
            {
                con.Open();
                string query = "select * from students";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                List<Student> students = new List<Student>();
                while (reader.Read())
                {
                    students.Add(new Student()
                    {
                        Id = (int)reader[0],
                        Name = (string)reader[1],
                        Age = (int)reader[2]
                    });
                }
                return students;
            }
        }
        [HttpPost]
        [Route("ADO/Post")]
        public int Post(string name, int age)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_conString))
            {
                con.Open();
                string query = "insert into students(name,age) values(@name,@age);";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.Add(new NpgsqlParameter("name", name));
                cmd.Parameters.Add(new NpgsqlParameter("age", age));
                return cmd.ExecuteNonQuery();
            }
        }

        [HttpDelete]
        [Route("ADO/Delete")]
        public int Delete(int id)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_conString))
            {
                con.Open();
                string query = "delete from students where id = @id";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.Add(new NpgsqlParameter("id", id));
                return cmd.ExecuteNonQuery();
            }
        }


        [HttpPut]
        [Route("ADO/Put")]
        public int Put(int id, string name, int age)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_conString))
            {
                con.Open();
                string query = "update students set name = @name,age = @age where id = @id;";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.Add(new NpgsqlParameter("id", id));
                cmd.Parameters.Add(new NpgsqlParameter("name", name));
                cmd.Parameters.Add(new NpgsqlParameter("age", age));
                return cmd.ExecuteNonQuery();
            }
        }

        [HttpPatch]
        [Route("ADO/Patch/age")]
        public int Patch(int id, int age)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_conString))
            {
                con.Open();
                string query = "update students set age = @age where id = @id;";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.Add(new NpgsqlParameter("id", id));
                cmd.Parameters.Add(new NpgsqlParameter("age", age));
                return cmd.ExecuteNonQuery();
            }
        }

        [HttpPatch]
        [Route("ADO/Patch/name")]
        public int Patch(int id, string name)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_conString))
            {
                con.Open();
                string query = "update students set name = @name where id = @id;";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.Add(new NpgsqlParameter("id", id));
                cmd.Parameters.Add(new NpgsqlParameter("name", name));
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
