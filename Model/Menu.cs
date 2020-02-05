using System.ComponentModel.DataAnnotations;

namespace Model
{
	public class Menu
    {
            [Key]
            public int ID { get; set; }
            public string Text { get; set; }
            public int DisplayOrder { get; set; }
            public string Link { get; set; }
            public bool Status { get; set; }
    }
}
