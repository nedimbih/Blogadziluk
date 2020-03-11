using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blogadziluk.Models;


namespace Blogadziluk.Data.DataAccess {
	public class PostsRepo : IPostsRepo {
		private BlogadzilukDbContext _context;

		public PostsRepo(BlogadzilukDbContext context) {
			_context=context;
		}

		public Post AddPost(Post post) {
			try {
				ProcessTags(post);
				post.CreatedAt = DateTime.Now;
				var created =  _context.Add(post);
				if (_context.SaveChanges() > 0) {
					return created.Entity;
				}
				else throw new Exception("Db Error: Entry is not saved!");
			} catch (Exception) {throw;}
		}
			
		public async Task<List<Post>> GetPostsAsync() {
			try {
				var posts = await _context.Posts
											.Include( p => p.PostTags)
											.ThenInclude(pt => pt.Tag)
											.ToListAsync();
				return posts;
			} catch (Exception) { throw; }
		}

		public async Task<Post> GetPostAsync(string slug) {
			try {
				return await _context.Posts
					.Include(p => p.PostTags)
					.ThenInclude(pt => pt.Tag)
					.FirstOrDefaultAsync<Post>(p => p.Slug==slug);

			} catch (Exception) { throw; }
		}

		public Post UpdatePost(Post post, string slug) {
			try {
				var entry = _context.Posts
							.Include(p => p.PostTags)
							.ThenInclude(pt => pt.Tag)
							.FirstOrDefault(p => p.Slug == slug);
				if (entry==null) return null;
				PopulateProperties(entry, post, slug);
				
				entry.UpdatedAt = DateTime.Now;
				var updated = _context.Update(entry);
				if (_context.SaveChanges()>0) {
					return updated.Entity;
				} else
					throw new Exception("Db Error: Entry is not saved!");
			} catch (Exception) { throw; }
		}

		public int DeletePost(string slug) {
			try {
				var post = _context.Posts.FirstOrDefault(p => p.Slug == slug);
				 _context.Posts.Remove(post);
				return  _context.SaveChanges();
			} catch (Exception) {throw;}
		}

		private void PopulateProperties(Post entry, Post post, string slug) {
			if (post.Title!=null) {
				entry.Title=post.Title;
				entry.Slug=post.Slug;
			}
			if (post.Body!=null) {
				entry.Body=post.Body;
			}
			if (post.Description!=null) {
				entry.Description=post.Description;
			}
			if (post.TagList != null) {
				entry.PostTags = new List<Tags2Posts>();
				RemoveUnnecessaryTags(entry, post, slug);
				entry.TagList=post.TagList;
				ProcessTags(entry, slug);
			}
		}

		private void RemoveUnnecessaryTags (Post entry, Post post, string slug) {
			Post.PopulateTagList(entry);
			var tags2delete = entry.TagList.Except(post.TagList);
			foreach (var tag in tags2delete) {
				var tag2post = _context.Set<Tags2Posts>()
						.FirstOrDefault(tp => tp.Tag.Text==tag&&tp.Post.Slug==slug);
				_context.Set<Tags2Posts>()
						.Remove(tag2post);
			}
		}

		private void ProcessTags(Post post, string slug="") {
			foreach (var item in post.TagList) {
				var tag = _context.Tags.FirstOrDefault(t => t.Text == item);
				var tag2post = new Tags2Posts();
				if (tag==null) {
					tag=new Tag { Text=item };
					tag2post = new Tags2Posts { Tag=tag };
				} else {
					var t2p =_context.Set<Tags2Posts>()
						.FirstOrDefault(
						tp => tp.Tag.Text == item && tp.Post.Slug == slug);
					if (t2p ==null) {
						tag2post =new Tags2Posts { Tag=tag, TagId = tag.TagId };
					} else {
						tag2post=t2p;
					}
				}

				post.PostTags.Add(tag2post);
			}
		}
	}
}
