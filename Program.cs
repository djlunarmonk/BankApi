using BankApi.Core.Interfaces;
using BankApi.Core.Services;
using BankApi.Data.Context;
using BankApi.Data.Interfaces;
using BankApi.Data.Repos;
using BankApi.Domain.Identity;
using BankApi.Domain.Profiles;
using BankApi.Extension;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAutoMapper(typeof(CustomerProfile));


// DI for db context, Identity and repos
builder.Services.AddDbContext<BankAppDataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BankAPIDev")));

builder.Services.AddIdentityApiEndpoints<AppUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BankAppDataContext>()
    .AddDefaultTokenProviders();


builder.Services.AddTransient<ICustomerRepo, CustomerRepo>();
builder.Services.AddScoped<IAccountRepo, AccountRepo>();

// DI for service layer
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ILoanService, LoanService>();


builder.Services.AddControllers();

// Authorization setup for API and Swagger
builder.Services.AddAuthorization();
builder.Services.AddSwaggerGenExtended();

var app = builder.Build();


app.UseRouting();
//app.UseAuthentication();
app.UseAuthorization();
app.MapIdentityApi<AppUser>();
app.UseEndpoints(configure: endpoints => endpoints.MapControllers());

app.UseSwagger();
app.UseSwaggerUI();

app.Run();