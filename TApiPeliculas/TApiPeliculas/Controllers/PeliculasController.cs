﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TApiPeliculas.Application.Dtos;
using TApiPeliculas.Application.Interfaces;
using TApiPeliculas.Application.Services;
using TApiPeliculas.Core.Entities;

namespace TApiPeliculas.Controllers
{
    [Route("api/Peliculas")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly IPeliculaService _pelRepo;
        //private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IMapper _mapper;

        public PeliculasController(IPeliculaService pelRepo, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _pelRepo = pelRepo;
            _mapper = mapper;
            //_hostingEnvironment = hostingEnvironment;
        }

        
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetPeliculas()
        {
            var listaPeliculas = _pelRepo.GetAllPeliculas();

            var listaPeliculasDto = new List<PeliculaDto>();

            foreach (var lista in listaPeliculas)
            {
                listaPeliculasDto.Add(_mapper.Map<PeliculaDto>(lista));
            }
            return Ok(listaPeliculasDto);
        }

       
        [AllowAnonymous]
        [HttpGet("{peliculaId:int}", Name = "GetPelicula")]
        public IActionResult GetPelicula(int peliculaId)
        {
            var itemPelicula = _pelRepo.GetPelicula(peliculaId);

            if (itemPelicula == null)
            {
                return NotFound();
            }

            var itemPeliculaDto = _mapper.Map<PeliculaDto>(itemPelicula);
            return Ok(itemPeliculaDto);
        }

       
        [AllowAnonymous]
        [HttpGet("GetPeliculasEnCategoria/{categoriaId:int}")]
        public IActionResult GetPeliculasEnCategoria(int categoriaId)
        {
            var listaPelicula = new List<object>();//_pelRepo.GetPeliculasEnCategoria(categoriaId);

            if (listaPelicula == null)
            {
                return NotFound();
            }

            var itemPelicula = new List<PeliculaDto>();
            foreach (var item in listaPelicula)
            {
                itemPelicula.Add(_mapper.Map<PeliculaDto>(item));
            }

            return Ok(itemPelicula);
        }

       
        [AllowAnonymous]
        [HttpGet("Buscar")]
        public IActionResult Buscar(string nombre)
        {
            //try
            //{
            //    var resultado = _pelRepo.BuscarPelicula(nombre);
            //    if (resultado.Any())
            //    {
            //        return Ok(resultado);
            //    }

            //    return NotFound();
            //}
            //catch (Exception)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError, "Error recuperando datos de la aplicación");
            //}
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]       
        [ProducesResponseType(201, Type = typeof(PeliculaDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult CrearPelicula([FromForm] PeliculaDto peliculaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (peliculaDto == null)
            {
                return BadRequest(ModelState);
            }

            //if (_pelRepo.ExistePelicula(peliculaDto.Nombre))
            //{
            //    ModelState.AddModelError("", "La película ya existe");
            //    return StatusCode(404, ModelState);
            //}            

            var pelicula = _mapper.Map<Pelicula>(peliculaDto);
            _pelRepo.CreateMovieAsync(pelicula);
            //if (!)
            //{
            //    ModelState.AddModelError("", $"Algo salio mal guardando el registro{pelicula.Nombre}");
            //    return StatusCode(500, ModelState);
            //}

            return CreatedAtRoute("GetPelicula", new { peliculaId = pelicula.Id }, pelicula);
        }


        [Authorize(Roles = "admin")]
        [HttpPatch("{peliculaId:int}", Name = "ActualizarPelicula")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult ActualizarPelicula(int peliculaId, [FromBody] PeliculaDto peliculaDto)
        {
            if (peliculaDto == null || peliculaId != peliculaDto.Id)
            {
                return BadRequest(ModelState);
            }


            var pelicula = _mapper.Map<Pelicula>(peliculaDto);
            _pelRepo.UpdateMovieAsync(pelicula);
            //if (!)
            //{
            //    ModelState.AddModelError("", $"Algo salio mal actualizando el registro{pelicula.Nombre}");
            //    return StatusCode(500, ModelState);
            //}

            return NoContent();
        }


        [Authorize(Roles = "admin")]
        [HttpDelete("{peliculaId:int}", Name = "BorrarPelicula")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult BorrarPelicula(int peliculaId)
        {
            //if (!_pelRepo.ExistePelicula(peliculaId))
            //{
            //    return NotFound();
            //}

            var pelicula = _pelRepo.GetPelicula(peliculaId);
            _pelRepo.DeleteMovieAsync(pelicula.Id);
            //if (!)
            //{
            //    ModelState.AddModelError("", $"Algo salio mal borrando el registro{pelicula.Nombre}");
            //    return StatusCode(500, ModelState);
            //}

            return NoContent();
        }
    }
}
