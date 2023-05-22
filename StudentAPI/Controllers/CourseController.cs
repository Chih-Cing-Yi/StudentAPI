using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.Data;
using StudentAPI.DTO;
using StudentAPI.Models;
using System.Data.SqlClient;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CourseController : ControllerBase
    {
        //連接上下文類別
        private readonly WebAPIContext _db;
        //資料庫連線
        private readonly IConfiguration _connconfig;

        public CourseController(WebAPIContext db, IConfiguration connconfig)
        {
            _db = db;
            _connconfig = connconfig;
        }

        //查詢課程
        [HttpGet]
        public async Task<IEnumerable<CourseM>> get()
        {
            //查詢語句
            string sql = @"SELECT *
                                        FROM Course_M";
            using (var conn = new SqlConnection(_connconfig.GetConnectionString("SchoolConn")))
            {
                return await conn.QueryAsync<CourseM>(sql, new { });
            }
        }

        //查詢修課學生
        [HttpGet("{courseId}")]
        public async Task<IEnumerable<CourseStudentDTO>> get2(int courseId)
        {
            //查詢語句
            string sql = @"SELECT d.courseId,d.StudentId,s.studentName
                                        FROM Course_D d
                                        LEFT JOIN Student s
                                        ON d.StudentId = s.StudentId
                                        WHERE d.courseId = @courseId
                                        ORDER BY d.StudentId";
            using (var conn = new SqlConnection(_connconfig.GetConnectionString("SchoolConn")))
            {
                return await conn.QueryAsync<CourseStudentDTO>(sql, new { courseId });
            }
        }

        //新增修課學生
        [HttpPost]
        public async Task<IActionResult> post(CourseD model)
        {
            await _db.AddRangeAsync(model);
            await _db.SaveChangesAsync();
            return NoContent();//回傳204
        }

    }
}
