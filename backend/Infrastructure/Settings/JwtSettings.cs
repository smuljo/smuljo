﻿using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Settings;

public sealed class JwtSettings
{
    public const string SectionName = "Jwt";

    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required string Key { get; set; }

    public SecurityKey SecurityKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
}