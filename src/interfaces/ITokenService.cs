using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catedra_3_Backend.src.models;

namespace Catedra_3_Backend.src.interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);

    }
}