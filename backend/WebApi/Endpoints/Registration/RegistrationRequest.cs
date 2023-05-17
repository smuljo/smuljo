﻿namespace WebApi.Endpoints.Registration;

public sealed class RegistrationRequest
{
    public required string UserName { get; set; }

    public required string Password { get; set; }
}

