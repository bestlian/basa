namespace BasaProject.Outputs
{
    public class AuthenticateResponse
    {
        public Guid UserID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int? RoleID { get; set; }
        public string RoleName { get; set; }
        public string BearerToken { get; set; }
        public string RefreshToken { get; set; }


        public AuthenticateResponse(UserResponse user, string token, string refreshToken)
        {
            UserID = user.UserID;
            Name = user?.Name;
            RoleID = user?.RoleID;
            RoleName = user?.RoleName;
            BearerToken = token;
            RefreshToken = refreshToken;
        }
    }

}

