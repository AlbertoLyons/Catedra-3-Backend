using Catedra_3_Backend.src.dtos;

namespace Catedra_3_Backend.src.interfaces
{
    public interface IAuthRepository
    {
        Task<AuthDTO> RegisterUserAsync(RegisterDTO user);
        Task<AuthDTO> LoginUserAsync(LoginDTO loginDTO);
    }
}