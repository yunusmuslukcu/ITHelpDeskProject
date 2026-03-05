using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.UserDtos
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "Kullanıcı adınızı giriniz.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifrenizi giriniz.")]
        public string Password { get; set; }

        // "Beni Hatırla"
        public bool RememberMe { get; set; }
    }
}
