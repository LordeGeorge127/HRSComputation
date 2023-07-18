using HRSCompute.Entity;
using System.ComponentModel.DataAnnotations;

namespace HRSCompute.Models
{
    public class EmployeeCreateViewModel
    {
        public int Id { get; set; }

        public string EmployeeNo { get; set; }
        [Required(ErrorMessage = "First Name is Required"), StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z][a-zA-Z "" '\s-]*$")]
        public string FirstName { get; set; }
        [StringLength(50), Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "First Name is Required"), StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z][a-zA-Z "" '\s-]*$"), Display(Name = "Last Name")]

        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + (string.IsNullOrEmpty(MiddleName) ? "" : (" " + (char?)MiddleName[0] + " .").ToUpper()) + LastName;
            }
        }
        public Gender Gender { get; set; }
        public IFormFile ImageUrl { get; set; }
        [DataType(DataType.Date), Display(Name = "DOB")]
        public DateTime DOB { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Joined")]
        public DateTime DateJoined { get; set; } = DateTime.UtcNow;
        public string Phone { get; set; }
        [Required(ErrorMessage = "Job Role is Required"), StringLength(100)]
        public string Designation { get; set; }
        public string Email { get; set; }
        public int NationalID { get; set; }
        public string PIN { get; set; }//National insurance No
        public string NHIF { get; set; }
        [Display(Name = "Payment Method")]
        public PaymentMethod PaymentMethod { get; set; }
        [Display(Name = "Student Loan")]
        public StudentLoan StudentLoan { get; set; }
        [Display(Name = "Union Member")]
        public NSSFMember UnionMember { get; set; }
        //National housing fund 3%
        public string NHF { get; set; }
        //NATION SOCIAL SECURITY FUND 

        public string Address { get; set; }
        public string City { get; set; }
        [Display(Name = "Postal Code")]

        public string PostCode { get; set; }


        public ICollection<PaymentRecord> PaymentRecords { get; set; }
    }
}
