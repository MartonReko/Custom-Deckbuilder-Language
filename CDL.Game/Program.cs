using Scalar.AspNetCore;

using CDL.Lang;
using CDL.Lang.Parsing;

namespace CDL.Game
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //            LanguageProcessor lp = new();
            //            ObjectsHelper? objectsHelper = lp.ProcessText(Path.Combine(Environment.CurrentDirectory, @"Examples", "example.cdl"));
            //
            //            if (objectsHelper == null)
            //            {
            //                // Input processing failed
            //                return;
            //            }

            var builder = WebApplication.CreateBuilder(args);

            //            builder.Services.AddSingleton<GameService>(x => new GameService(objectsHelper));
            builder.Services.AddSingleton<GameServiceManager>(x => new());

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", policy =>
                policy.WithOrigins("http://localhost:64563")
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

            var app = builder.Build();

            // TODO: Init through front-end
            //
            //            var gameService = app.Services.GetService<GameService>();
            //            gameService!.Initialize();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseCors("AllowSpecificOrigin");

            //app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.MapGet("/", () => "Hello world!");

            app.Run();
        }
    }
}
