using Blogadziluk.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogadziluk.Data.DataAccess {
	public interface IPostsRepo {
		Post AddPost(Post post);
		Task<List<Post>> GetPostsAsync ();
		Task<Post> GetPostAsync (string slug);
		Post UpdatePost (Post post, string slug);
		int DeletePost (string slug);
	}
}
