namespace WebVWweek7.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public int Price { get; set; }
        public int Worth { get; set; }
        public DateTime BuyDate { get; set; }
        public int CollectionId { get; set; }
        public Collection Collection { get; set; }
    }
}
