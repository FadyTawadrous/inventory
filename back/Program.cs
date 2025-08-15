var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

services.AddDBContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

app.MapGet("/", () => "Hello World!");

app.Run();
