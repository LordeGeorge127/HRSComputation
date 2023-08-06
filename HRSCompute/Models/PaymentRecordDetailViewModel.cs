using HRSCompute.Entity;
using System.ComponentModel.DataAnnotations;

namespace HRSCompute.Models
{
    public class PaymentRecordDetailViewModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        //NATIONAL INSURANCE NUMBER /NHIF
        public string NiNo { get; set; }
        [DataType(DataType.Date), Display(Name = "Pay Date")]
        public DateTime PayDate { get; set; }
        [Display(Name = "Pay Month")]
        public string PayMonth { get; set; }
        public string Year { get; set; }
        [Display(Name = "TaxYear")]
        public int TaxYearId { get; set; }
        public TaxYear TaxYear { get; set; }
        [Display(Name = "Tax Code")]

        public string TaxCode { get; set; }
        [Display(Name = "Hourly Rate")]
        public decimal HourlyRate { get; set; }
        [Display(Name = "Hours Worked")]
        public decimal HoursWorked { get; set; }
        [Display(Name = "Contractual Hours")]
        public decimal ContractualHours { get; set; } = 140m;
        [Display(Name = "Overtime Hours")]
        public decimal OverTimeHours { get; set; }
        [Display(Name = "Overtime Rate")]
        public decimal OverTimeRate { get; set; }
        [Display(Name = "Contractual Earnings")]
        public decimal ContractualEarnings { get; set; }
        [Display(Name = "Overtime Earnings")]
        public decimal OverTimeEarnings { get; set; }
        public decimal Tax { get; set; }
        //NATIONAL INSURANCE CONTRIBUTION
        [Display(Name = "NHIF PAYMENT")]

        public decimal NIC { get; set; }
        //some employees are not not in unions
        [Display(Name = "NSSF FEES")]
        public decimal? UnionFee { get; set; }
        //SLC STUDENT LOAN COMPANY
        public Nullable<decimal> HELB { get; set; }
        [Display(Name = "Total Earnings")]
        public decimal TotalEarnings { get; set; }
        [Display(Name = "Total Deductions")]
        public decimal TotalDeduction { get; set; }
        [Display(Name = "Net Payments")]

        public decimal NetPayment { get; set; }
    }
}
