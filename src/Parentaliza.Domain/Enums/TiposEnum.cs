namespace Parentaliza.Domain.Enums;
public enum TiposEnum
{
    Mãe = 1,
    Pai = 2,
    Parente = 3
}
public enum SexoEnum
{
    Masculino = 1,
    Feminino = 2,
    Outro = 3
}
public enum TipoSanguineoEnum
{
    APositivo = 1,
    ANegativo = 2,
    BPositivo = 3,
    BNegativo = 4,
    ABPositivo = 5,
    ABNegativo = 6,
    OPositivo = 7,
    ONegativo = 8
}
public enum TipoEvento
{
    Consulta = 1,
    Vacina = 2,
    Exame = 3,
    Compromisso = 4,
    Evento = 5,
    Lembrete = 6,
    Outro = 7
}
public enum StatusEvento
{

    Pendente = 1,
    Realizado = 2,
    Cancelado = 3
}