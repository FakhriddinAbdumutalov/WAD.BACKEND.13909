using System.ComponentModel.DataAnnotations;

namespace WAD.BACKEND._13909.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "AgeRestriction should be a non-negative integer")]
        public int? AgeRestriction { get; set; }
    }
}
