using AutoMapper;
using Filmes.Data;
using Filmes.Data.Dtos.
    
    Sessao;
using Filmes.Models;
using Microsoft.AspNetCore.Mvc;

namespace Filmes.Controllers;

[ApiController]
[Route("[controller]")]
public class SessaoController : ControllerBase
{

    private FilmeContext _context;
    private IMapper _mapper;

    public SessaoController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionarSessao([FromBody] CreateSessaoDto sessaoDto)
    {
        Sessao sessao = _mapper.Map<Sessao>(sessaoDto);

        _context.Sessoes.Add(sessao);
        _context.SaveChanges();

        return CreatedAtAction(nameof(BuscarSessao),
               new { filmeId = sessao.FilmeId, cinemaId = sessao.CinemaId },
               sessao);
    }

    [HttpGet]
    public IEnumerable<ReadSessaoDto> ListarSessoes([FromQuery] int skip = 0,
        [FromQuery] int take = 10)
    {
        List<Sessao> sessoes = _context.Sessoes.Skip(skip).Take(take).ToList();

        List<ReadSessaoDto> sessoesDto = _mapper.Map<List<ReadSessaoDto>>(sessoes);
            
        return sessoesDto;
    }

    [HttpGet("{filmeId}/{cinemaId}")]
    public IActionResult BuscarSessao(int filmeId, int cinemaId)
    {
        var sessao = _context.Sessoes.FirstOrDefault(
            sessao => sessao.FilmeId == filmeId && sessao.CinemaId == cinemaId);

        if (sessao == null) return NotFound();

        ReadSessaoDto sessaoDto = _mapper.Map<ReadSessaoDto>(sessao);

        return Ok(sessaoDto);
    }

    [HttpDelete("{filmeId}/{cinemaId}")]
    public IActionResult DeletarSessao(int filmeId, int cinemaId)
    {
        var sessao = _context.Sessoes.FirstOrDefault(
            sessao => sessao.FilmeId == filmeId && sessao.CinemaId == cinemaId);

        if (sessao == null) return NotFound();

        _context.Remove(sessao);
        _context.SaveChanges();

        return NoContent();
    }

}
