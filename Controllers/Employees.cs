using LazyCache;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using WebAPI_CRUD.Cache;
using WebAPI_CRUD.Data;

namespace WebAPI_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class Employees : ControllerBase
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly ICacheProvider _cacheProvider;
   
        public Employees(EmployeeRepository employeeRepository, ICacheProvider cacheProvider)
        {
            _employeeRepository = employeeRepository;
            _cacheProvider = cacheProvider;
        }

        [HttpPost]
        public async Task<ActionResult> AddEmployee([FromBody] Employee model)
        {
            await _employeeRepository.AddEmployee(model);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetEmployee()
        {
            if (!_cacheProvider.TryGetValue(CacheKey.employeeKey, out List<Employee> employee))
            {
                employee = await _employeeRepository.GetAllEmployeeList();
                var memoryoption = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(30),
                    SlidingExpiration= TimeSpan.FromMinutes(30),
                    Size=1024
                };
                _cacheProvider.Set(CacheKey.employeeKey, employee, memoryoption);

            }

            return Ok(employee);
        }


        [HttpGet("{ID}")]
        public async Task<ActionResult> GetEmployeeByID([FromRoute] int ID)
        {
            var employee = await _employeeRepository.GetEmpluyeeByID(ID);
            return Ok(employee);
        }

        [HttpPut()]
        public async Task<ActionResult> UpdateEmployee( [FromBody] Employee? model)
        {
            await _employeeRepository.UpdateEmployee(model);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteEmployee([FromRoute] int Id)
        {
            await _employeeRepository.DeleteEmployee(Id);
            return Ok();
        }
    }
}
