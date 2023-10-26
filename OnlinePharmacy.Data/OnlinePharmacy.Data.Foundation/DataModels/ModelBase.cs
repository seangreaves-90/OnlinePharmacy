namespace ScriptEase.OnlinePharmacy.Data.DataModels
{
    public abstract class ModelBase
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastModified { get; set; }
    }
}
