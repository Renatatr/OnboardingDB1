using System.ComponentModel;

namespace Hospital.Dominio.Especialidades;

public enum Especialidade
{
    [Description("Clínico geral")]
    ClinicoGeral = 0,
    [Description("Ortopedista")]
    Ortopedista = 1,
    [Description("Ginecologista")]
    Ginecologista = 2,
    [Description("Otorrino")]
    Otorrino = 3,
    [Description("Cardiologia")]
    Cardiologia = 4,
    [Description("Neurologia")]
    Neurologia = 5,
    [Description("Psiquiatria")]
    Psiquiatria = 6,
    [Description("Endocrinologia")]
    Endocrinologia = 7,
    [Description("Oftalmologia")]
    Oftalmologia = 8,
    [Description("Dermatologia")]
    Dermatologia = 9
}