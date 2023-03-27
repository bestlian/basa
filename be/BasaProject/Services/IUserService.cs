namespace BasaProject.Services;

using System;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BasaProject.Outputs;
using BasaProject.Models;
using BCHash = BCrypt.Net.BCrypt;
using BasaProject.Helpers;

public interface IUserService
{
    AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress);
    // AuthenticateResponse RefreshToken(string token, string ipAddress, string from);
    IEnumerable<UserResponse> GetAll();
    UserResponse GetById(Guid id);
    // bool RevokeToken(string token, string ipAddress);
}

public class UserServices : IUserService
{
    private readonly AppSettings _appSettings;
    private readonly DataContext _db;

    public UserServices(IOptions<AppSettings> appSettings, DataContext dataContext)
    {
        _appSettings = appSettings.Value;
        _db = dataContext;
    }

    public AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress)
    {
        var user = UserHelper.GetByEmail(model.Email, _db);

        // return null if user not found
        if (user == null) return null;
        if (!BCHash.Verify(model.Password, user.Password)) return null;
        // var oldRefreshToken = user.RefreshTokens.Where(x => x.IsActive == true && x.CreatedByIp == ipAddress).FirstOrDefault();

        // if (oldRefreshToken != null)
        // {
        //     var result = RefreshToken(oldRefreshToken.Token, ipAddress, "newLogin", config);
        //     return result;
        // }

        // authentication successful so generate jwt token
        // var refreshToken = generateRefreshToken(user.UserID, user.RoleID, ipAddress);
        // refreshToken.UserID = user.UserID;
        // refreshToken.UserIn = user.UserID;

        var resp = new UserResponse()
        {
            UserID = user.UserID,
            Email = user.Email,
            Name = user.Name,
            RoleID = user.RoleID,
            RoleName = user.Role.RoleName
        };
        //var tok = GetToken(config);
        var jwtToken = generateJwtToken(resp);

        return new AuthenticateResponse(resp, jwtToken, jwtToken);
    }

    public IEnumerable<UserResponse> GetAll()
    {
        return UserHelper.GetAll(_db);
    }

    public UserResponse GetById(Guid id)
    {
        return UserHelper.Find(id, _db);
    }

    private string generateJwtToken(UserResponse val)
    {
        // generate token that is valid for 1 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", val.UserID.ToString()), new Claim(ClaimTypes.Role, val.RoleID.ToString()) }),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    // private MsUserRefreshToken generateRefreshToken(string id, int? roleID, string ipAddress)
    // {
    //     using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
    //     {
    //         var randomBytes = new byte[64];
    //         rngCryptoServiceProvider.GetBytes(randomBytes);

    //         var tokenHandler = new JwtSecurityTokenHandler();
    //         var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
    //         var tokenDescriptor = new SecurityTokenDescriptor
    //         {
    //             Subject = new ClaimsIdentity(new[] { new Claim("id", id), new Claim(ClaimTypes.Role, roleID.ToString()) }),
    //             Expires = DateTime.Now.AddDays(7),
    //             SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    //         };
    //         var token = tokenHandler.CreateToken(tokenDescriptor);

    //         return new MsUserRefreshToken
    //         {
    //             Token = tokenHandler.WriteToken(token),
    //             Expires = DateTime.Now.AddDays(7),
    //             Created = DateTime.Now,
    //             CreatedByIp = ipAddress
    //         };
    //     }
    // }
}