using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catedra_3_Backend.src.data;
using Catedra_3_Backend.src.dtos;
using Catedra_3_Backend.src.interfaces;
using Catedra_3_Backend.src.mappers;
using Catedra_3_Backend.src.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Catedra_3_Backend.src.repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<User> _signInManager;
        public AuthRepository(DataContext dataContext, UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager)
        {
            _dataContext = dataContext;
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }
        public async Task<AuthDTO> LoginUserAsync(LoginDTO loginDTO)
        {
            string normalizedEmail = loginDTO.Email.ToUpper();
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.NormalizedUserName == normalizedEmail);
            if(user == null) throw new Exception("Invalid email or password");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);
            if (!result.Succeeded) throw new Exception("Invalid email or password");
            var newUser = await _tokenService.CreateToken(user);
            var auth = new AuthDTO
            {
                Id = user.Id,
                Email = user.Email!,
                Roles = (List<string>)await _userManager.GetRolesAsync(user),
                Token = newUser
            };
            return auth;        
        }

        public async Task<AuthDTO> RegisterUserAsync(RegisterDTO user)
        {
            bool exist = await _userManager.FindByEmailAsync(user.Email) != null;
            if(exist) throw new Exception("Email already exists");
            // Crea un nuevo usuario
            User newUser = user.ToUserFromRegisteredDTO();
            // Obtiene el resultado de la creación del usuario
            var result = await _userManager.CreateAsync(newUser, user.Password);
            // Si la creación fue exitosa
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "User");
                await _dataContext.SaveChangesAsync();
            }
            // Si la creación falló
            else
            {
                throw new Exception("Error creating user");
            }
            // Crea un token para el usuario
            var registeredUser = await _tokenService.CreateToken(newUser);
            var auth = new AuthDTO
            {
                Id = newUser.Id,
                Email = newUser.Email!,
                Roles = (List<string>)await _userManager.GetRolesAsync(newUser),
                Token = registeredUser
            };
            return auth;      
        }
    }
}