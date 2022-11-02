using Microsoft.EntityFrameworkCore;
using NASCAR_Backend.Context;
using NASCAR_Backend.Repositories;
using NASCAR_Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<NascarDbContext>(options => options
                                                            .UseLazyLoadingProxies()
                                                            .UseSqlServer(connectionString));
// Repositories
builder.Services.AddScoped<PilotsRepository>();
builder.Services.AddScoped<StagesRepository>();
builder.Services.AddScoped<TeamsRepository>();
builder.Services.AddScoped<ResultsRepository>();
builder.Services.AddScoped<ManufacturersRepository>();
builder.Services.AddScoped<ChangesRepository>();
builder.Services.AddScoped<TracksRepository>();

// Services
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<StagesService>();
builder.Services.AddScoped<PilotsService>();
builder.Services.AddScoped<TracksService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        );

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();


app.UseAuthorization();

app.MapControllers();

app.Run();
