
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public class LoginViewModel
    {
        [DisplayName("ایمیل")]
        [EmailAddress(ErrorMessage = "{0} معتبر نمی باشد")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        public string Email { get; set; }

        [DisplayName("کلمه عبور")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        public string Password { get; set; }
    }
}
