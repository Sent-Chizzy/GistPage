﻿using System.ComponentModel.DataAnnotations;

namespace GistPage.ViewModels
{
    public class ResetPasswordVM
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }

        [Required]  
        public string? NewPassword { get; set; }
        [Compare(nameof(NewPassword))]
        [Required]
        public string? ConfirmPassword { get; set; }


    }
}
