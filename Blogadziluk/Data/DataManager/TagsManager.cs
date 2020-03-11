using Blogadziluk.Data.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogadziluk.Data.DataManager {
	public class TagsManager : ITagsManager {
		private ITagsRepo _tagsRepo;

		public TagsManager(ITagsRepo repo) {
			_tagsRepo=repo;
		}
		public async Task<List<string>> GetAllTagsAsync() => 
			await _tagsRepo.GetAllTagsAsync();
	}
}
