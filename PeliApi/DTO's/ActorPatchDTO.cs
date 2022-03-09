﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliApi.DTO_s
{
    public class ActorPatchDTO
    {
        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
