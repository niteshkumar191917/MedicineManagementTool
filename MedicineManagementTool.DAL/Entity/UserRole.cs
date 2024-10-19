﻿namespace MedicineManagementTool.DAL.Entity
{
    public class UserRole
    {
        
        public virtual User User { get; set; }
        public int UserId { get; set; }
                
        public virtual Role Role { get; set; }
        public int RoleId { get; set; }
    }
}
