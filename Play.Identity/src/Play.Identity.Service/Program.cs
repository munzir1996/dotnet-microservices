using AspNetCore.Identity.Mongo;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Play.Common.Settings;
using Play.Identity.Service.Entities;
using Play.Identity.Service.HostedServices;
using Play.Identity.Service.Settings;

var builder = WebApplication.CreateBuilder(args);


BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
var serviceSettings = builder.Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
var mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
var identityServerSettings = builder.Configuration.GetSection(nameof(IdentityServerSettings)).Get<IdentityServerSettings>();

// Configure Identity with MongoDB
builder.Services.Configure<IdentitySettings>(builder.Configuration.GetSection(nameof(IdentitySettings)));
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = false;
})
.AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(options =>
{
    options.ConnectionString = mongoDbSettings.ConnectionString;
    //options.DatabaseName = serviceSettings.ServiceName;
}).AddDefaultTokenProviders()
.AddDefaultUI(); 

builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseSuccessEvents = true;
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseFailureEvents = true;
})
    .AddAspNetIdentity<ApplicationUser>()
    .AddInMemoryApiScopes(identityServerSettings.ApiScopes)
    .AddInMemoryApiResources(identityServerSettings.ApiResources)
    .AddInMemoryClients(identityServerSettings.Clients)
    .AddInMemoryIdentityResources(identityServerSettings.IdentityResources);

builder.Services.AddLocalApiAuthentication();

// Add a dummy email sender to satisfy the dependency
builder.Services.AddTransient<IEmailSender, NullEmailSender>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Set to true if you want email confirmation
});
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHostedService<IdentitySeedHostedServices>();
builder.Services.AddRazorPages();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors(builders =>
    {
        builders.WithOrigins(builder.Configuration["AllowedOrigin"]).AllowAnyHeader().AllowAnyMethod();
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Enable routing
app.UseRouting();

app.UseIdentityServer();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();

// Null email sender implementation
public class NullEmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        // Do nothing - this is just to satisfy the dependency
        return Task.CompletedTask;
    }
}