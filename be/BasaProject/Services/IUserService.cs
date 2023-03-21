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

public interface IUserService
{
    AuthenticateResponse? Authenticate(AuthenticateRequest model, string? ipAddress);
    // AuthenticateResponse RefreshToken(string token, string ipAddress, string from);
    IEnumerable<UserResponse> GetAll();
    UserResponse? GetById(string id);
    // bool RevokeToken(string token, string ipAddress);
}

public class UserServices : IUserService
{
    // users hardcoded for simplicity, store in a db with hashed passwords in production applications
    private List<UserResponse> _users = new List<UserResponse>
    {
        new UserResponse { UserID = "1", Email = "test.user@abc.co", Name = "Tes User", RoleID = 1, RoleName = "Tes Role", Password = "123" }
    };

    private readonly AppSettings _appSettings;

    public UserServices(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    public AuthenticateResponse? Authenticate(AuthenticateRequest model, string? ipAddress)
    {
        var user = _users.SingleOrDefault(x => x.Email == model.Email && x.Password == model.Password);

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
            RoleName = user.RoleName
        };
        //var tok = GetToken(config);
        var jwtToken = generateJwtToken(resp);

        return new AuthenticateResponse(resp, jwtToken, jwtToken);
    }

    public IEnumerable<UserResponse> GetAll()
    {
        return _users;
    }

    public UserResponse? GetById(string id)
    {
        return _users.FirstOrDefault(x => x.UserID == id);
    }

    private string generateJwtToken(UserResponse val)
    {
        // generate token that is valid for 1 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", val.UserID), new Claim(ClaimTypes.Role, val.RoleID.ToString()) }),
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