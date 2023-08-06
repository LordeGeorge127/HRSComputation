using HRSCompute.Entity;
using System.ComponentModel.DataAnnotations;

namespace HRSCompute.Models
{
    public class PaymentRecordCreateViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Full Name")]
        public int EmployeeId { get; set; }
        //public Employee Employee { get; set; }
        //public string FullName { get; set; }
        //NATIONAL INSURANCE NUMBER /NHIF
        //public string NiNo { get; set; }
        [DataType(DataType.Date), Display(Name = "Pay Date")]
        public DateTime PayDate { get; set; } = DateTime.Now;
        [Display(Name = "Pay Month")]
        public string PayMonth { get; set; }
        [Display(Name = "TaxYear")]
        public int TaxYearId { get; set; }
        //public TaxYear TaxYear { get; set; }
        public string TaxCode { get; set; } = "23/24";
         [Display(Name = "Hourly Rate")]
        public decimal HourlyRate { get; set; }
        [Display(Name = "Hours Worked")]
        public decimal HoursWorked { get; set; }
        [Display(Name = "Contractual Hours")]
        public decimal ContractualHours { get; set; } = 160m;
        //public decimal OverTimeHours { get; set; }
        //public decimal ContractualEarnings { get; set; }
        //public decimal OverTimeEarnings { get; set; }
        //public decimal Tax { get; set; }
        ////NATIONAL INSURANCE CONTRIBUTION
        //public decimal NIC { get; set; }
        ////some employees are not not in unions
        //public decimal? UnionFee { get; set; }
        ////SLC STUDENT LOAN COMPANY
        //public Nullable<decimal> HELB { get; set; }
        //public decimal TotalEarnings { get; set; }
        //public decimal TotalDeduction { get; set; }
        //public decimal NetPayment { get; set; }
    }
}
