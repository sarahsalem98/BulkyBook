using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [DisplayName("Dispaly Name")]
        [Range(1, 100, ErrorMessage = "value of this field should be between 1 and 100!")]
        public int DisplayOrder { get; set; }
        public DateTime CreateedDateTime { get; set; } = DateTime.Now;
    }
}
