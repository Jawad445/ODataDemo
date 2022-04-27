namespace ODataOrder.Data
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int Revenue { get; set; }
        public Customer customer { get; set; }
        public int customerId { get; set; }
    }
}
