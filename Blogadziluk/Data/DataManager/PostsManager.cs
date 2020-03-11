using Blogadziluk.Data.DataAccess;
using System;
using Blogadziluk.Models;
using System.Threading.Tasks;
using SlugGenerator;
using System.Collections.Generic;
using System.Linq;

namespace Blogadziluk.Data.DataManager {
	public class PostsManager : IPostsManager {
		private IPostsRepo _repo;

		public PostsManager(IPostsRepo repo) {
			_repo=repo;
		}

		public PostDTO AddPost (PostDTO incomingPost) {
			incomingPost.BlogPost.Slug=incomingPost.BlogPost.Title.GenerateSlug();
			if (incomingPost.BlogPost.TagList==null) {
				incomingPost.BlogPost.TagList=new List<string>();
			}
			var createdPost = _repo.AddPost(incomingPost.BlogPost);
			var postDTO = new PostDTO { BlogPost = createdPost};
			return postDTO;
		}
		public async Task<PostsDTO> GetPostsAsync(string tag) {
			var posts = await _repo.GetPostsAsync();
			foreach (var post in posts) {
				Post.PopulateTagList(post);
			}

			if (!String.IsNullOrWhiteSpace(tag)) {
				posts=posts.Where(p => p.TagList.Contains(tag)).ToList();
			}
			return new PostsDTO { BlogPosts=posts };
		}

		public async Task<PostDTO> GetPostAsync(string slug) {
			Post post = await _repo.GetPostAsync(slug);
			if (post !=null) {
				Post.PopulateTagList(post);
				return new PostDTO { BlogPost=post };
			}
			else return null;
		}
		public PostDTO UpdatePost(PostDTO incomingPost, string slug) {
			incomingPost.BlogPost.Slug = incomingPost.BlogPost.Title.GenerateSlug();
			var updatedPost = _repo.UpdatePost(incomingPost.BlogPost, slug);
			Post.PopulateTagList(updatedPost);
			if (updatedPost!=null) {
				var postDTO = new PostDTO { BlogPost=updatedPost };
				return postDTO;
			} else
				return null;
		}

		public int DeletePost(string slug) => _repo.DeletePost(slug);

		
	}
}
