using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtTokenAuthenticationManager.Model
{
    public class AuthenticationResponse
    {

        public string UserName { get; set; }

        public string JwtToken { get; set; }

        public int ExpiresIn { get; set; }

        public string Message { get; set; }
    }
}
