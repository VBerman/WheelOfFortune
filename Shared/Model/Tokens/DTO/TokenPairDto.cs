using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheelOfFortune.Shared.Model.Tokens
{
    public class TokenPairDto
    {
        public TokenPairDto(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }   
    }
}
