namespace united_movers_api.Models
{
    public class EmployeeRoleMapping
    {
        public int MappingID { get; set; }  
        public int RoleID { get; set; }
        public string RoleName { get; set; }

        public string Comments { get; set; }

        public bool IsActive { get; set; }

        public int EmployeeID { get; set; }

        public bool IsReadOnly { get; set; }

        public bool IsReadWrite { get; set; }

    }
}
