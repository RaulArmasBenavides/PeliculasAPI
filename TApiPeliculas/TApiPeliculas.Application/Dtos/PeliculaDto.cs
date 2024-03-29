﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TApiPeliculas.Application.Dtos
{
    public class PeliculaDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }
        public string RutaImagen { get; set; }

        [Required(ErrorMessage = "La descripcion es obligatorio")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La duración es obligatoria")]
        public int Duracion { get; set; }
        public enum TipoClasificacion { Siete, Trece, Dieciseis, Dieciocho }
        public TipoClasificacion Clasificacion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int categoriaId { get; set; }
        //[ForeignKey("categoriaId")]
        //public Categoria Categoria { get; set; }
    }
}
