using Hospital.Dados.Repositorios;
using Hospital.Dominio.Dto;
using Hospital.Dominio.Interfaces;
using Hospital.Dominio.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class TratamentoController : ControllerBase
{
    private readonly ITratamentoRepository _tratamentoRepository;

    public TratamentoController(ITratamentoRepository tratamentoRepository)
    {
        _tratamentoRepository = tratamentoRepository;
    }

    [HttpPost]
    public IActionResult AdicionaTratamentoDto(TratamentoDto tratamentodto, [FromServices] IArmazenadorTratamento armazenadorTratamento)
    {
        armazenadorTratamento.Armazenar(tratamentodto);
        return Ok(tratamentodto);
    }

    [HttpGet]
    public IActionResult RecuperarTratamento()
    {
        return Ok(_tratamentoRepository.SelecionarTodos());
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaTratamentoPorId(int id)
    {
        var tratamentos = _tratamentoRepository.SelecionarPorId(id);
        if (tratamentos != null)
        {
            return Ok(tratamentos);
        }

        return NotFound();
    }

    [HttpPut("atualizarTratamento")]
    public IActionResult AtualizaTratamento(TratamentoDto dto, [FromServices] IArmazenadorTratamento armazenadorDeTratamento)
    {
        var mensagem = armazenadorDeTratamento.Armazenar(dto);
        return Ok("Médico " + mensagem);
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaTratamento(int id)
    {
        _tratamentoRepository.Excluir(id);
        return Ok("Tratamento Excluído!");
    }
}
