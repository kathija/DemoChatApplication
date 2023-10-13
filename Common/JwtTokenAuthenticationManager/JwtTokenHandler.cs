using ChatApplication.Model;
using JwtTokenAuthenticationManager.Model;
using JwtTokenAuthenticationManager.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtTokenAuthenticationManager
{
    public class JwtTokenHandler : IJwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "14b12882-0fa3-4074-ba33-36ba3fc0c085-AuthenticationService";
        private const int JWT_TOKEN_VALIDITY_MINS = 20;

        //private List<User> _userList;

        //private IUserRepository _userRepository;
        //public JwtTokenHandler(IUserRepository userRepository)
        //{
        //    //_userRepository = userRepository;
        //    //_userList = _userRepository.GetAll().ToList();
        //}

        public AuthenticationResponse GenerateToken(User model)
        {
            //if (string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrWhiteSpace(model.PasswordHash))
            //{
            //    return null;
            //}

            //var selUser = _userList.SingleOrDefault(x => x.UserName == model.UserName);

            //// validate
            //if (selUser == null || !BC.Verify(selUser.PasswordHash, model.PasswordHash))
            //{
            //    return null;
            //}

            var jwtTokenExpiryTimes = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var claimsIdentity = new ClaimsIdentity(new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Name, model.UserName)
            });

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT_SECURITY_KEY));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var expirationTimeStamp = DateTime.Now.AddMinutes(5);

            var securityDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = jwtTokenExpiryTimes,
                SigningCredentials = signingCredentials
            };

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtTokenHandler.CreateToken(securityDescriptor);
            var tokenString = jwtTokenHandler.WriteToken(securityToken);
            return new AuthenticationResponse{ 
                UserName =model.UserName,
                JwtToken=tokenString,
                ExpiresIn= (int)jwtTokenExpiryTimes.Subtract(DateTime.Now).TotalSeconds
            };
        }
    }
}
