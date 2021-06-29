using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestCurrencyAnalyzer.Application.Dtos
{
    public class UserDto
    {
        public string UserName { get;}
        public string Role { get; }
        
        [JsonConstructor]
        public UserDto(string username, string role)
        {
            UserName = username;
            Role = role;
        }
    }
}
