using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCurrencyAnalyzer.Application.Dtos
{
    public class JwtAuthResultDto
    {
        public string AccessToken { get; set; }

        public RefreshTokenDto RefreshToken { get; set; }
    }
}
