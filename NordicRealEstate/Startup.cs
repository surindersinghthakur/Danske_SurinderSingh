using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NordicRealEstate.Api.DataAccess;
using NordicRealEstate.Api.Interfaces;

namespace NordicRealEstate
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			/* To add authorization from Auth Server
			string domain = $"https://{Configuration["Auth0:Domain"]}/";
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

			}).AddJwtBearer(options =>
			{
				options.Authority = domain;
				options.Audience = Configuration["Auth0:ClientId"];

				options.Events = new JwtBearerEvents
				{
					OnMessageReceived = context =>
					{
						var tokenValue = context.Request.Query["access_token"];
						//  if (VerifyToken(tokenValue))
						//{
						context.Token = tokenValue;
						//}
						return Task.CompletedTask;
					}
				};
			});

			services.AddHttpContextAccessor();

			services.AddAuthorization(options =>
			{
				options.AddPolicy("read:messages", policy => policy.Requirements.Add(new HasScopeRequirement("read:messages", domain)));
			});
			*/
			services.AddControllers();
			services.AddScoped<IMunicipalityDal, MunicipalityDal>();
			services.AddScoped<ITaxes, TaxesDal>();
			services.AddScoped<ITaxMapping, TaxMappingDal>();
			services.AddScoped<ITaxUtil, TaxRateDal>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
