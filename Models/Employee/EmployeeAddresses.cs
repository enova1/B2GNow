using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_CORE.Models.Employee
{
    public class EmployeeAddresses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressId { get; set; }

        [Required] public required string Address1{ get; set; }

        public string Address2 { get; set; }
        
        [Required] public required string City { get; set; }

        public string State { get; set; }

        [Required] public required string ZipCode { get; set; }

        // Foreign Key
        public int EmployeeId { get; set; }

        // Navigation property
        [ForeignKey("EmployeeId")]
        public Employees Employees { get; set; }
    }
}
