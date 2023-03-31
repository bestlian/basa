using BasaProject.Models;
using BasaProject.Outputs;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using BCHash = BCrypt.Net.BCrypt;

namespace BasaProject.Helpers
{
    public class ClientAppHelper
    {
        // GENERATE CLIENT SECRET
        public static ClientAppResponse Generate(Guid id, DataContext _db)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var randomBytes = new byte[64];
                rng.GetBytes(randomBytes);
                string uniq = Convert.ToBase64String(randomBytes);
                var hint = "xxxxxxxx" + uniq.Substring(uniq.Length - 16);

                var client = new TrClientApp()
                {
                    ClientSecret = BCHash.HashPassword(uniq),
                    Hint = hint,
                    UserIn = id
                };

                _db.Add(client);
                _db.SaveChanges();

                var res = new ClientAppResponse()
                {
                    CLIENT_ID = client.ClientID,
                    CLIENT_SECRET = uniq
                };

                return res;
            }
        }

        // get by client id
        public static TrClientApp GetByClientID(Guid clientID, DataContext _db)
        {
            var client = _db.TrClientApps.Where(a => a.ClientID == clientID)
                .Include(x => x.RefreshTokens.OrderByDescending(y => y.Expires).Take(3))
                .Include(x => x.User).ThenInclude(y => y.Role)
                .FirstOrDefault();

            if (client == null) return null;

            return client;
        }

        // get client by refreshToken
        public static TrClientApp GetClientByRefreshToken(string token, DataContext _db)
        {
            var res = _db.TrClientApps.Where(u => u.RefreshTokens.Any(t => t.Token == token))
                .Include(x => x.RefreshTokens.OrderByDescending(y => y.Expires).Take(3))
                .Include(x => x.User).ThenInclude(y => y.Role)
                .FirstOrDefault();

            return res;
        }
    }
}