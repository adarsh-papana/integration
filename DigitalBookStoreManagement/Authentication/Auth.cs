using DigitalBookStoreManagement.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DigitalBookStoreManagement.Authentication
{
   
        public class Auth : IAuth
        {
            private readonly UserContext _userContext;
            private readonly string key;
            public Auth(string key, UserContext userContext)
            {
                this.key = key;
                this._userContext = userContext;
            }


            public string Authentication(string email, string password)
            {
                var user = _userContext.Users.SingleOrDefault(u => u.Email == email && u.Password == password);
                if (user == null || user.Email!=email || user.Password!=password)
                {
                    return "invalid credential";
                }
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes(key);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role,user.Role)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
        }
    
}
