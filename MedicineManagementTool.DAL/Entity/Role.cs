using System.ComponentModel.DataAnnotations;

namespace MedicineManagementTool.DAL.Entity
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
