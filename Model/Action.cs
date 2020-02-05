﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model
{
	public class Action
    {
        [Key]
        public int ActionId { get; set; }
        [DisplayName("Tên action")]
		public string ActionName { get; set; }
		[DisplayName("Mô tả")]
		public string Description { get; set; }
        public virtual ICollection<RoleAction> RoleActions { get; set; }
    }
}
