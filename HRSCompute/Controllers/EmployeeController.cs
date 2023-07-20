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
        public  IActionResult Index()
        {
            //var employees = _employeeRepository.GetAll().Select(employee => new EmployeeIndexViewModel
            //{
            //    Id = employee.Id,
            //    EmployeeNo = employee.EmployeeNo,
            //    FullName = employee.FullName,
            //    Designation = employee.Designation,
            //    Gender = employee.Gender.ToString(),
            //    ImageUrl = employee.ImageUrl,
            //    DateJoined = employee.DateJoined,
            //    City = employee.City,
            //}).ToList();
            //return View(employees);
            IEnumerable<Employee> employees = _employeeRepository.GetAll();
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
                Gender = employee.Gender,
                Email = employee.Email,
                DOB = employee.DOB,
                DateJoined = employee.DateJoined,
                NHIF = employee.NHIF,
                PIN = employee.PIN,
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
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EmployeeEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", " Failed to update");
                return View(model);
            }
            var employee = _employeeRepository.GetById(id);
            //var employee = _employeeRepository.GetById(model.Id); test both
            if (employee == null) { return NotFound(); }
            employee.EmployeeNo = model.EmployeeNo;
            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.Gender = model.Gender;
            employee.Email = model.Email;
            employee.Phone = model.Phone;
            employee.PIN = model.PIN;
            employee.Designation = model.Designation;
            employee.NationalID = model.NationalID;
            employee.NHIF = model.NHIF;
            employee.PaymentMethod = model.PaymentMethod;
            employee.StudentLoan = model.StudentLoan;
            employee.UnionMember = model.NSSFMember;
            employee.Address = model.Address;
            employee.City = model.City;
            employee.PostCode = model.PostCode;
            //image url
            if (model.ImageUrl != null && model.ImageUrl.Length > 0)
            {
                var uploadDir = @"images/employee";
                var fileName = Path.GetFileNameWithoutExtension(model.ImageUrl.FileName);
                var extension = Path.GetExtension(model.ImageUrl.FileName);
                var webRootPath = _hostEnvironment.WebRootPath;
                fileName = DateTime.UtcNow.ToString("yyMMdd") + fileName + extension;
                var path = Path.Combine(webRootPath, uploadDir, fileName);
                await model.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));
                employee.ImageUrl = "/" + uploadDir + "/" + fileName;

            }
            await _employeeRepository.UpdateAsync(employee);
            return RedirectToAction("Index");


        }
        public async Task<IActionResult> Detail(int id)
        {
            //Employee employee = _employeeRepository.GetById(id);
            //return View(employee);
            var employee = _employeeRepository.GetById(id);
            if (employee == null) { return NotFound(); }
            EmployeeDetailViewModel model = new EmployeeDetailViewModel()
            {
                Id = employee.Id,
                EmployeeNo = employee.EmployeeNo,
                FullName = employee.FullName,
                Gender = employee.Gender,
                Email = employee.Email,
                DOB = employee.DOB,
                DateJoined = employee.DateJoined,
                Designation = employee.Designation,
                NationalID = employee.NationalID,
                NHIF = employee.NHIF,
                PIN = employee.PIN,
                Phone = employee.Phone,
                PaymentMethod = employee.PaymentMethod,
                StudentLoan = employee.StudentLoan,
                NSSFMember = employee.UnionMember,
                Address = employee.Address,
                City = employee.City,
                PostCode = employee.PostCode,
                ImageUrl = employee.ImageUrl,
            };
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var employee =  _employeeRepository.GetById(id);
            if (employee == null) { return NotFound(); }
            var model = new EmployeeDeleteViewModel()
            {
                Id = employee.Id,
                FullName = employee.FullName
            };
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EmployeeDeleteViewModel model)
        {
      
        await _employeeRepository.DeleteAsync(model.Id);
            return RedirectToAction("Index");
        }





        //public async Task<IActionResult> Delete(int id)
        //{
        //    var clubDetails = await _clubRepository.GetByIdAsync(id);
        //    if (clubDetails == null) return View("Error");
        //    return View(clubDetails);
        //}
        //[HttpPost, ActionName("Delete")]
        //public async Task<IActionResult> DeleteClub(int id)
        //{
        //    var clubDetails = await _clubRepository.GetByIdAsync(id);
        //    if (clubDetails == null) return View("Error");
        //    _clubRepository.Delete(clubDetails);
        //    return RedirectToAction("Index");
        //}
        //public async Task<IActionResult> Detail(int id)
        //{
        //    //Club club = _context.Clubs.Include(a =>a.Address).FirstOrDefault(c => c.Id == id);
        //    Club club = await _clubRepository.GetByIdAsync(id);
        //    return View(club);
        //}

    }
}
