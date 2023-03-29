using BasaProject.Models;
using BasaProject.Outputs;
using Microsoft.EntityFrameworkCore;

namespace BasaProject.Helpers
{
    public class UserHelper
    {
        // get by email
        public static MsUser GetByEmail(string email, DataContext _db)
        {
            var user = _db.MsUsers.Where(x => x.Email.ToLower().Trim() == email.ToLower().Trim() && x.IsDeleted == false)
                .Include(x => x.Role)
                .Include(x => x.RefreshTokens.OrderByDescending(y => y.Expires).Take(3))
                .FirstOrDefault();

            if (user == null) return null;

            return user;
        }

        // get all
        public static IList<UserResponse> GetAll(DataContext _db)
        {
            var res = _db.MsUsers.Where(x => x.IsDeleted == false)
                .Include(x => x.Role)
                .Select(x => new UserResponse
                {
                    UserID = x.UserID,
                    Email = x.Email,
                    RoleID = x.RoleID,
                    RoleName = x.Role.RoleName,
                    Password = x.Password,
                    Name = x.Name
                })
                .ToList();

            return res;
        }

        // get by id
        public static UserResponse Find(Guid id, DataContext _db)
        {
            var user = _db.MsUsers.Where(x => x.UserID == id && x.IsDeleted == false)
                .Include(x => x.Role)
                .Select(x => new UserResponse
                {
                    UserID = x.UserID,
                    Email = x.Email,
                    RoleID = x.RoleID,
                    RoleName = x.Role.RoleName,
                    Password = x.Password,
                    Name = x.Name
                })
                .FirstOrDefault();

            if (user == null) return null;

            return user;
        }

        // get user by refreshToken
        public static MsUser GetUserByRefreshToken(string token, DataContext _db)
        {
            var res = _db.MsUsers.Where(u => u.RefreshTokens.Any(t => t.Token == token))
                .Include(x => x.RefreshTokens.OrderByDescending(y => y.Expires).Take(3))
                .Include(x => x.Role)
                .FirstOrDefault();

            return res;
        }

        // update refreshToken
        public static bool UpdateRefreshToken(TrUserRefreshToken mrt, DataContext _db)
        {
            try
            {
                _db.Entry(mrt).State = EntityState.Modified;
                _db.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                ErrorLog.ErrorException(ex, mrt.UserID, _db);
                return false;
            }
        }
    }
}