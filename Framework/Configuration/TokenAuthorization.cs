using Common.ErrorHandlingException;
using Common.HttpContextExtentions;
using DataTransfer.SettingsDto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SiteService.Repositories.Implementation;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Configuration
{
    public static class TokenAuthorization
    {
        public static void TokenAuthorize(this IServiceCollection services, SiteSetting siteSetting)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    var securityKey = Encoding.UTF8.GetBytes(siteSetting.JwtSetting.SecretKey);
                    var encriptKey = Encoding.UTF8.GetBytes(siteSetting.JwtSetting.Encryptkey);
                    var ValidatePrameters = new TokenValidationParameters
                    {
                        //Tlorance for Expire Time and Befor Time of Token .
                        ClockSkew = TimeSpan.Zero,
                        RequireSignedTokens = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(securityKey),
                        // I Need Check Expire Token or Not
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ValidateAudience = true,
                        ValidAudience = siteSetting.JwtSetting.Audience,
                        ValidateIssuer = true,
                        ValidIssuer = siteSetting.JwtSetting.Issuer,
                        TokenDecryptionKey = new SymmetricSecurityKey(encriptKey)

                    };
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = ValidatePrameters;
                    options.Events = new JwtBearerEvents
                    {
                        // Fail Authorize
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception != null)
                                throw new FilmstanUnAuthourizeException("No Access");
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = async context =>
                         {
                             var domainUnitofWork = context.HttpContext.RequestServices.GetRequiredService<IDomainUnitOfWork>();
                             var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                             var securityStamp = claimsIdentity.FindAll(new ClaimsIdentityOptions().SecurityStampClaimType).ToList();
                             var userSecurityInfo = await domainUnitofWork.UsersRepository.GetUserTokenInfo(Guid.Parse(claimsIdentity.GetUserId()));

                             if (claimsIdentity.Claims.Any() != true)
                                 context.Fail("Token Has No Claim");

                             if (securityStamp == null)
                                 throw new FilmstanUnAuthourizeException("No securityStamp");

                             if (userSecurityInfo.Result.IsActive == false)
                                 throw new FilmstanUnAuthourizeException("User not Active");

                             if (userSecurityInfo.Result.UserSecurityStamp != Guid.Parse(securityStamp[0].Value))
                                 throw new FilmstanUnAuthourizeException("User Security Stamp Invalid");

                             if (userSecurityInfo.Result.RoleSecurityStamp!= Guid.Parse(securityStamp[1].Value))
                                 throw new FilmstanUnAuthourizeException("Role Security Stamp Invalid");

                         },
                        // when Server Recived the Token haven't Token 
                        OnChallenge = context =>
                        {
                            if (context.AuthenticateFailure != null)
                                throw new FilmstanUnAuthourizeException("AuthenticateFailure");
                            return Task.CompletedTask;
                        }
                    };
                });
        }
    }
}
