using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UoUWebApp.Models
{
    [Table("Rooms")]
    public class RoomModel
    {
        [Key]
        public int RoomId { get; set; }

        [Required(ErrorMessage = "You have to specify room no"),
            StringLength(3, MinimumLength = 3, ErrorMessage = "Room no must be 3 characters long"),
            Column(TypeName = "char"), 
            Index(IsUnique = true)]
        public string Room { get; set; }
    }
}