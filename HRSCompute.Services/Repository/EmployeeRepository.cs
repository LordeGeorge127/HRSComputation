using HRSCompute.Entity;
using HRSCompute.Persistence;
using HRSCompute.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSCompute.Services.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        private decimal Helb;
        private decimal fee;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Employee newEmployee)
        {
            await _context.Employees.AddAsync(newEmployee);
            await _context.SaveChangesAsync();
        }
        public Employee GetById(int employeeId) => _context.Employees.Where(e => e.Id == employeeId).FirstOrDefault();
        
        public async Task DeleteAsync(int employeeId)
        {
            var employee = GetById(employeeId);
            _context.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Employee> GetAll() => _context.Employees;
        public async Task UpdateAsync(Employee employee)
        {
             _context.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id)
        {
            var employee = GetById(id);
             _context.Update(employee);   
            await _context.SaveChangesAsync();

        }


        public decimal StudenLoanRepayment(int id, decimal totalAmount)
        {
            var employee = GetById(id);
            if(employee.StudentLoan == StudentLoan.Yes )
            {
                Helb = .15m * totalAmount;
            }
            return Helb;
        }

        public decimal UnionFees(int id,decimal totalAmount)
        {
            var employee = GetById(id);
            if (employee.UnionMember == NSSFMember.Yes && totalAmount <18000)
            {
                fee = 720m;
            }
            else
            {
                fee = .06m * totalAmount;
            }
            return fee;
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
        }

   


        //public async Task<Employee> GetBydIdAsNoTracking(int id)
        //{
        //    return await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        //}
    }
}
