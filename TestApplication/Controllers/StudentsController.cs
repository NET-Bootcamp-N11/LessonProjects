using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using TestApplication.Models;
using Dapper;

namespace TestApplication.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly string _conString = "Server=127.0.0.1;Port=5432;Database=TestDb;User Id=postgres;Password=root;";

        [HttpGet]
        public List<Student> GetStudents()
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_conString))
            {
                string query = "select * from students";
                return con.Query<Student>(query).ToList();
            }
        }

        [HttpPost]
        public int AddStudent(string studentName, int studentAge)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_conString))
            {
                string query = "insert into students(name,age) values(@name,@age);";
                return con.Execute(query, new { name = studentName, age = studentAge });
            }
        }

        [HttpDelete]
        public int DeleteStudent(int studentId)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_conString))
            {
                string query = "delete from students where id = @delId";
                return con.Execute(query, new { delId = studentId });
            }
        }

        [HttpPatch]
        public int PatchStudentName(int studentId, string studentName)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_conString))
            {
                string query = "update students set name = @names where id = @ids";
                return con.Execute(query, new { ids = studentId, names = studentName });
            }
        }

        [HttpPatch]
        public int PatchStudentAge(int studentId, int studentAge)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_conString))
            {
                string query = "update students set age = @age where id = @ids";
                return con.Execute(query, new { age = studentAge, ids = studentId });
            }
        }

        [HttpPut]
        public int PutStudentAge(int studentId, string studentName, int studentAge)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_conString))
            {
                string query = "update students set name = @name, age = @age where id = @ids";
                return con.Execute(query, new { age = studentAge, name = studentName, ids = studentId });
            }
        }

    }
}