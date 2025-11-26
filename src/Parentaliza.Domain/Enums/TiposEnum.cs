using System.ComponentModel.DataAnnotations;

namespace Parentaliza.Domain.Enums;

/// <summary>
/// Enum que representa o tipo de responsável pelo bebê
/// </summary>
public enum TipoResponsavel
{
    [Display(Name = "Mãe")]
    Mae = 1,

    [Display(Name = "Pai")]
    Pai = 2,

    [Display(Name = "Parente")]
    Parente = 3
}

/// <summary>
/// Enum que representa o sexo do bebê
/// </summary>
public enum Sexo
{
    [Display(Name = "Masculino")]
    Masculino = 1,

    [Display(Name = "Feminino")]
    Feminino = 2,

    [Display(Name = "Outro")]
    Outro = 3
}

/// <summary>
/// Enum que representa o tipo sanguíneo do bebê
/// </summary>
public enum TipoSanguineo
{
    [Display(Name = "A+")]
    APositivo = 1,

    [Display(Name = "A-")]
    ANegativo = 2,

    [Display(Name = "B+")]
    BPositivo = 3,

    [Display(Name = "B-")]
    BNegativo = 4,

    [Display(Name = "AB+")]
    ABPositivo = 5,

    [Display(Name = "AB-")]
    ABNegativo = 6,

    [Display(Name = "O+")]
    OPositivo = 7,

    [Display(Name = "O-")]
    ONegativo = 8
}

/// <summary>
/// Enum que representa o tipo de evento na agenda
/// </summary>
public enum TipoEvento
{
    [Display(Name = "Consulta")]
    Consulta = 1,

    [Display(Name = "Vacina")]
    Vacina = 2,

    [Display(Name = "Exame")]
    Exame = 3,

    [Display(Name = "Compromisso")]
    Compromisso = 4,

    [Display(Name = "Evento")]
    Evento = 5,

    [Display(Name = "Lembrete")]
    Lembrete = 6,

    [Display(Name = "Outro")]
    Outro = 7
}

/// <summary>
/// Enum que representa o status de um evento na agenda
/// </summary>
public enum StatusEvento
{
    [Display(Name = "Pendente")]
    Pendente = 1,

    [Display(Name = "Realizado")]
    Realizado = 2,

    [Display(Name = "Cancelado")]
    Cancelado = 3
}