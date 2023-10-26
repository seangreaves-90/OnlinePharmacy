namespace ScriptEase.OnlinePharmacy.Data.DataModels
{
    public class User : ModelBase
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string LastModifiedBy { get; set; }

        public User(int userId, string name, string email, string role, DateTime createdDate, string createdBy, DateTime? lastModified, string lastModifiedBy)
        {
            UserId = userId;
            Name = name;
            Email = email;
            Role = role;
            CreatedDate = createdDate;
            CreatedBy = createdBy;
            LastModified = lastModified;
            LastModifiedBy = lastModifiedBy;
        }
    }
}
