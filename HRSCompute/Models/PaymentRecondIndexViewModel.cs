using HRSCompute.Entity;
using System.ComponentModel.DataAnnotations;

namespace HRSCompute.Models
{
    public class PaymentRecondIndexViewModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set;}
        public Employee Employee { get; set; }
        [Display(Name ="Name")]
        public string FullName { get; set; }
        [Display(Name = "PayDate")]
        public DateTime PayDate { get; set; }
        [Display(Name = "Month")]
        public string Paymonth { get; set; }
        public int TaxYearId { get; set; }
        public string Year  { get; set; }
        [Display(Name = "Total Earnings")]
        public decimal TotalEarnings { get; set; }
        [Display(Name = "Total Deductions")]
        public decimal TotalDeductions { get; set; }
        public decimal NetPayment { get; set; }


    }
}
