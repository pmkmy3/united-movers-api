namespace united_movers_api.Models
{
    public class Vendor
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }

        public bool IsActive { get; set; }

    }
}
