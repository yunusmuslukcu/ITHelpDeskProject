using Business.Abstract;
using Dto.UserDtos;
using Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AccountManager : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountManager(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> CreateUserAsync(UserRegisterDto userRegisterDto)
        {
            // 1. Adım: DTO'dan gelen verileri AppUser nesnesine eşliyoruz (Mapping)
            AppUser user = new AppUser()
            {
                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName,
                Email = userRegisterDto.Email,
                UserName = userRegisterDto.UserName,
                ImageUrl = "/images/default-user.png"
            };

            // 2. Adım: Kullanıcıyı oluşturuyoruz
            var result = await _userManager.CreateAsync(user, userRegisterDto.Password);

            // 3. Adım: Eğer kullanıcı başarıyla oluşturulduysa, ona varsayılan bir rol atıyoruz
            if (result.Succeeded)
            {
                // Yeni kayıt olan her kullanıcıyı başlangıçta "Member" (Üye) yapıyoruz
                // Not: Veritabanında "Member" adında bir rolün önceden eklenmiş olması gerekir.
                await _userManager.AddToRoleAsync(user, "Member");
            }

            return result;
        }

        public async Task<SignInResult> LoginAsync(UserLoginDto userLoginDto)
        {
            // PasswordSignInAsync parametreleri: 
            // UserName, Password, Beni Hatırla (Persistent), Hatalı girişte hesap kilitlensin mi?
            var result = await _signInManager.PasswordSignInAsync(
                userLoginDto.UserName,
                userLoginDto.Password,
                userLoginDto.RememberMe,
                false);

            return result;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}