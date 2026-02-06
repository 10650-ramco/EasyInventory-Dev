using System.ComponentModel.DataAnnotations;

namespace EasyInventory.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }
}