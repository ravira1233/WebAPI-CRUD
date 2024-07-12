﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_CRUD.Data;

namespace WebAPI_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Employees : ControllerBase
    {
        private EmployeeRepository _employeeRepository;
        public Employees(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
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
            var employee = await _employeeRepository.GetAllEmployeeList();
            return Ok(employee);
        }


        [HttpGet("{ID}")]
        public async Task<ActionResult> GetEmployeeByID([FromRoute] int ID)
        {
            var employee = await _employeeRepository.GetEmpluyeeByID(ID);
            return Ok(employee);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateEmployee([FromRoute]int Id,[FromBody] Employee? model)
        {
             await _employeeRepository.UpdateEmployee(Id, model);
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