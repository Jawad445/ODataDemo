namespace ODataOrder.Data
{
    public class Customer
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Country { get; set; }
        public List<Order> orders { get; set; }

    }
}
