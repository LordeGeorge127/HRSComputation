﻿using HRSCompute.Entity;
using HRSCompute.Persistence;
using HRSCompute.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSCompute.Services.Repository
{
    public class PayComputationRepository : IPayComputationRepository
    {
        private readonly ApplicationDbContext _context;
        private decimal contractualEarnings;
        private decimal overtimehours;

        public PayComputationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public decimal ContractualEarnings(decimal contractualHours, decimal hoursWorked, decimal hourlyRate)
        {
            if (hoursWorked < contractualHours)
            {
                contractualEarnings = hoursWorked * hourlyRate;
            }
            else
            {
                contractualEarnings = contractualHours * hourlyRate;
            }
            return contractualEarnings;
        }

        public async Task CreateAsync(PaymentRecord paymentRecord)
        {
           await _context.PaymentRecords.AddAsync(paymentRecord);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<PaymentRecord> GetAll() =>
            _context.PaymentRecords.OrderBy(p => p.EmployeeId);   
        

        public IEnumerable<SelectListItem> GetAllTaxYear()
        {
            var allTaxYears = _context.TaxYears.Select(taxYears => new SelectListItem
            {
                Text = taxYears.YearofTax,
                Value = taxYears.Id.ToString(),
            });
            return allTaxYears;
        }

        public PaymentRecord GetById(int id) => _context.PaymentRecords.Where(p => p.Id == id).FirstOrDefault();

        public TaxYear GetTaxYearById(int id) =>
            _context.TaxYears.Where(year => year.Id == id).FirstOrDefault();    
        

        public decimal NetPay(decimal totalEarnings, decimal totalDeductions) => totalEarnings - totalDeductions;

        public decimal OverTimeEarnings(decimal overtimeRate, decimal overtimeHours) => overtimeHours*overtimeRate;

        public decimal OverTimeHours(decimal hoursWorked, decimal contractualHours)
        {
            if (hoursWorked < contractualHours) { overtimehours = 0.00m; }
            else
            {
                overtimehours = hoursWorked - contractualHours;
            }
            return overtimehours;
        }

        public decimal OverTimeRate(decimal hourlyRate)
        {
            return hourlyRate * 1.5m;
        }

        public decimal TotalDeductions(decimal tax, decimal NHIFC, decimal studentLoanPayment, decimal NSSFFees) =>
            tax + NHIFC + studentLoanPayment + NSSFFees;


        public decimal TotalEarnings(decimal overtimeEarnings, decimal contractualEarnings)
        => overtimeEarnings + contractualEarnings;
        //public decimal TotalDeductions(decimal tax, decimal NHIFC, decimal studentLoanPayment, decimal NSSFFees) =>
        //    tax + NHIFC + studentLoanPayment + NSSFFees;

        //public decimal TotalEarnings(decimal overtimeEarnings, decimal contractualEarnings) => overtimeEarnings + contractualEarnings;
    }
}
