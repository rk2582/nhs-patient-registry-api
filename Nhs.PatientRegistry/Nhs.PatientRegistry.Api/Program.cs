using Asp.Versioning;
using FluentValidation;
using Nhs.PatientRegistry.Api.Abstractions;
using Nhs.PatientRegistry.Api.Mapping;
using Nhs.PatientRegistry.Api.Services;
using Nhs.PatientRegistry.Api.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configure Automapper
builder.Services.AddAutoMapper(configuration =>
{
    configuration.AddProfile<PatientMappingProfile>();
});


builder.Services.AddSingleton<IPatientRepository, InMemoryPatientRepository>();
builder.Services.AddScoped<IPatientService, PatientService>();

builder.Services.AddScoped<IValidator<int>, PatientIdValidator>();

//--- API Versioning Configuration ---
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
})
   .AddApiExplorer(options =>
   {
       options.GroupNameFormat = "'v'VVV";
       options.SubstituteApiVersionInUrl = true;
   });


var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapControllers();

app.Run();
