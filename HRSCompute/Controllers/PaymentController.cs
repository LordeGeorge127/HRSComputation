using HRSCompute.Entity;
using HRSCompute.Models;
using HRSCompute.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRSCompute.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPayComputationRepository _payComputation;
        private readonly ITaxRepository _taxRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly INHIFContributionRepository _nHIFContribution;
        private decimal overtimeHrs;
        private decimal overtimeEarnings;
        private decimal contractualEarnings;
        private decimal totalEarnings;

        public PaymentController(
            IPayComputationRepository payComputation,
            ITaxRepository taxRepository,
            IEmployeeRepository employeeRepository,
            INHIFContributionRepository nHIFContribution
            )
        {
            _payComputation = payComputation;
            _taxRepository = taxRepository;
            _employeeRepository = employeeRepository;
             _nHIFContribution = nHIFContribution;
            ;
        }
        public IActionResult Index()
        {
            var paymentRecords = _payComputation.GetAll().Select(pay => new PaymentRecondIndexViewModel
            {
                Id = pay.Id,
                EmployeeId= pay.EmployeeId,
                FullName = pay.FullName,
                PayDate = pay.PayDate,
                Paymonth = pay.PayMonth,
                TaxYearId = pay.TaxYearId,
                Year = _payComputation.GetTaxYearById(pay.TaxYearId).YearofTax,
                 TotalEarnings = pay.TotalEarnings,
                 TotalDeductions = pay.TotalDeductions,
                 NetPayment = pay.NetPayment,
                 Employee = pay.Employee,

            });
               
            return View(paymentRecords);
        }
        public IActionResult Create()
        {
            ViewBag.Employees = _employeeRepository.GetAllEmployeesForPayroll();
            ViewBag.taxYears = _payComputation.GetAllTaxYear();
            var model = new PaymentRecordCreateViewModel();
        }
        [HttpPost]
        public IActionResult Create(PaymentRecordCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var payRecord = new PaymentRecord()
                {
                    Id = model.Id,
                    EmployeeId = model.EmployeeId,
                    FullName = _employeeRepository.GetById(model.EmployeeId).FullName,
                     NiNo = _employeeRepository.GetById(model.EmployeeId).NHIF,
                     PayDate = model.PayDate,
                     PayMonth= model.PayMonth,
                     TaxYearId = model.TaxYearId,
                    TaxCode = model.TaxCode,
                    HourlyRate = model.HourlyRate,
                    HoursWorked = model.HoursWorked,
                    ContractualHours = model.ContractualHours,
                    OverTimeHours = overtimeHrs = _payComputation.OverTimeHours(model.HoursWorked,model.ContractualHours),
                    ContractualEarnings = contractualEarnings =  _payComputation.ContractualEarnings(model.ContractualHours, model.HoursWorked,model.HourlyRate),
                    OverTimeEarnings = overtimeEarnings =  _payComputation.OverTimeEarnings(_payComputation.OverTimeRate(model.HourlyRate),overtimeHrs),
                    TotalEarnings = totalEarnings = _payComputation.TotalEarnings(overtimeEarnings, contractualEarnings),
                    TaxPAYE = _taxRepository.TaxAmount(totalEarnings),
                    UnionFee = _employeeRepository.UnionFees(model.EmployeeId),
                    HELB = _employeeRepository.StudenLoanRepayment(model.HELB),
                    NHIFContribution = _nHIFContribution.CalculateNHIFContribution(totalEarnings),

                }
            }
        }
    }
}
