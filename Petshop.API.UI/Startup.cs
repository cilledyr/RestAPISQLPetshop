using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Petshop.Core.ApplicationService;
using Petshop.Core.ApplicationService.Impl;
using Petshop.Core.DomainService;
using Petshop.Core.Enteties;
using Petshop.Infrastructure.Data;
using Petshop.Infrastructure.Data.Repositories;
using System;

namespace Petshop.RestAPI.UI
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
            services.AddDbContext<PetshopAppContext>(
                opt => opt.UseInMemoryDatabase("PetshopDB")
                ); ;

            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IOwnerService, OwnerService>();
            services.AddScoped<IPetTypeRepository, PetTypeRepository>();
            services.AddScoped<IPetTypeService, PetTypeService>();


            services.AddControllers();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var ctx = scope.ServiceProvider.GetService<PetshopAppContext>();
                    var owner1 = ctx.Owners.Add(new Owner()
                    {
                        OwnerFirstName = "Anders",
                        OwnerLastName = "And",
                        OwnerAddress = "Andevænget 4, 3333 Andeby",
                        OwnerPhoneNr = "+33 1234 5678",
                        OwnerEmail = "andersanden@andeby.dk"
                    }).Entity;

                    var owner2 = ctx.Owners.Add(new Owner()
                    {
                        OwnerFirstName = "Andersine",
                        OwnerLastName = "And",
                        OwnerAddress = "Andevænget 8, 3333 Andeby",
                        OwnerPhoneNr = "+33 8765 5678",
                        OwnerEmail = "densmukke@andeby.dk"
                    }).Entity;

                    var dog = ctx.PetTypes.Add(new PetType()
                    {
                        PetTypeName = "Dog"
                    }).Entity;

                    var cat = ctx.PetTypes.Add(new PetType()
                    {
                        PetTypeName = "Cat"
                    }).Entity;

                    ctx.Pets.Add(new Pet()
                    {
                        PetName = "Olfert",
                        PetType = dog,
                        PetBirthday = DateTime.Now.AddDays(-1584),
                        PetColor = "black",
                        PetPreviousOwner ="Martin Madsen",
                        PetSoldDate = DateTime.Now.AddDays(-26),
                        PetPrice = 596,
                        PetOwner = owner1
                    });
                    ctx.SaveChanges();
                }
                
            }
            //remember this is for development only
            /*using (var scope = app.ApplicationServices.CreateScope())
            {
                var ownerRepo = scope.ServiceProvider.GetService<IOwnerRepository>();
                var petRepo = scope.ServiceProvider.GetService<IPetRepository>();
                var petTypeRepo = scope.ServiceProvider.GetService<IPetTypeRepository>();
                new DataInitializer(ownerRepo, petRepo, petTypeRepo).InitData();
            }*/
            //app.UseHttpsRedirection();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PetShopRestAPI V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
