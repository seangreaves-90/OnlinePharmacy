namespace ScriptEase.OnlinePharmacy.Data.DataModels
{
    public class DrugOrder
    {
        public int OrderId { get; set; }
        public int DrugId { get; set; }
        public int Quantity { get; set; }
        public Order Order { get; set; }
        public Drug Drug { get; set; }

        public DrugOrder(int orderId, int drugId, int quantity, Order order, Drug drug)
        {
            OrderId = orderId;
            DrugId = drugId;
            Quantity = quantity;
            Order = order;
            Drug = drug;
        }
    }

}
