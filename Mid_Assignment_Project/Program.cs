using Microsoft.EntityFrameworkCore;
using Mid_Assignment_Project.Models;
using Mid_Assignment_Project.Repository;
using Mid_Assignment_Project.Service;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddDbContext<LibaryDbContext>(options => options.UseSqlServer("Server=DESKTOP-C00IDIB;Initial Catalog=LibMana;Integrated Security=True"));
builder.Services.AddDbContext<LibaryDbContext>(
    options => options.UseSqlServer("Server=DESKTOP-C00IDIB;Initial Catalog=LibMana;Integrated Security=True")
);
// Dependency Repository
builder.Services.AddTransient<ITokenRepository, TokenRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IBookBorrowingRequestRepository, BookBorrowingRequestRepository>();

// Dependency Service
builder.Services.AddTransient<IUserServices, UserServices>();
builder.Services.AddTransient<ICategoryServices, CategoryServices>();
builder.Services.AddTransient<IBookServices, BookServices>();
builder.Services.AddTransient<IBookRequestServices, BookRequestServices>();


builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
                      builder =>
                      {
                          //   builder.WithOrigins("http://localhost:3000");
                          builder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .WithExposedHeaders("*"); // params string[];
                      });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
