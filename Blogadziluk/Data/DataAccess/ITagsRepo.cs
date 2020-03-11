using Blogadziluk.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogadziluk.Data.DataAccess {
	public interface ITagsRepo {
		Task<List<string>> GetAllTagsAsync();
	}
}
