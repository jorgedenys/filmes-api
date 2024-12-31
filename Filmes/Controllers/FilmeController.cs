using AutoMapper;
using Filmes.Data;
using Filmes.Data.Dtos.Cinema;
using Filmes.Data.Dtos.Filme;
using Filmes.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Filmes.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{

    private FilmeContext _context;
    private IMapper _mapper;

    public FilmeController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionarFilme([FromBody] CreateFilmeDto filmeDto)
    {
        Filme filme = _mapper.Map<Filme>(filmeDto);

        _context.Filmes.Add(filme);
        _context.SaveChanges();

        return CreatedAtAction(nameof(BuscarFilme),
               new { id = filme.Id },
               filme);
    }

    [HttpGet]
    public IEnumerable<ReadFilmeDto> ListarFilmes([FromQuery] int skip = 0,
        [FromQuery] int take = 10)
    {
        return _mapper.Map<List<ReadFilmeDto>>(
            _context.Filmes.Skip(skip).Take(take).ToList());
    }

    [HttpGet("{id}")]
    public IActionResult BuscarFilme(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

        if (filme == null) return NotFound();

        ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme);

        return Ok(filmeDto);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizarFilme(int id, 
        [FromBody] UpdateFilmeDto filmeDto)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

        if (filme == null) return NotFound();

        _mapper.Map(filmeDto, filme);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletarFilme(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        
        if (filme == null) return NotFound();

        _context.Remove(filme);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpGet("por-cinema")]
    public IEnumerable<ReadFilmeDto> BuscarFilmesPorCinema(
        [FromQuery] string nomeCinema)
    {
        List<Filme> filmes = _context.Filmes
            .Where(filme => filme.Sessoes.Any(sessao => sessao.Cinema.Nome == nomeCinema))
            .ToList();

        List<ReadFilmeDto> filmesDto = _mapper.Map<List<ReadFilmeDto>>(filmes);

        return filmesDto;
    }

    [HttpGet("por-genero")]
    public IEnumerable<ReadFilmeDto> BuscarFilmesPorGenero(
        [FromQuery] string genero)
    {
        List<Filme> listaFilmes = _context.Filmes.ToList();

        var listaFilmesPorGenero = (from filme in listaFilmes 
                                    where filme.Genero == genero
                                    select filme)
                                    .ToList();

        List<ReadFilmeDto> filmesDto = _mapper.Map<List<ReadFilmeDto>>(listaFilmesPorGenero);

        return filmesDto;
    }

    [HttpGet("curta-metragem")]
    public IEnumerable<ReadFilmeDto> BuscarFilmesCurtaMetragem()
    {
        List<Filme> listaFilmes = _context.Filmes.ToList();

        List<Filme> listaFilmesCurtaMetragem = 
            listaFilmes.Where(filme => filme.Duracao <= 15).ToList();

        return _mapper.Map<List<ReadFilmeDto>>(listaFilmesCurtaMetragem);
    }

}
