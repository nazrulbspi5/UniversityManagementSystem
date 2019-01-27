using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UoUWebApp.Models
{
    [Table("Semesters")]
    public class SemesterModel
    {
        [Key]
        public int SemesterId { get; set; }

        [Required, 
            StringLength(11, MinimumLength = 11, ErrorMessage = "Semester name should like 'SEMESTER-xx'"),
            Column(TypeName = "varchar"),
            Index(IsUnique = true)]
        public string Semester { get; set; }
    }
}