using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//MyComment:dependencies injection starts here
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//MyComment: this is called Dependencies injection
builder.Services.AddDbContext<StoreContext>(opt=>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));  
});


 
var app = builder.Build();

//MyComment:depency inject ends here

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Used for production Https
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<StoreContext>();
var logger =  scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
try
{
    context.Database.Migrate();
    DbInitializer.Initialize(context);
}
catch (Exception Ex)
{
    logger.LogError(Ex,"Problem Occured during Migration");
}

app.Run();
