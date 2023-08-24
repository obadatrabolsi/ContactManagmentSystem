using CMS.Api.Helpers;
using CMS.Api.Middlewares;
using CMS.Core.Base;
using CMS.Core.Cache;
using CMS.Core.Mappers;
using CMS.Core.Models;
using CMS.Core.Settings;
using CMS.Data.Helpers;
using CMS.Data.Repository;
using CMS.Services.Companies;
using CMS.Services.Contacts;
using CMS.Services.ExtendedFields;
using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Quivyo.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
     .ConfigureApiBehaviorOptions(options =>
     {
         options.SuppressModelStateInvalidFilter = false;
         options.InvalidModelStateResponseFactory =
         context => new BadRequestObjectResult(ApiResponse.Error(context.ModelState.GetErrorsDictionary()));
     });

builder.Services.AddSwaggerGen();

// DI Registration
builder.Services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

builder.Services.AddScoped<IExtendedFieldService, ExtendedFieldService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddSingleton<DbHelper>();

// Caching
builder.Services.AddInMemoryCache(new CacheSettings()
{
    DefaultCacheTime = 1440, // Time in minutes. (1 day)
    EnableCaching = true,
});

// Auto Mapper Config
var config = AutoMapperManager.Configure();
AutoMapperFactory.AddAutoMapper(config);

// MongoDb Settings
var mongoDbConfig = new MongoDbSettings();
builder.Configuration.Bind(nameof(MongoDbSettings), mongoDbConfig);
builder.Services.AddSingleton(mongoDbConfig);

// Fluent Validation
var assembles = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.StartsWith("CMS"));

builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddFluentValidationRulesToSwagger()
    .AddValidatorsFromAssemblies(assembles);

var app = builder.Build();

app.UseExceptionMiddleware();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbHelper = scope.ServiceProvider.GetService<DbHelper>();

    await dbHelper.CreateIndexes();
}

app.Run();
