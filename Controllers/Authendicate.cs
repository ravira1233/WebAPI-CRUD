using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebAPI_CRUD.Data;
using WebAPI_CRUD.Model;

namespace WebAPI_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Authendicate : ControllerBase
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly JWTOption _jwtOption;
        public Authendicate(EmployeeRepository employeeRepository,IOptions<JWTOption> jwtOption)
        {
            _employeeRepository = employeeRepository;
            _jwtOption = jwtOption.Value;
        }


        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginModel login)
        {


            var employee=   await  _employeeRepository.GetEmployeeByEmail(login.Email);
            if (employee is null)
            {
                BadRequest(new { error = "Email does't exist" });
            }
            if (login.Password != employee.password)
            {
                BadRequest(new { error = "Username/Password does't matched" });
            }

            var jwtKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOption.Key));
            var credential = new SigningCredentials(jwtKey, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>()
            {
                new Claim("Email",login.Email),
                new Claim("New ID","")
            };
            var sToken=new JwtSecurityToken(_jwtOption.Key,_jwtOption.Issuer, claims, expires:DateTime.Now.AddHours(5),signingCredentials:credential);
            var token = new JwtSecurityTokenHandler().WriteToken(sToken);
            return Ok(new{token=token});
        }
    }
}
