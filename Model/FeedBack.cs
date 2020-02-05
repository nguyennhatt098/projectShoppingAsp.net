using System;

namespace Model
{
	public class FeedBack
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public string Content { get; set; }
        public DateTime Createad { get; set; }
        public bool Status { get; set; }
    }
}
