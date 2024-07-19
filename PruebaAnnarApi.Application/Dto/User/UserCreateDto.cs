﻿namespace PruebaAnnarApi.Application.Dto.User
{
    public record UserCreateDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
