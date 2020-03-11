using Blogadziluk.Models;
using Blogadziluk.Data.DataManager;
using Microsoft.AspNetCore.Mvc;
using SlugGenerator;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace Blogadziluk.Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase {
        private IPostsManager _postManager;

        public PostsController(IPostsManager mngr) {
            _postManager=mngr;
        }

        // POST: api/Posts
        [HttpPost]
        public ActionResult<PostDTO> Post([FromBody] PostDTO post) {
            if (ModelState.IsValid && RequiredFieldPresent(post.BlogPost)) {
                try {
                    var createdPost = _postManager.AddPost(post);
                    return CreatedAtAction(nameof(GetAsync), createdPost); // createdPost can not be null 
                 } catch (Exception) { /*BadRequest();*/ }
            }
            return BadRequest();
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<PostsDTO>> GetPostsAsync([FromQuery]string tag) {
            try {
                var posts =await _postManager.GetPostsAsync(tag);
                if (posts.BlogPosts.Count > 0) {
                    return posts;
                }
                else 
                    return NotFound();
            } catch (Exception) {
                return BadRequest();
            }
        }

        // GET: api/Posts/:slug
        [HttpGet("{slug}", Name = "Get")]
        public async Task<ActionResult<PostDTO>> GetAsync(string slug) {
            try {
                var post = await _postManager.GetPostAsync(slug);
                if (post != null) {
                    return post;
                }
                else 
                    return NotFound();
            } catch (Exception) {
                return BadRequest();
            }
        }

        // PUT: api/Posts/:slug
        [HttpPut("{slug}")]
        public ActionResult<PostDTO> Put([FromBody] PostDTO post, [FromRoute]string slug) {
            if (ModelState.IsValid) {
                try {
                    var updatedPost = _postManager.UpdatePost(post, slug);
                    if (updatedPost!=null) {
                        return Ok(updatedPost);
                    } else
                        return NotFound();
                } catch (Exception) { /*BadRequest();*/ }
            }
            return BadRequest();
        }

        // DELETE: api/posts/:slug
        [HttpDelete("{slug}")]
        public ActionResult Delete(string slug) {
            try {
                var success = _postManager.DeletePost(slug);
                if (success == 0) {
                    return NotFound();
                } else {
                    return Ok();
                }
            } catch (Exception) {return BadRequest();}
        }

        private bool RequiredFieldPresent(Post post) =>
            post.Body!=null&&
            post.Title!=null&&
            post.Description!=null;
    }
}
