using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace UoUWebApp.Models
{
    [Table("RoomAllocations")]
    public class RoomAllocationModel
    {
        [Key]
        public long AllocationId { get; set; }

        [Required(ErrorMessage = "You have to select Department"),
            DisplayName("Department:")]
        [NotMapped]
        public int CourseDeptId { get; set; }

        [Required(ErrorMessage = "You have to select a course"),
            DisplayName("Course:")]
        public int RoomAllocationCourseId { get; set; }

        [ForeignKey("RoomAllocationCourseId")]
        public virtual CourseModel CourseModel { get; set; }

        [Required(ErrorMessage = "You have to select a room"),
            DisplayName("Room:")]
        public int RoomAllocationRoomId { get; set; }

        [ForeignKey("RoomAllocationRoomId")]
        public RoomModel RoomModel { get; set; }

        [Required(ErrorMessage = "You have to select a week day"),
            DisplayName("Week Day:")]
        public int RoomAllocationDayId { get; set; }

        [ForeignKey("RoomAllocationDayId")]
        public WeekDayModel WeekDayModel { get; set; }
        
        [Required(ErrorMessage = "You have to specify start time"),
            DisplayName("From:"),
            Column(TypeName = "varchar")]
        public string FromTime { get; set; }

        [Required(ErrorMessage = "You have to specify end time"),
            DisplayName("To:"),
            Column(TypeName = "varchar")]
        [Remote("IsTimeConflicts", "course", HttpMethod = "POST", ErrorMessage = "Times overlapping with self/other course schedule.", AdditionalFields = "RoomAllocationDayId, RoomAllocationRoomId, FromTime, ToTime")]
        public string ToTime { get; set; }

        [Display(AutoGenerateField = false)]
        public int RecordStatus { get; set; }
    }
}