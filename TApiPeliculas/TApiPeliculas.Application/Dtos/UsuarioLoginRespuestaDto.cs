﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
 

namespace TApiPeliculas.Application.Dtos
{
    public class UsuarioLoginRespuestaDto
    {
        
        public UsuarioDatosDto Usuario { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
