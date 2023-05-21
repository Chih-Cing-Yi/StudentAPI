using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.Data;
using StudentAPI.DTO;
using StudentAPI.Models;
using Dapper;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        //連接上下文類別
        private readonly WebAPIContext _db;
        //資料庫連線
        private readonly IConfiguration _connconfig;

        public StudentController(WebAPIContext db, IConfiguration connconfig)
        {
            _db = db;
            _connconfig = connconfig;
        }

        #region dapper查詢
        //查詢學生
        [HttpGet]
        public async Task<IEnumerable<StudentClassDTO>> get()
        {
            //查詢語句
            string sql = @"SELECT s.StudentId,s.StudentName,s.studentAge,
                                        s.studentAddress,c.classId,c.className
                                        FROM Student s
                                        LEFT JOIN Class c
                                        ON s.classId = c.classId";
            using (var conn = new SqlConnection(_connconfig.GetConnectionString("SchoolConn")))
            {
                return await conn.QueryAsync<StudentClassDTO>(sql, new { });
            }
        }
        #endregion

        //新增學生
        [HttpPost]
        public async Task<IActionResult> post(Student model)
        {
           await _db.AddRangeAsync(model);
           await _db.SaveChangesAsync();
           return NoContent();//回傳204
        }

        ////新增學生(如需要系統寫入值)
        //[HttpPost]
        //public async Task<IActionResult> post2(Student model)
        //{
        //    Student student = new Student();
        //    {
        //        student.StudentId = model.StudentId;
        //        student.StudentName = model.StudentName;
        //        student.StudentAddress = model.StudentAddress;
        //        student.ClassId = model.ClassId;
        //    }
        //    await _db.AddRangeAsync(student);
        //    await _db.SaveChangesAsync();
        //    return NoContent();//回傳204
        //}

        //修改
        [HttpPut]
        public async Task<IActionResult> Put(Student model)
        {
            //查詢要修改的值
            var student = await _db.Students.FindAsync(model.StudentId);

            if (student == null) { return NotFound(); }//404 
            
            //修改的
            student.StudentId = model.StudentId;
            student.StudentName = model.StudentName;
            student.StudentAddress = model.StudentAddress;
            student.StudentAge= model.StudentAge;
            student.ClassId = model.ClassId;

            await _db.SaveChangesAsync();
            return NoContent();//回傳204
        }

        //刪除
        [HttpDelete("{studentId}")]
        public async Task<IActionResult> Delete(int studentId)
        {
            //查詢要刪除的值
            var student = await _db.Students.FindAsync(studentId);

            if (student == null) { return NotFound(); }//404 
            
            //刪除
            _db.Remove(student);

            await _db.SaveChangesAsync();
            return NoContent();//回傳204
        }

    }
}
