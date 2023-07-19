using HRSCompute.Entity;
using HRSCompute.Models;
using HRSCompute.Persistence;
using HRSCompute.Services.Interfaces;
using HRSCompute.Services.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HRSCompute.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _hostEnvironment;

        public EmployeeController( IEmployeeRepository employeeRepository, IWebHostEnvironment hostEnvironment)
        {
          
            _employeeRepository = employeeRepository;
            _hostEnvironment = hostEnvironment;
        }

        private string GenerateEmployeeNumber()
        {
            // Get the total number of existing employees from the database or other source
            int employeeCount = _employeeRepository.GetAll().Count();

            // Increment the count and format the employee number
            int nextEmployeeCount = employeeCount + 1;
            string EmployeeNo = "EMP" + nextEmployeeCount.ToString().PadLeft(3, '0');

            return EmployeeNo;
        }
        public IActionResult Index()
        {
            var employees = _employeeRepository.GetAll().Select(employee => new EmployeeIndexViewModel
            {
                Id = employee.Id,
                EmployeeNo = employee.EmployeeNo,
                FullName = employee.FullName,
                Designation = employee.Designation,
                Gender = employee.Gender.ToString(),
                ImageUrl = employee.ImageUrl,
                DateJoined = employee.DateJoined,
                City = employee.City,
            }).ToList();
            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var model = new EmployeeCreateViewModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {

                    Id = model.Id,
                    EmployeeNo = GenerateEmployeeNumber(),
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    FullName = model.FullName,
                    Gender = model.Gender,
                    Email = model.Email,
                    DOB = model.DOB,
                    NHIF = model.NHIF,
                    Phone = model.Phone,
                    PIN = model.PIN,
                    DateJoined = model.DateJoined,
                    NationalID = model.NationalID,
                    PaymentMethod = model.PaymentMethod,
                    StudentLoan = model.StudentLoan,
                    UnionMember = model.UnionMember,
                    Address = model.Address,
                    City = model.City,
                    PostCode = model.PostCode,
                    Designation = model.Designation,
                };
                //imageurl
                if (model.ImageUrl != null && model.ImageUrl.Length > 0)
                {;
                    var uploadDir = @"images/employee";
                    var fileName = Path.GetFileNameWithoutExtension(model.ImageUrl.FileName);
                    var extension = Path.GetExtension(model.ImageUrl.FileName);
                    var webRootPath = _hostEnvironment.WebRootPath;
                    fileName = DateTime.UtcNow.ToString("yyyyyMMdd") + fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    await model.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));
                    employee.ImageUrl = "/" + uploadDir + "/" + fileName;
                }
                await _employeeRepository.CreateAsync(employee);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee == null) { return NotFound(); }
            var model = new EmployeeEditViewModel()
            {
                Id = employee.Id,
                EmployeeNo = employee.EmployeeNo,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                Gender = employee.Gender.ToString(),
                Email = employee.Email,
                DOB = employee.DOB,
                DateJoined = employee.DateJoined,
                NHIF = employee.NHIF,
                Phone = employee.Phone,
                NationalID = employee.NationalID,
                PaymentMethod = employee.PaymentMethod,
                StudentLoan = employee.StudentLoan,
                NSSFMember = employee.UnionMember,
                Address = employee.Address,
                City = employee.City,
                PostCode = employee.PostCode,
                Designation = employee.Designation,
            };
            return View(model);
        }

    }
}
