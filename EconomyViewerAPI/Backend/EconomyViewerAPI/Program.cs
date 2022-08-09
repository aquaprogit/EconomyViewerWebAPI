using EconomyViewerAPI.BLL.Services;
using EconomyViewerAPI.BLL.Services.Interfaces;
using EconomyViewerAPI.DAL.EF;
using EconomyViewerAPI.BLL.Filters;
using EconomyViewerAPI.BLL.Repos;
using EconomyViewerAPI.BLL.Repos.Interfaces;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();

builder.Services.AddDbContext<ApplicationContext>();

builder.Services.AddScoped<IItemRepo, ItemRepo>();
builder.Services.AddScoped<IServerRepo, ServerRepo>();

builder.Services.AddSingleton<IServerLoader, ServersParser>();

builder.Services.AddScoped<ServerService>();
builder.Services.AddScoped<ItemService>();

builder.Services.AddControllers(config => config.Filters.Add(new CustomExceptionFilterAttribute(builder.Environment)));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication? app = builder.Build();
app.UseCors(builder => builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithOrigins("http://localhost:5500"));
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
