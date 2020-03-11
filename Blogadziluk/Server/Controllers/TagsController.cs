using Blogadziluk.Data.DataManager;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogadziluk.Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase {
        private ITagsManager _tagsManager;

        public TagsController(ITagsManager mngr) {
            _tagsManager=mngr;
        }
        // GET: api/Tags
        [HttpGet]
        public async Task<ActionResult<List<string>>> Get() {
            try {
                var tags = await _tagsManager.GetAllTagsAsync();
                return Ok(new { Tags = tags });
            } catch (Exception) {
                return BadRequest();
            }
        }
    }
}
