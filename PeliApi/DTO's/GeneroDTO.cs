﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliApi.DTO_s
{
    public class GeneroDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Name { get; set; } 
    }
}
