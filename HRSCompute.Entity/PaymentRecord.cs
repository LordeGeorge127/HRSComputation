using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSCompute.Entity
{
    public class PaymentRecord
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public string FullName { get; set; }
        public string NiNo { get; set; }

        public DateTime PayDate { get; set; }
        public string PayMonth { get; set; }
        [ForeignKey("TaxYear")]
        public int TaxYearId { get; set; }
        public TaxYear TaxYear { get; set; }
        public string TaxCode { get; set; }
        [Column(TypeName = "money")]
        public decimal HourlyRate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal HoursWorked { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ContractualHours { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal OverTimeHours { get; set; }
        [Column(TypeName = "money")]
        public decimal ContractualEarnings { get; set; }
        [Column(TypeName = "money")]
        public decimal OverTimeEarnings { get; set; }
        [Column(TypeName = "money")]
        public decimal TaxPAYE { get; set; }
        [Column(TypeName = "money")]
        public decimal NHIFContribution { get; set; }
        [Column(TypeName = "money")]
        public decimal? NSSFFees { get; set; }
        [Column(TypeName = "money")]
        public Nullable<decimal> HELB { get; set; }
        [Column(TypeName = "money")]
        public decimal TotalEarnings { get; set; }
        [Column(TypeName = "money")]
        public decimal TotalDeductions { get; set; }
        [Column(TypeName = "money")]
        public decimal NetPayment { get; set; }




    }
}
