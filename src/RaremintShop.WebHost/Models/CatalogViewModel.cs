﻿using System.ComponentModel.DataAnnotations;

namespace RaremintShop.WebHost.Models
{
    public class CatalogViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Range(0, 100000)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        public List<string> ImageUrls { get; set; } = [];
    }
}
