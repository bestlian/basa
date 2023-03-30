namespace BasaProject.Outputs
{
    public class Message
    {
        public int Statuscode { get; set; }

        public string Msg { get; set; }
        public Dictionary<string, string[]> DetailError { get; set; }
    }

    public class Roles
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }
}