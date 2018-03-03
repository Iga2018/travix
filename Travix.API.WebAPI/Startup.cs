using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Travix.API.BusinessLogic.DTO;
using Travix.API.BusinessLogic.Interfaces;
using Travix.API.BusinessLogic.Services;
using Travix.API.DataAccess.EF;
using Travix.API.DataAccess.Implementations;
using Travix.API.DataAccess.Interfaces;
using Travix.API.WebAPI.Filters;

namespace Travix.API.WebAPI
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			var connectionString = Configuration.GetConnectionString("DefaultConnection");
			services.AddDbContext<BlogContext>(options =>
				options.UseSqlServer(connectionString));
			services.AddTransient<IUnitOfWork, AppUnitOfWork>();
			services.AddTransient<IService<CommentDTO>, CommentService>();
			services.AddTransient<IService<PostDTO>, PostService>();
			services.AddAutoMapper();
			services.AddMvc(options =>
				options.Filters.Add(typeof(GlobalErrorFilter))
			);
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseMvc();
		}
	}
}
