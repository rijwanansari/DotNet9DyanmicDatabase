using System;

namespace Application.Users.Dto;

public record UserDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
