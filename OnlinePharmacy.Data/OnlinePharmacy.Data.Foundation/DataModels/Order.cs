namespace ScriptEase.OnlinePharmacy.Data.DataModels
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string LastModifiedBy { get; set; }

        public Order(int orderId, int userId, DateTime createdDate, string createdBy, DateTime? lastModified, string lastModifiedBy)
        {
            OrderId = orderId;
            UserId = userId;
            CreatedDate = createdDate;
            CreatedBy = createdBy;
            LastModified = lastModified;
            LastModifiedBy = lastModifiedBy;
        }
    }

}
