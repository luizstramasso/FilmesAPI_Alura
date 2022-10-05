using FilmesAPI.Data;
using FilmesAPI.Data.DTOs;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route( "[controller]" )]
    public class FilmeController : ControllerBase
    {
        private readonly FilmeContext _context;

        public FilmeController( FilmeContext context )
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AdicionarFilme( [FromBody] CreateFilmeDTO filmeDTO )
        {
            Filme filme = new()
            {
                Titulo = filmeDTO.Titulo,
                Genero = filmeDTO.Genero,
                Diretor = filmeDTO.Diretor,
                Duracao = filmeDTO.Duracao
            };

            _context.Add( filme );
            _context.SaveChanges();
            return CreatedAtAction( nameof( RecuperarFilmePorId ), new { filme.Id }, filme );
        }

        [HttpGet]
        public IActionResult RecuperarFilmes()
        {
            return Ok( _context.Filmes );
        }

        [HttpGet( "{id}" )]
        public IActionResult RecuperarFilmePorId( int id )
        {
            var filme = _context.Filmes.FirstOrDefault( filme => filme.Id == id );

            if( filme != null )
            {
                ReadFilmeDTO filmeDTO = new()
                {
                    Id = filme.Id,
                    Titulo = filme.Titulo,
                    Genero = filme.Genero,
                    Diretor = filme.Diretor,
                    Duracao = filme.Duracao,
                    HoraDaConsulta = DateTime.Now
                };

                return Ok( filmeDTO );
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut( "{id}" )]
        public IActionResult AtualizarFilme( int id, [FromBody] UpdateFilmeDTO filmeDTO )
        {
            Filme filme = _context.Filmes.FirstOrDefault( filme => filme.Id == id );

            if( filme == default )
            {
                return NotFound();
            }

            filme.Titulo = filmeDTO.Titulo;
            filme.Genero = filmeDTO.Genero;
            filme.Diretor = filmeDTO.Diretor;
            filme.Duracao = filmeDTO.Duracao;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete( "{id}" )]
        public IActionResult DeletarFilme( int id )
        {
            Filme filme = _context.Filmes.FirstOrDefault( x => x.Id == id );

            if( filme == default )
            {
                return NotFound();
            }

            _context.Remove( filme );
            _context.SaveChanges();

            return NoContent();
        }
    }
}
