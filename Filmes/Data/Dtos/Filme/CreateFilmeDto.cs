using System.ComponentModel.DataAnnotations;

namespace Filmes.Data.Dtos.Filme;

public class CreateFilmeDto
{
    [Required(ErrorMessage = "O Título do filme é obrigatório.")]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "O Gênero do filme é obrigatório.")]
    [StringLength(50, ErrorMessage = "O tamanho do Gênero não pode exceder 50 caracteres.")]
    public string Genero { get; set; }

    [Required(ErrorMessage = "A Duração do filme é obrigatória.")]
    [Range(1, 600, ErrorMessage = "A Duração deve ter entre 1 e 600 minutos.")]
    public int Duracao { get; set; }
}
