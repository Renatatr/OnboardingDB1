using Hospital.Dominio.Dto;
using Hospital.Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class PacienteController : ControllerBase
{
    private readonly IPacienteRepository _pacienteRepository;

    public PacienteController(IPacienteRepository pacienteRepository)
    {
        _pacienteRepository = pacienteRepository;
    }

    [HttpPost]
    public async Task<IActionResult> AdicionaPaciente(PacienteDto pacienteDto, [FromServices] IArmazenadorPaciente armazenadorPaciente)
    {
        armazenadorPaciente.Armazenar(pacienteDto); 
        return Ok(pacienteDto);
    }

    [HttpGet("selecionaTodos")]
    public IActionResult RecuperarPacientes()
    {
        return Ok(_pacienteRepository.SelecionarTodos());
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaPacientePorId(int id)
    {
        var paciente = _pacienteRepository.SelecionarPorId(id);
        if (paciente != null)
        {
            return Ok(paciente);
        }
        return NotFound("Paciente não encontrado");
    }

    [HttpPut("atualizaPaciente")]
    public IActionResult AtualizaMedico(PacienteDto pacienteDto, [FromServices] IArmazenadorPaciente armazenadorPaciente)
    {
        var mensagem = armazenadorPaciente.Armazenar(pacienteDto);
        return Ok("Paciente " + mensagem);
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaPaciente(int id)
    {
        _pacienteRepository.Excluir(id);
        return Ok("Paciente excluído");
    }
}