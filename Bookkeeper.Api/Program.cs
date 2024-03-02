using Bookkeeper.Api.Helpers;

var builder = WebApplication.CreateBuilder(args);


// Add necessary services

builder.Services.AddControllers();

// Add Swagger

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IFakeDbHelper,DbHelper>();



// Build App

var app = builder.Build();

// Enable Swagger UI and OAS json
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
