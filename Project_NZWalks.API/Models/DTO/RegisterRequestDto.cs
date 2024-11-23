﻿using System.ComponentModel.DataAnnotations;

namespace Project_NZWalks.API.Models.DTO;

public class RegisterRequestDto
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Username {  get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    public string[] Roles { get; set; }
}
