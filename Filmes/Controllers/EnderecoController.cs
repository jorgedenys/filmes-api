using AutoMapper;
using Filmes.Data;
using Filmes.Models;
using Microsoft.AspNetCore.Mvc;
using Filmes.Data.Dtos.Endereco;

namespace Filmes.Controllers;

[ApiController] 
[Route("[controller]")]
public class EnderecoController : ControllerBase
{

    private FilmeContext _context;
    private IMapper _mapper;

    public EnderecoController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionarEndereco([FromBody] CreateEnderecoDto enderecoDto)
    {
        Endereco endereco = _mapper.Map<Endereco>(enderecoDto);

        _context.Enderecos.Add(endereco);
        _context.SaveChanges();

        return CreatedAtAction(nameof(BuscarEndereco),
               new { id = endereco.Id },
               endereco);
    }

    [HttpGet]
    public IEnumerable<ReadEnderecoDto> ListarEnderecos([FromQuery] int skip = 0,
        [FromQuery] int take = 10)
    {
        return _mapper.Map<List<ReadEnderecoDto>>(
            _context.Enderecos.Skip(skip).Take(take));
    }

    [HttpGet("{id}")]
    public IActionResult BuscarEndereco(int id)
    {
        var endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);

        if (endereco == null) return NotFound();

        ReadEnderecoDto enderecoDto = _mapper.Map<ReadEnderecoDto>(endereco);

        return Ok(enderecoDto);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizarEndereco(int id,
        [FromBody] UpdateEnderecoDto enderecoDto)
    {
        var endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);

        if (endereco == null) return NotFound();

        _mapper.Map(enderecoDto, endereco);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletarEndereco(int id)
    {
        var endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);

        if (endereco == null) return NotFound();

        _context.Remove(endereco);
        _context.SaveChanges();

        return NoContent();
    }

}
