using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UoUWebApp.Models
{
    [Table("Designations")]
    public class DesignationModel
    {
        [Key]
        public int DesignationId { get; set; }

        [Required(ErrorMessage = "You have to specify designation"),
            StringLength(80, ErrorMessage = "Designation can't be empty"),
            Column(TypeName = "varchar")]
        public string Designation { get; set; }
    }
}