using System.ComponentModel.DataAnnotations;

namespace Filmes.Data.Dtos.Cinema;

public class UpdateCinemaDto
{
    [Required(ErrorMessage = "O Nome do cinema é obrigatório.")]
    public string Nome { get; set; }

    public int EnderecoId { get; set; }

}