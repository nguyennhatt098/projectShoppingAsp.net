using System.ComponentModel.DataAnnotations;

namespace Model
{
	public class LoginModel
    {
        [Required(ErrorMessage = "Mời nhập UserName")]
        public string UserName { set; get; }
        [Required(ErrorMessage = "Mời nhập password")]
        public string Password { set; get; }
        public bool RememberMe { set; get; }
    }
}
