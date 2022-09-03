using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Core.DTOs
{
    public class LoginViewModel
    {
        [DisplayName("ایمیل")]
        [Email(ErrorMessage = "{0} معتبر نمی باشد")]
        [Required(ErrorMessage ="فیلد {0} اجباری است")]
        public string Email { get; set; }

        [DisplayName("کلمه عبور")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        public string Password { get; set; }


        [DisplayName("مرا به خاطر بسپار !")]
        public bool RememberMe { get; set; }
    }
}
