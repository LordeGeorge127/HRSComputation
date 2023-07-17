using HRSCompute.Models;
using HRSCompute.Persistence;
using HRSCompute.Services.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HRSCompute.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeController( EmployeeRepository employeeRepository)
        {
          
            _employeeRepository = employeeRepository;
        }
        public IActionResult Index()
        {
            var employees = _employeeRepository.GetAll().Select(employee => new EmployeeIndexViewModel
            {
                Id = employee.Id,
                EmployeeNo = employee.EmployeeNo,
                FullName = employee.FullName,
                //CHECK THIS ENUM
                Gender = employee.Gender.ToString(),
                ImageUrl = employee.ImageUrl,
                DateJoined = employee.DateJoined,
                City = employee.City,
            }).ToList();
            return View();
        }
    }
}
