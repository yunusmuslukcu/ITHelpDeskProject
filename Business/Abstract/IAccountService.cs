using Dto.UserDtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAccountService
    {
        // Kayıt olma mantığı
        Task<IdentityResult> CreateUserAsync(UserRegisterDto userRegisterDto);

        // Giriş yapma mantığı
        Task<SignInResult> LoginAsync(UserLoginDto userLoginDto);

        // Güvenli çıkış
        Task LogoutAsync();
    }
}
