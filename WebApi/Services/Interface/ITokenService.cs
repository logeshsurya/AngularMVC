using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;

namespace WebApi.Services.Interface
{
    public interface ITokenService
    {   
        object GenerateToken(Login Credentials);
    }
}