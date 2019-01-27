using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UoUWebApp.Models
{
    [Table("Grades")]
    public class GradeModel
    {
        [Key]
        public int GradeId { get; set; }

        [Required, StringLength(2, ErrorMessage = "Should not more that 2 characters"),
            Column(TypeName = "varchar"), 
            Index(IsUnique = true)]
        public string Grade { get; set; }
    }
}