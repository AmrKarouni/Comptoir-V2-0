using System.ComponentModel.DataAnnotations;

namespace COMPTOIR.Models.AppModels
{
    public class Tax
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters long", MinimumLength = 3)]
        [Display(Name = "Tax Name")]
        public string? Name { get; set; }
        [Required]
        public string? Type { get; set; }
        public double Rate { get; set; }
        public virtual ICollection<ChannelCategory>? ChannelCategories { get; set; }
        public virtual ICollection<Ticket>? Tickets { get; set; }
        public bool IsDeleted { get; set; }
    }
}
