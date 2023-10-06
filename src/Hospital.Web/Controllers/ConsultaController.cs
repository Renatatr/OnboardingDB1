using Hospital.Dominio.Dto;
using Hospital.Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ConsultaController : ControllerBase
{
    private readonly IConsultaRepository _consultaRepository;

    public ConsultaController(IConsultaRepository consultaRepository)
    {
        _consultaRepository = consultaRepository;
    }

    [HttpPost]
    public async Task<IActionResult> AdicionaConsulta(ConsultaDto consultaDto, [FromServices] IArmazenadorConsulta armazenadorConsulta)
    {
        armazenadorConsulta.Armazenar(consultaDto);
        return Ok(consultaDto);
    }

    [HttpGet("selecionarTodos")]
    public IActionResult RecuperarConsultas()
    {
        return Ok(_consultaRepository.SelecionarTodos());
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaConsultaPorId(int id)
    {
        var consulta = _consultaRepository.SelecionarPorId(id);
        if (consulta == null)
        {
            return NotFound("Consulta não encontrada");
        }
        return Ok(consulta);
    }

    [HttpPut("atualizarConsulta")]
    public IActionResult AtualizaConsulta(ConsultaDto dto, [FromServices] IArmazenadorConsulta armazenadorDeConsulta)
    {
        var mensagem = armazenadorDeConsulta.Armazenar(dto);
        return Ok("Consulta " + mensagem);
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaMedico(int id)
    {
        _consultaRepository.Excluir(id);
        return Ok("Médico Excluído!");
    }
}