using System.ComponentModel.DataAnnotations;

namespace BasaProject.Outputs
{
    public class ClientAppResponse
    {
        public Guid CLIENT_ID { get; set; }
        public string CLIENT_SECRET { get; set; }
    }

    public class ClientRequest
    {
        [Required]
        public Guid CLIENT_ID { get; set; }

        [Required]
        public string CLIENT_SECRET { get; set; }
    }
    public class ClientResponse
    {
        public Guid CLIENT_ID { get; set; }
        public int? RoleID { get; set; }
    }

    public class ClientAuthResponse
    {
        public Guid ClientID { get; set; }
        public int? RoleID { get; set; }
        public string BearerToken { get; set; }
        public string RefreshToken { get; set; }


        public ClientAuthResponse(ClientResponse client, string token, string refreshToken)
        {
            ClientID = client.CLIENT_ID;
            RoleID = client.RoleID;
            BearerToken = token;
            RefreshToken = refreshToken;
        }
    }
}