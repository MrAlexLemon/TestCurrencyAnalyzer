using System;
using System.Collections.Generic;
using System.Text;

namespace TestCurrencyAnalyzer.Application.Interfaces.Services.Identity
{
    public interface IPasswordService
    {
        bool IsValid(string hash, string password);
        string Hash(string password);
    }
}
