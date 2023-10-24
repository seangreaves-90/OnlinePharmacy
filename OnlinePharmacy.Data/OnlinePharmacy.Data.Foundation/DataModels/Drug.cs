namespace ScriptEase.OnlinePharmacy.Data.DataModels
{
    public class Drug
    {
        public int DrugId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string LastModifiedBy { get; set; }

        public Drug(int drugId, string name, string description, int stock, DateTime createdDate, string createdBy, DateTime? lastModified, string lastModifiedBy)
        {
            DrugId = drugId;
            Name = name;
            Description = description;
            Stock = stock;
            CreatedDate = createdDate;
            CreatedBy = createdBy;
            LastModified = lastModified;
            LastModifiedBy = lastModifiedBy;
        }
    }
}
