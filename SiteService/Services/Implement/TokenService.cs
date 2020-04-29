﻿using Common.LifeTime;
using DataTransfer.SettingsDto;
using DataTransfer.TokenDtos;
using Domain.Aggregate.DomainAggregates.UserAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SiteService.Services.Contract;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SiteService.Services.Implement
{
    public class TokenService : ITokenService, IScoped
    {
        private readonly SiteSetting _setting;

        public TokenService(IOptionsSnapshot<SiteSetting> setting)
        {
            this._setting = setting.Value;
        }
        private async Task<string> GenerateToken(User user, int lifeTime)
        {
            var secretKey = Encoding.UTF8.GetBytes(_setting.JwtSetting.SecretKey); // longer that 16 character
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey)
                , SecurityAlgorithms.HmacSha256Signature);

            var encryptionkey = Encoding.UTF8.GetBytes(_setting.JwtSetting.Encryptkey); //must be 16 character
            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey),
                SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            ///تنظیم ویژگی های توکن
            var descriptor = new SecurityTokenDescriptor
            {
                ///صادر کننده توکن
                Issuer = _setting.JwtSetting.Issuer,
                Audience = _setting.JwtSetting.Audience,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(lifeTime),
                SigningCredentials = signingCredentials,
                EncryptingCredentials = encryptingCredentials,
                Subject = new ClaimsIdentity(CustomClaims(user))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(tokenHandler.CreateToken(descriptor));
        }


        private IEnumerable<Claim> CustomClaims(User user)
        {
            /// Create Custom Claim
            var securityStampClaimType = new ClaimsIdentityOptions().SecurityStampClaimType;
            var rolesecurityStampClaimType = new ClaimsIdentityOptions().SecurityStampClaimType;
            var roleId = new ClaimsIdentityOptions().RoleClaimType;
            /// Identity Claim
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(securityStampClaimType,user.SecurityStamp.ToString()),
                new Claim(rolesecurityStampClaimType,user.UserRoles.FirstOrDefault().Role.SecurityStamp.ToString()),
                new Claim(roleId,user.UserRoles.FirstOrDefault().Role.Id.ToString())
            };
            /// User Permissions Claim 
            foreach (var claim in user.UserRoles.FirstOrDefault().Role.AccessLevels.Select(role => role.Access))
                claims.Add(new Claim("Permission", claim));
            return claims;
        }

        public async Task<string> GenerateToken(User user)
        {
            return await GenerateToken(user, 90);

        }
    }
}
