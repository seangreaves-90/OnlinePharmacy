using ScriptEase.OnlinePharmacy.Data.Security.Enums;

namespace ScriptEase.OnlinePharmacy.Data.Security.Permissions
{
    public static class UserPermissions
    {
        public static bool CanManageInventory(UserRole role)
        {
            return role is UserRole.Admin or UserRole.Pharmacist;
        }
    }
}
