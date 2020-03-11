using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogadziluk.Data.DataManager {
	public interface ITagsManager {
		Task<List<string>> GetAllTagsAsync();
	}
}