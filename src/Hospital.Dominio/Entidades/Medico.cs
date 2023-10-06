using Hospital.Dominio.Base;
using Hospital.Dominio.Especialidades;
using Hospital.Dominio.Extension;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Text.RegularExpressions;

namespace Hospital.Dominio.Entidades;

public class Medico : IId
{
    public int Id { get; protected set; }
    public string Nome { get; private set; }
    public Especialidade Especialidade { get; private set; }
    public string DescricaoEspecialidades => Especialidade.ObterDescricaoFormatada();
    public string CPF { get; private set; }
    public string MascaraCPF => string.Format("{0:000\\.###\\.###-##}", long.Parse(CPF));
    public string CRM { get; private set; }

    public Medico()
    {
    }

    public Medico(string nome, Especialidade especialidade, string cpf, string crm)
    {
        Nome = nome;
        Especialidade = especialidade;
        //retirar caracter especial
        CPF = Regex.Replace(cpf, @"[^a-zA-Z0-9\s]", "");
        CRM = crm;
    }

    public void AlterarNome(string nome)
    {
        Nome = nome;
    }

    public void AlterarEspecialidade(Especialidade especialidade)
    {
        Especialidade = especialidade;
    }

    public void AlterarCPF(string cpf)
    {
        CPF = Regex.Replace(cpf, @"[^a-zA-Z0-9\s]", "");
    }

    public void AlterarCRM(string crm)
    {
        CRM = crm;
    }

    public void Validar()
    {
        ValidadorDeRegra.Novo()
            .Quando(string.IsNullOrEmpty(Nome), Resource.NomeInvalido)
            .Quando(!EnumExtensions.ContemOpcao(Especialidade), Resource.EspecialidadeInvalida)
            .Quando(!ValidaRegistrosOficiais.ValidateCPF(CPF), Resource.CPFInvalido)
            .Quando(!ValidaRegistrosOficiais.ValidateCRM(CRM), Resource.CRMInvalido)
            .DispararExcecaoSeExistir();
    }
}

