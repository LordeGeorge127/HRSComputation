using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSCompute.Entity
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string EmployeeNo { get; set; }
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string MiddleName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        public string FullName { get; set; }
        public Gender Gender { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DateJoined { get; set; }
        public string Phone { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public int NationalID { get; set; }
        public string PIN { get; set; }//National insurance No
        public string NHIF { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public StudentLoan StudentLoan { get; set; }
        //NATION SOCIAL SECURITY FUND 
        public NSSFMember UnionMember { get; set; }
        //National housing fund 3%
        public string NHF { get; set; }      
        public string Address  { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public ICollection<PaymentRecord> PaymentRecords { get; set; }
    }
}
