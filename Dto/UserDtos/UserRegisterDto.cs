using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.UserDtos
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "Lütfen adınızı giriniz.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lütfen soyadınızı giriniz.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Lütfen kullanıcı adınızı giriniz.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "E-posta adresi boş bırakılamaz.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre tekrarı zorunludur.")]
        [Compare("Password", ErrorMessage = "Şifreler birbiriyle uyuşmuyor.")]
        public string ConfirmPassword { get; set; }
    }
}
