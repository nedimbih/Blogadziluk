using Blogadziluk.Models;
using System.Threading.Tasks;

namespace Blogadziluk.Data.DataManager {
	public interface IPostsManager {
		PostDTO AddPost(PostDTO post);
		Task<PostsDTO> GetPostsAsync(string tag);
		Task<PostDTO> GetPostAsync (string slug);
		PostDTO  UpdatePost(PostDTO post, string slug);
		int DeletePost(string slug);
	}
}
