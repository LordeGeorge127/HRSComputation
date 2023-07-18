using HRSCompute.Entity;
using System.ComponentModel.DataAnnotations;

namespace HRSCompute.Models
{
    public class EmployeeEditViewModel
    {
          public int Id { get; set; }
       
        public string EmployeeNo { get; set; }
      
        public string FirstName { get; set; }
  
        public string MiddleName { get; set; }
     
        public string LastName { get; set; }

        public string Gender { get; set; }
        public IFormFile? ImageUrl { get; set; }
        public string Phone { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Joined")]
        
        public DateTime DateJoined { get; set; }
        [DataType(DataType.Date), Display(Name = "DOB")]
        public DateTime DOB { get; set; }
        [Required(ErrorMessage = "Job Role is Required"), StringLength(100)]

        public string Designation { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public int NationalID { get; set; }

        public string NHIF { get; set; }
        [Display(Name = "Payment Method")]
        public PaymentMethod PaymentMethod { get; set; }
        [Display(Name = "Student Loan")]
        public StudentLoan StudentLoan { get; set; }
        [Display(Name = "Union Member")]
        public NSSFMember NSSFMember { get; set; }
        [Required, StringLength(150)]

        public string Address { get; set; }
        [Required, StringLength(50)]

        public string City { get; set; }
        [Display(Name = "Postal Code")]
        public string PostCode { get; set; }
    }
}
