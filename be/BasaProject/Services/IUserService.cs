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
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

public interface IUserService
{
    AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress);
    ClientAuthResponse ClientAuthenticate(ClientRequest model, string ipAddress);
    AuthenticateResponse RefreshToken(string token, string ipAddress);
    ClientAuthResponse ClientRefreshToken(string token, string ipAddress);
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
        var oldRefreshToken = user.RefreshTokens?.Where(x => x.IsActive == true && x.CreatedByIp == ipAddress).FirstOrDefault();

        if (oldRefreshToken != null)
        {
            var result = RefreshToken(oldRefreshToken.Token, ipAddress);
            return result;
        }

        // authentication successful so generate jwt token
        var refreshToken = generateRefreshToken(user.UserID, user.RoleID, ipAddress);
        refreshToken.UserID = user.UserID;
        refreshToken.UserIn = user.UserID;

        var resp = new UserResponse()
        {
            UserID = user.UserID,
            Email = user.Email,
            Name = user.Name,
            RoleID = user.RoleID,
            RoleName = user.Role.RoleName
        };

        var jwtToken = generateJwtToken(resp);

        _db.Add(refreshToken);
        _db.SaveChanges();

        return new AuthenticateResponse(resp, jwtToken, refreshToken.Token);
    }

    public ClientAuthResponse ClientAuthenticate(ClientRequest model, string ipAddress)
    {
        var client = ClientAppHelper.GetByClientID(model.CLIENT_ID, _db);

        // return null if user not found
        if (client == null) return null;
        if (!BCHash.Verify(model.CLIENT_SECRET, client.ClientSecret)) return null;
        var oldRefreshToken = client.RefreshTokens?.Where(x => x.IsActive == true && x.CreatedByIp == ipAddress).FirstOrDefault();

        if (oldRefreshToken != null)
        {
            var result = ClientRefreshToken(oldRefreshToken.Token, ipAddress);
            return result;
        }

        // authentication successful so generate jwt token
        var refreshToken = generateRefreshToken(client.User.UserID, client.User.RoleID, ipAddress);
        refreshToken.ClientID = client.ClientID;
        refreshToken.UserIn = client.User.UserID;

        var resp = new ClientResponse()
        {
            CLIENT_ID = client.ClientID,
            RoleID = client.User.RoleID
        };

        var jwtToken = generateClientJwtToken(resp);

        _db.Add(refreshToken);
        _db.SaveChanges();

        return new ClientAuthResponse(resp, jwtToken, refreshToken.Token);
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

    private TrUserRefreshToken generateRefreshToken(Guid id, int? roleID, string ipAddress)
    {
        using (var rng = RandomNumberGenerator.Create())
        {
            var randomBytes = new byte[64];
            rng.GetBytes(randomBytes);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", id.ToString()), new Claim(ClaimTypes.Role, roleID.ToString()) }),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new TrUserRefreshToken
            {
                Token = tokenHandler.WriteToken(token),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now,
                CreatedByIp = ipAddress
            };
        }
    }

    public AuthenticateResponse RefreshToken(string token, string ipAddress)
    {
        var user = UserHelper.GetUserByRefreshToken(token, _db);

        // return null if no user found with token
        if (user == null) return null;

        var refreshToken = user.RefreshTokens.Where(x => x.IsActive == true && x.CreatedByIp == ipAddress).FirstOrDefault();

        // return null if token is no longer active
        if (!refreshToken.IsActive) return null;

        var date = DateTime.Now;
        var startdate = date.AddMinutes(-1);
        if (refreshToken.DateIn > startdate && refreshToken.DateIn < date) return null;

        // replace old refresh token with a new one and save
        var newRefreshToken = generateRefreshToken(user.UserID, user.RoleID, ipAddress);

        //revoke old token
        refreshToken.Revoked = DateTime.UtcNow;
        refreshToken.RevokedByIp = ipAddress;
        refreshToken.ReplacedByToken = newRefreshToken.Token;
        refreshToken.UserUp = user.UserID;
        refreshToken.DateUp = DateTime.Now;

        //save new refresh token
        user.RefreshTokens.Add(newRefreshToken);
        newRefreshToken.UserID = user.UserID;
        newRefreshToken.UserIn = user.UserID;
        _db.Add(newRefreshToken);
        _db.SaveChanges();


        var resp = new UserResponse()
        {
            UserID = user.UserID,
            Email = user.Email,
            Name = user.Name,
            RoleID = user.RoleID,
            RoleName = user.Role.RoleName
        };

        // generate new jwt
        var jwtToken = generateJwtToken(resp);

        return new AuthenticateResponse(resp, jwtToken, newRefreshToken.Token);
    }

    // Revoke Token
    public bool RevokeToken(string token, string ipAddress)
    {
        var user = UserHelper.GetUserByRefreshToken(token, _db);

        // return false if no user found with token
        if (user == null) return false;

        var refreshToken = user.RefreshTokens.Where(x => x.Token == token).FirstOrDefault();

        // return false if token is not active
        if (!refreshToken.IsActive) return false;

        // revoke token and save
        refreshToken.Revoked = DateTime.UtcNow;
        refreshToken.RevokedByIp = ipAddress;
        _db.SaveChanges();

        //EntityHelper.Update<MsUser>(user);
        //UserHelper.UpdateRefreshToken(refreshToken, _db);


        var tokenHandler = new JwtSecurityTokenHandler();

        return true;
    }

    private string generateClientJwtToken(ClientResponse val)
    {
        // generate token that is valid for 1 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", val.CLIENT_ID.ToString()), new Claim(ClaimTypes.Role, val.RoleID.ToString()) }),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public ClientAuthResponse ClientRefreshToken(string token, string ipAddress)
    {
        var client = ClientAppHelper.GetClientByRefreshToken(token, _db);

        // return null if no user found with token
        if (client == null) return null;

        var refreshToken = client.RefreshTokens.Where(x => x.IsActive == true && x.CreatedByIp == ipAddress).FirstOrDefault();

        // return null if token is no longer active
        if (!refreshToken.IsActive) return null;

        var date = DateTime.Now;
        var startdate = date.AddMinutes(-1);
        if (refreshToken.DateIn > startdate && refreshToken.DateIn < date) return null;

        // replace old refresh token with a new one and save
        var newRefreshToken = generateRefreshToken(client.ClientID, client.User.RoleID, ipAddress);

        //revoke old token
        refreshToken.Revoked = DateTime.UtcNow;
        refreshToken.RevokedByIp = ipAddress;
        refreshToken.ReplacedByToken = newRefreshToken.Token;
        refreshToken.UserUp = client.User.UserID;
        refreshToken.DateUp = DateTime.Now;

        //save new refresh token
        client.RefreshTokens.Add(newRefreshToken);
        newRefreshToken.ClientID = client.ClientID;
        newRefreshToken.UserIn = client.User.UserID;
        _db.Add(newRefreshToken);
        _db.SaveChanges();


        var resp = new ClientResponse()
        {
            CLIENT_ID = client.ClientID,
            RoleID = client.User.RoleID
        };

        // generate new jwt
        var jwtToken = generateClientJwtToken(resp);

        return new ClientAuthResponse(resp, jwtToken, newRefreshToken.Token);
    }
}