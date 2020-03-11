using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogadziluk.Models {
	public class Tag {
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string TagId { get; set; }
		public string Text { get; set; }
		public List<Tags2Posts> PostTags { get; set; } = new List<Tags2Posts>();
	}
}