﻿using System.ComponentModel.DataAnnotations;

namespace Model.ViewModel
{
	public class ResetPassWord
    {
        [Required(ErrorMessage="Mật Khẩu không được để trống",AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="Xác nhận mật khẩu và mật khẩu không giống nhau")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string ResetCode { get; set; }
    }
}
