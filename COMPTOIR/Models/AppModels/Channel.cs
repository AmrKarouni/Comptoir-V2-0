using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COMPTOIR.Models.AppModels
{
    public class ChannelCategory
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters long", MinimumLength = 3)]
        [Display(Name = "Category Name")]
        public string? Name { get; set; }
        [ForeignKey("Place")]
        public int? PlaceId { get; set; }
        public bool IsMiniPos { get; set; } = false;
        public virtual Place? Place { get; set; }
        public virtual ICollection<Channel>? Channels { get; set; }
        public virtual ICollection<Tax>? Taxes { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class Channel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters long", MinimumLength = 3)]
        [Display(Name = "Channel Name")]
        public string? Name { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual ChannelCategory? Category { get; set; }
        public virtual ICollection<Ticket>? Tickets { get; set; }
        public int? PositionX { get; set; }
        public int? PositionY { get; set; }
        public int? Rotation { get; set; }
        public int? Seats { get; set; }
        public bool IsAnonymous { get; set; } = true;
        public bool IsMultiple { get; set; } = true;
        public bool IsDeleted { get; set; }
    }
}
