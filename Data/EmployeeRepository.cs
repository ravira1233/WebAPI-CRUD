using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace WebAPI_CRUD.Data
{
    public class EmployeeRepository
    {
        private AppDbContext _context;
        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddEmployee(Employee model)
        {
            await _context.Set<Employee>().AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Employee>> GetAllEmployeeList()
        {
            return await _context.employees.ToListAsync();
        }

        public async Task<Employee> GetEmpluyeeByID(int ID)
        {
            return await _context.employees.FindAsync(ID);
        }

        public async Task UpdateEmployee(Employee? model)
        {
            var employee = await _context.employees.FindAsync(model.Id);
            if(employee ==null)
            {
               
                throw new System.Exception("Employee is empty");
            }
            employee.Name = model.Name;
            employee.Email = model.Email;
            employee.Phone = model.Phone;
            employee.salary = model.salary;
            employee.Age = model.Age;
           await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployee(int Id)
        {
            var employee = await _context.employees.FindAsync(Id);
            if (employee == null)
            {
                throw new System.Exception("Employee is empty");
            }
            _context.employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<Employee> GetEmployeeByEmail(string Email)
        {
            return await _context.employees.Where(x => x.Email == Email).FirstOrDefaultAsync();
        }
    }
}
