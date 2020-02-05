using System.ComponentModel.DataAnnotations;

namespace Model.ViewModel
{
	public class RegisterUser
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PassWord { get; set; }
        [Required]
        [Compare("PassWord")]
        public string ConfirmPassWord { get; set; }

    }
}
