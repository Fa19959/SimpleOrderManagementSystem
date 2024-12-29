﻿using System.ComponentModel.DataAnnotations;

namespace SimpleOrderManagementSystem.DTOs
{
    public class ProductInputDTO
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }
    }
}
