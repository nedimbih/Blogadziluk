using System.Collections.Generic;

namespace Blogadziluk.Models {
	public class PostsDTO {
		public List<Post> BlogPosts { get; set; } = new List<Post>();
		public int PostsCount { get { return BlogPosts.Count; } }
	}
}
