using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVC_CORE.Models.Employee
{
    public class EmployeePhones
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhoneNumberId { get; set; }

        //TODO: Add PhoneType Enum
        [Required] public required string PhoneType { get; set; }

        [Required] public required string PhoneNumber { get; set; }

        // Foreign Key
        public int EmployeeId { get; set; }

        // Navigation property
        [ForeignKey("EmployeeId")]
        public Employees Employees { get; set; }
    }
}
