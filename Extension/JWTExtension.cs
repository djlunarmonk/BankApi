using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;


namespace BankApi.Extension
{
    public static class JWTExtension
    {

        public static IServiceCollection AddAuthenticationExtended(this IServiceCollection services)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
        //I andra delen sätts konfiguration av JWT upp
        .AddJwtBearer(opt =>
        {

            //Hur en token skall valideras
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "RuBankLtd",
                ValidAudience = "RuBankLtd",
                IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("!?12345BabysFirstSecretKey7890!?"))
            };

        });

            return services;
        }


        //public static string GenerateToken(AppUser user)
        //{
        //    var claims = new List<Claim>();
        //    claims.Add(new Claim(ClaimTypes.Role, user.));
        //    claims.Add(new Claim(ClaimTypes.Name, user.Name));
        //    claims.Add(new Claim(ClaimTypes.Email, user.Email));
        //    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        //    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("!?12345BabysFirstSecretKey7890!?"));
        //    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        //    var tokenOptions = new JwtSecurityToken(
        //        issuer: "RuBankLtd",
        //        audience: "RuBankLtd",
        //        claims: claims,
        //        expires: DateTime.Now.AddMinutes(20),
        //        signingCredentials: signinCredentials
        //        );

        //    return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        //}


        public static IServiceCollection AddSwaggerGenExtended(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "BankAPI", Version = "v1" });
                opt.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,

                });
                opt.OperationFilter<SecurityRequirementsOperationFilter>();


            });
            return services;

        }
    }
}

