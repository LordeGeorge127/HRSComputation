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
        private decimal totalDeductions;
        private decimal taxPAYE;
        private decimal unionfee;
        private decimal helb;
        private decimal nhif;

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
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(PaymentRecordCreateViewModel model)
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
                    PayMonth = model.PayMonth,
                    TaxYearId = model.TaxYearId,
                    TaxCode = model.TaxCode,
                    HourlyRate = model.HourlyRate,
                    HoursWorked = model.HoursWorked,
                    ContractualHours = model.ContractualHours,
                    OverTimeHours = overtimeHrs = _payComputation.OverTimeHours(model.HoursWorked, model.ContractualHours),
                    ContractualEarnings = contractualEarnings = _payComputation.ContractualEarnings(model.ContractualHours, model.HoursWorked, model.HourlyRate),
                    OverTimeEarnings = overtimeEarnings = _payComputation.OverTimeEarnings(_payComputation.OverTimeRate(model.HourlyRate), overtimeHrs),
                    TotalEarnings = totalEarnings = _payComputation.TotalEarnings(overtimeEarnings, contractualEarnings),
                    TaxPAYE = taxPAYE = _taxRepository.TaxAmount(totalEarnings),
                    NSSFFees = unionfee = _employeeRepository.UnionFees(model.EmployeeId, totalEarnings),
                    HELB = helb = _employeeRepository.StudenLoanRepayment(model.EmployeeId, totalEarnings),
                    NHIFContribution = nhif = _nHIFContribution.CalculateNHIFContribution(totalEarnings),
                    TotalDeductions = totalDeductions =  _payComputation.TotalDeductions(taxPAYE, unionfee, helb, nhif),
                    NetPayment = _payComputation.NetPay(totalEarnings, totalDeductions)
                };
                await _payComputation.CreateAsync(payRecord);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Employees = _employeeRepository.GetAllEmployeesForPayroll();
            ViewBag.taxYears = _payComputation.GetAllTaxYear();
            return View();
        }
        public IActionResult Detail (int id)
        {
            var paymentRecord = _payComputation.GetById(id);
            if (paymentRecord == null) { return NotFound(); }
            var model = new PaymentRecordDetailViewModel()
            {
                Id = paymentRecord.Id,
                EmployeeId = paymentRecord.EmployeeId,
                FullName = paymentRecord.FullName,
                NiNo = paymentRecord.NiNo,
                PayDate = paymentRecord.PayDate,
                PayMonth = paymentRecord.PayMonth,
                TaxYearId = paymentRecord.TaxYearId,
                Year = _payComputation.GetTaxYearById(paymentRecord.TaxYearId).YearofTax,
                TaxCode = paymentRecord.TaxCode,
                HourlyRate = paymentRecord.HourlyRate,
                HoursWorked = paymentRecord.HoursWorked,
                ContractualHours = paymentRecord.ContractualHours,
                OverTimeHours = paymentRecord.OverTimeHours,
                OverTimeRate = _payComputation.OverTimeRate(paymentRecord.HourlyRate),
                OverTimeEarnings = paymentRecord.OverTimeEarnings,
                ContractualEarnings = paymentRecord.ContractualEarnings,
                Tax = paymentRecord.TaxPAYE,
                NIC = paymentRecord.NHIFContribution,
                UnionFee = paymentRecord.NSSFFees,
                HELB = paymentRecord.HELB,
                TotalEarnings = paymentRecord.TotalEarnings,
                TotalDeduction = paymentRecord.TotalDeductions,
                Employee = paymentRecord.Employee,
                TaxYear = paymentRecord.TaxYear,
                NetPayment = paymentRecord.NetPayment,
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Payslip(int id)
        {
            var paymentRecord = _payComputation.GetById(id);
            if (paymentRecord == null) { return NotFound(); }
            var model = new PaymentRecordDetailViewModel()
            {
                Id = paymentRecord.Id,
                EmployeeId = paymentRecord.EmployeeId,
                FullName = paymentRecord.FullName,
                NiNo = paymentRecord.NiNo,
                PayDate = paymentRecord.PayDate,
                PayMonth = paymentRecord.PayMonth,
                TaxYearId = paymentRecord.TaxYearId,
                Year = _payComputation.GetTaxYearById(paymentRecord.TaxYearId).YearofTax,
                TaxCode = paymentRecord.TaxCode,
                HourlyRate = paymentRecord.HourlyRate,
                HoursWorked = paymentRecord.HoursWorked,
                ContractualHours = paymentRecord.ContractualHours,
                OverTimeHours = paymentRecord.OverTimeHours,
                OverTimeRate = _payComputation.OverTimeRate(paymentRecord.HourlyRate),
                ContractualEarnings = paymentRecord.ContractualEarnings,
                Tax = paymentRecord.TaxPAYE,
                NIC = paymentRecord.NHIFContribution,
                UnionFee = paymentRecord.NSSFFees,
                HELB = paymentRecord.HELB,
                TotalEarnings = paymentRecord.TotalEarnings,
                Employee = paymentRecord.Employee,
                TaxYear = paymentRecord.TaxYear,
                NetPayment = paymentRecord.NetPayment,
            };
            return View(model);
        }

    }
}
