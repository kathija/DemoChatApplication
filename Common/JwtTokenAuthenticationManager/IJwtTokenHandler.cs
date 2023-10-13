using ChatApplication.Model;
using JwtTokenAuthenticationManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtTokenAuthenticationManager
{
    public interface IJwtTokenHandler
    {
        public AuthenticationResponse GenerateToken(User user);
    }
}
