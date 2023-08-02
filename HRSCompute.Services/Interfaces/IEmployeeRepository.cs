using HRSCompute.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSCompute.Services.Interfaces
{
    public interface IEmployeeRepository
    {
        Task CreateAsync(Employee newEmployee);
        //Task<Employee> GetBydIdAsNoTracking(int id);
        Employee GetById(int employeeId);
        Task<Employee> GetByIdAsync(int id);
        Task UpdateAsync( Employee employee);
        Task UpdateAsync(int id);
     
        Task DeleteAsync(int employeeId);
        decimal UnionFees(int id, decimal totalAmount);
        decimal StudenLoanRepayment(int id,  decimal totalAmount);
        IEnumerable<Employee> GetAll();
        IEnumerable<SelectListItem> GetAllEmployeesForPayroll();
            
    }
}
