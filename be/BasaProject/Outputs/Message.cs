namespace BasaProject.Outputs
{
    public class Message
    {
        public int Statuscode { get; set; }

        public string Msg { get; set; } = string.Empty;
    }

    public class Roles
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }
}