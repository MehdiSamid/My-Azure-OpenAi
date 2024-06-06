//using Azure.AI.OpenAI;
//using Azure;
//using Microsoft.EntityFrameworkCore;
//using OpenAI_UIR.Data;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//builder.Services.AddControllers();
//builder.Services.AddDbContext<ConversationContextDb>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//// Configure Azure OpenAI
////builder.Services.AddSingleton(new OpenAIClient(new Uri("https://YOUR_OPENAI_ENDPOINT"), new AzureKeyCredential("YOUR_OPENAI_API_KEY")));

//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();



//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();

using Azure;
using Azure.AI.OpenAI;
using Microsoft.EntityFrameworkCore;
using OpenAI_UIR.Data;
using OpenAI_UIR.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<ConversationContextDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<OpenAIClient>(sp =>
{
    var endpoint = new Uri(builder.Configuration["AzureOpenAI:Endpoint"]);
    var apiKey = builder.Configuration["AzureOpenAI:ApiKey"];
    return new OpenAIClient(endpoint, new AzureKeyCredential(apiKey));
});

// Configure JSON serialization options
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

//builder.Services.AddSingleton<OpenAIService>(new OpenAIService(Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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