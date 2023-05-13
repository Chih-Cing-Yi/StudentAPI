using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using StudentAPI.Data;
using StudentAPI.DTO;
using StudentAPI.Models;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //連接上下文類別
        private readonly WebAPIContext _db;
        //資料庫連線
        private readonly IConfiguration _connconfig;

        public LoginController(WebAPIContext db, IConfiguration connconfig)
        {
            _db = db;
            _connconfig = connconfig;
        }

        #region JWT
        JwtSecurityTokenHandler JwtTokenHandler = new JwtSecurityTokenHandler();

        public IConfiguration Configuration { get; }

        //創建JWT
        string GenerateJwtToken(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidOperationException("Name is not specified.");
            }

            var claims = new[] { new Claim(ClaimTypes.Name, name) };
            var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_connconfig["jwt:Secret"])), SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                "ExampleServer",
                "ExampleClients",
                claims,
                expires: DateTime.Now.AddSeconds(1800),
                signingCredentials: credentials);
            return JwtTokenHandler.WriteToken(token);
        }
        #endregion

        [HttpPost]
        public async Task <IActionResult> Login(User user)
        {
            using var conn = new SqlConnection(_connconfig.GetConnectionString("SchoolConn"));

            //查詢語法
            string sql = @"SELECT UserName,UserPassword
                                    FROM [User]
                                    WHERE UserName COLLATE Latin1_General_CS_AI =@Name
                                    AND UserPassword COLLATE Latin1_General_CS_AI = @Password";

              var res = await conn.QueryAsync<User>(sql, new {Name=user.UserName,Password=user.UserPassword });

            if (res.Count() == 0) 
            {
                return NotFound("登入失敗");//404
            }
            else
            {
                UserDTO userDTO = new UserDTO()
                {
                    UserName = user.UserName,
                    JWT = "Bearer" + GenerateJwtToken(user.UserName),
                };
                return Ok(userDTO);//200
            }
        }

        //新增學生
        [HttpPost("register")]
        public async Task<IActionResult> post(User model)
        {
            await _db.AddRangeAsync(model);
            await _db.SaveChangesAsync();
            return NoContent();//回傳204
        }


    }
}
