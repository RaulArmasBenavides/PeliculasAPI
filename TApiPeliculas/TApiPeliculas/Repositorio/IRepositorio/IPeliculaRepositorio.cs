﻿using TApiPeliculas.Modelos;

namespace TApiPeliculas.Repositorio.IRepositorio
{
    public interface IPeliculaRepositorio
    {
        ICollection<Pelicula> GetPeliculas();
        ICollection<Pelicula> GetPeliculasEnCategoria(int CatId);
        Pelicula GetPelicula(int PeliculaId);
        bool ExistePelicula(string nombre);
        IEnumerable<Pelicula> BuscarPelicula(string nombre);
        bool ExistePelicula(int id);
        bool CrearPelicula(Pelicula pelicula);
        bool ActualizarPelicula(Pelicula pelicula);
        bool BorrarPelicula(Pelicula pelicula);
        bool Guardar();
    }
}
