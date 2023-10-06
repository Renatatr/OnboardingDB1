using Hospital.Dominio.Dto;
using Hospital.Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class PrescricaoController : ControllerBase
{
    private readonly IPrescricaoRepository _prescricaoRepository;

    public PrescricaoController(IPrescricaoRepository prescricaoRepository)
    {
        _prescricaoRepository = prescricaoRepository;
    }

    [HttpPost]
    public async Task<IActionResult> AdicionaPrescricao(PrescricaoDto dto, [FromServices] IArmazenadorPrescricao armazenadorPrescricao)
    {
        armazenadorPrescricao.Armazenar(dto);
        return Ok(dto);
    }

    [HttpGet]
    public IActionResult RecuperarPrescricao()
    {
        return Ok(_prescricaoRepository.SelecionarTodos());
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaPrescricaoPorId(int id)
    {
        var prescricao = _prescricaoRepository.SelecionarPorId(id);
        if (prescricao != null)
        {
            return Ok(prescricao);
        }
        return NotFound();
    }

    [HttpPut("atualizaPrescricao")]
    public IActionResult AtualizaPrescricao(PrescricaoDto prescricaoDto, [FromServices] IArmazenadorPrescricao armazenadorPrescricao)
    {
        var mensagem = armazenadorPrescricao.Armazenar(prescricaoDto);
        return Ok("Prescrição " + mensagem);
    }
}
