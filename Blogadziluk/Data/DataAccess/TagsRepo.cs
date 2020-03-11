using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blogadziluk.Models;
using Microsoft.EntityFrameworkCore;

namespace Blogadziluk.Data.DataAccess {
	public class TagsRepo : ITagsRepo {
		private BlogadzilukDbContext _context;

		public TagsRepo(BlogadzilukDbContext cntxt) => _context=cntxt;


		public async Task<List<string>> GetAllTagsAsync() => 
			await _context.Tags
				. Select(t => t.Text)
				.ToListAsync();
	}
}
