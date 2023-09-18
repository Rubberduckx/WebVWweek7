using System.ComponentModel.DataAnnotations;

namespace WebVWweek7.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Sort { get; set; }
        public virtual ICollection<Collection> Collections { get; set; }
    }
}
