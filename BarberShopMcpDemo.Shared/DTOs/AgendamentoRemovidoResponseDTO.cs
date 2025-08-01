namespace BarberShopMcpDemo.Shared.DTOs;

public class AgendamentoRemovidoResponseDTO : ResponseBaseDTO
{
    public string Nome { get; set; }
    public string Profissional { get; set; }
    public DateOnly Dia { get; set; }
    public string Horario { get; set; }
}