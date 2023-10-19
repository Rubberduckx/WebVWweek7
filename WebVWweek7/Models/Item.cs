using System.ComponentModel;

namespace WebVWweek7.Models
{
    public class Item
    {
        public int Id { get; set; }
        [DisplayName ("Item Name")]
        public string ItemName { get; set; }
        [DisplayName("Item Description")]
        public string ItemDescription { get; set; }
        public int Price { get; set; }
        public int Worth { get; set; }
        [DisplayName("Buy Date")]
        public DateTime BuyDate { get; set; }
        [DisplayName("Collection Name")]
        public int CollectionId { get; set; }
        public Collection Collection { get; set; }

        public int Difference => Price - Worth;
    }
}
