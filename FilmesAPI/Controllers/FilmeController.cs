using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
        public IActionResult AdicionarFilme( [FromBody] Filme filme )
        {
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
                return Ok( filme );
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut( "{id}" )]
        public IActionResult AtualizarFilme( int id, [FromBody] Filme filmeAtualizado )
        {
            var filme = _context.Filmes.FirstOrDefault( filme => filme.Id == id );

            if( filme == default )
            {
                return NotFound();
            }

            filme.Titulo = filmeAtualizado.Titulo;
            filme.Genero = filmeAtualizado.Genero;
            filme.Diretor = filmeAtualizado.Diretor;
            filme.Duracao = filmeAtualizado.Duracao;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete( "{id}" )]
        public IActionResult DeletarFilme( int id )
        {
            var filme = _context.Filmes.FirstOrDefault( x => x.Id == id );

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
