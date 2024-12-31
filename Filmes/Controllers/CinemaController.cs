using AutoMapper;
using Filmes.Data;
using Filmes.Data.Dtos.Cinema;
using Filmes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Filmes.Controllers;

[ApiController]
[Route("[controller]")]
public class CinemaController : ControllerBase
{

    private FilmeContext _context;
    private IMapper _mapper;

    public CinemaController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionarCinema([FromBody] CreateCinemaDto cinemaDto)
    {
        Cinema cinema = _mapper.Map<Cinema>(cinemaDto);

        _context.Cinemas.Add(cinema);
        _context.SaveChanges();

        return CreatedAtAction(nameof(BuscarCinema),
               new { id = cinema.Id },
               cinema);
    }

    [HttpGet]
    public IEnumerable<ReadCinemaDto> ListarCinemas([FromQuery] int skip = 0,
        [FromQuery] int take = 10)
    {
        List<Cinema> cinemas = _context.Cinemas.Skip(skip).Take(take).ToList();

        List<ReadCinemaDto> cinemasDto = _mapper.Map<List<ReadCinemaDto>>(cinemas);
            
        return cinemasDto;
    }

    [HttpGet("{id}")]
    public IActionResult BuscarCinema(int id)
    {
        var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

        if (cinema == null) return NotFound();

        ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);

        return Ok(cinemaDto);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizarCinema(int id, 
        [FromBody] UpdateCinemaDto cinemaDto)
    {
        var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

        if (cinema == null) return NotFound();

        _mapper.Map(cinemaDto, cinema);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletarCinema(int id)
    {
        var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
        
        if (cinema == null) return NotFound();

        _context.Remove(cinema);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpGet("por-endereco/{enderecoId}")]
    public IEnumerable<ReadCinemaDto> ListarCinemasPorEndereco(int enderecoId)
    {
        List<Cinema> cinemas = _context.Cinemas.FromSqlRaw(
            $"SELECT Id, Nome, EnderecoId FROM cinemas c where c.EnderecoId = {enderecoId}")
            .ToList();

        List<ReadCinemaDto> cinemasDto = _mapper.Map<List<ReadCinemaDto>>(cinemas);

        return cinemasDto;
    }

}
