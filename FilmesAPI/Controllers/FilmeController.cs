using AutoMapper;
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
        private readonly IMapper _mapper;

        public FilmeController( FilmeContext context, IMapper mapper )
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionarFilme( [FromBody] CreateFilmeDTO filmeDTO )
        {
            Filme filme = _mapper.Map<Filme>( filmeDTO );

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
                ReadFilmeDTO filmeDTO = _mapper.Map<ReadFilmeDTO>( filme );

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

            _mapper.Map( filmeDTO, filme );
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
