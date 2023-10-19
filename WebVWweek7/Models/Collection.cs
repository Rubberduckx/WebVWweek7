using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebVWweek7.Models
{
    public class Collection
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("Date of collecting")]
        public DateTime DateOfCollecting { get; set; }
        [DisplayName("Category Name")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Item> Items { get; set; }

    }
}
