﻿using HRSCompute.Entity;
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
        Employee GetById(int employeeId);
        Task UpdateAsync( Employee employee);
        Task UpdateAsync(int id);
        Task DeleteAsync(int employeeId);
        decimal UnionFees(int Id);
        decimal StudenLoanRepayment(int id,  decimal totalAmount);
        IEnumerable<Employee> GetAll(); 
    }
}
