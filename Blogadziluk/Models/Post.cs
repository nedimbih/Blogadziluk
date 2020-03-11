using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Blogadziluk.Models {
	public class Post {
		[JsonIgnore]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string PostId { get; set; }
		public string Slug { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Body { get; set; }
		[NotMapped] public List<string> TagList { get; set; } 
		[JsonIgnore] public List<Tags2Posts> PostTags { get; set; } = new List<Tags2Posts>();

		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		public static void PopulateTagList (Post post) {
			post.TagList=new List<string>();
			foreach (var item in post.PostTags) {
				post.TagList.Add(item.Tag.Text);
			}
		}
	}
}
