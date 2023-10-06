using Hospital.Dominio.Dto;
using Hospital.Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class MedicoController : ControllerBase
{
    private readonly IMedicoRepository _medicoRepository;

    public MedicoController(IMedicoRepository medicoRepository)
    {
        _medicoRepository = medicoRepository;
    }

    [HttpPost]
    public async Task<IActionResult> AdicionaMedico(MedicoDto medicoDto, [FromServices] IArmazenadorMedico armazenadorDeMedico)
    {
        armazenadorDeMedico.Armazenar(medicoDto);
        return Ok(medicoDto);
    }

    [HttpGet("selecionarTodos")]
    public IActionResult RecuperarMedicos()
    {
        return Ok(_medicoRepository.SelecionarTodos());
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaMedicoPorId(int id)
    {
        var medico = _medicoRepository.SelecionarPorId(id);
        if (medico == null)
        {
            return NotFound("Médico não encontrado");
        }
        return Ok(medico);
    }

    [HttpPut("atualizarMedico")]
    public IActionResult AtualizaMedico(MedicoDto dto, [FromServices] IArmazenadorMedico armazenadorDeMedico)
    {
        var mensagem = armazenadorDeMedico.Armazenar(dto);
        return Ok("Médico " + mensagem);
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaMedico(int id)
    {
        _medicoRepository.Excluir(id);
        return Ok("Médico Excluído!");
    }
}
