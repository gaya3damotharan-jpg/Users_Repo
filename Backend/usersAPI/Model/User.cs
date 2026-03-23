using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace usersAPI.Model
{
    [Table("tblUsers")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Range(0, 120)]
        public int Age { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required, StringLength(10, MinimumLength = 4)]
        public string Pin { get; set; }

        // Constructor to satisfy nullable reference types
        public User()
        {
            Name = string.Empty;
            City = string.Empty;
            State = string.Empty;
            Pin = string.Empty;
        }
    }
}
