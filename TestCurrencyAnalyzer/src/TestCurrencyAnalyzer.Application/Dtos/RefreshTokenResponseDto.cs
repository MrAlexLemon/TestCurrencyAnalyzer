using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestCurrencyAnalyzer.Application.Dtos
{
    public class RefreshTokenResponseDto
    {
        public string UserName { get; }
        public string Role { get; }
        public string AccessToken { get; }
        public string RefreshToken { get; }

        [JsonConstructor]
        public RefreshTokenResponseDto(string userName, string role, string accessToken, string refreshToken)
        {
            UserName = userName;
            Role = role;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
