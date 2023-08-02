using HRSCompute.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSCompute.Services.Interfaces
{
    public interface IPayComputationRepository
    {
        Task CreateAsync(PaymentRecord paymentRecord);
        TaxYear GetTaxYearById(int id);
        PaymentRecord GetById(int id);
        IEnumerable<PaymentRecord> GetAll();
        IEnumerable<SelectListItem> GetAllTaxYear();
        decimal OverTimeHours(decimal hoursWorked, decimal contractualHours);
        decimal ContractualEarnings(decimal hoursWorked, decimal hourlyRate, decimal contractualHours);
        decimal OverTimeRate(decimal hourlyRate);
        decimal OverTimeEarnings(decimal overtimeHours, decimal overtimeRate);
        decimal TotalEarnings(decimal overtimeEarnings, decimal contractualEarnings);
        decimal TotalDeductions(decimal Tax, decimal NHIFC, decimal studentLoanPayment, decimal NSSFFees);
        decimal NetPay(decimal totalDeductions, decimal totalEarnings);

    }
}
