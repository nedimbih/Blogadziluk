
using Blogadziluk.Data.DataAccess;
using Blogadziluk.Data.DataManager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace Blogadziluk.Server {
	public class Startup {
		public IConfiguration Configuration { get; }
		public Startup(IConfiguration configuration) {
			Configuration=configuration;
		}
		public void ConfigureServices(IServiceCollection services) {
			services.AddDbContext<BlogadzilukDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddControllers();
			services.AddResponseCompression(opts => {
				opts.MimeTypes=ResponseCompressionDefaults.MimeTypes.Concat(
					new[] { "application/octet-stream" });
			});

			services.AddScoped<IPostsRepo, PostsRepo>();
			services.AddScoped<ITagsRepo, TagsRepo>();
			services.AddScoped<IPostsManager, PostsManager>();
			services.AddScoped<ITagsManager, TagsManager>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			app.UseResponseCompression();

			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
				app.UseBlazorDebugging();
			}

			app.UseStaticFiles();
			//app.UseClientSideBlazorFiles<Client.Startup>();

			app.UseRouting();

			app.UseEndpoints(endpoints => {
				endpoints.MapDefaultControllerRoute();
				//endpoints.MapFallbackToClientSideBlazor<Client.Startup>("index.html");
			});
		}
	}
}
