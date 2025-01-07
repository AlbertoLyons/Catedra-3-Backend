using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catedra_3_Backend.src.dtos;
using Catedra_3_Backend.src.models;

namespace Catedra_3_Backend.src.mappers
{
    public static class UserMapper
    {
        public static User ToUserFromRegisteredDTO(this RegisterDTO user)
        {
            return new User
            {
                Email = user.Email,
                UserName = user.Email,
            };
        }
    }
}