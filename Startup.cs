using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Covid_19.CoreAPI {
    public class Startup {
        public void ConfigureServices (IServiceCollection services) {
            services.AddControllers ();
            services.AddSwaggerGen (c => {
                c.SwaggerDoc ("v1", new OpenApiInfo {
                    Title = "Covid-19 API Document V1",
                        Version = "v1",
                        Contact = new OpenApiContact {
                            Name = "Double D",
                                Email = "namdd72@wru.vn",
                                Url = new Uri ("http://namdd72.sytes.net")
                        },
                        Description = "Source data ncov.mog.gov.vn"
                });
            });
        }

        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseRouting ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });

            app.UseSwagger (c => {
                c.RouteTemplate = "Covid19/swagger/{documentname}.json";
            });

            app.UseSwaggerUI (c => {
                c.DocumentTitle = "Covid-19 API Document </> Double D";
                c.RoutePrefix = "Covid19";
                c.SwaggerEndpoint ("/Covid19/swagger/v1.json", "Covid-19 API v1");
            });
        }
    }
}