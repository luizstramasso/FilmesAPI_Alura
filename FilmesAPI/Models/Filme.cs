using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models
{
    public class Filme
    {
        [Required( ErrorMessage = "O campo título é obrigatório" )]
        public string Titulo { get; set; }

        [Required( ErrorMessage = "O campo diretor é obrigatório" )]
        public string Diretor { get; set; }

        [StringLength( 30, ErrorMessage = "O campo gênero não pode passar de 30 caracteres" )]
        public string Genero { get; set; }

        [Range( 1, 300, ErrorMessage = "A duração deve ter entre 1 e 300 minutos" )]
        public int Duracao { get; set; }
    }
}