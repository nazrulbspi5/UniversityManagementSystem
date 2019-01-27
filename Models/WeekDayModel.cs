using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UoUWebApp.Models
{
    [Table("WeekDays")]
    public class WeekDayModel
    {
        [Key]
        public int DayId { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [StringLength(3, MinimumLength = 3)]
        public string Day { get; set; }
    }
}