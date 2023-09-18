using System.ComponentModel.DataAnnotations;

namespace WebVWweek7.Models
{
    public class Collection
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime DateOfCollecting { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Item> Items { get; set; }

    }
}
