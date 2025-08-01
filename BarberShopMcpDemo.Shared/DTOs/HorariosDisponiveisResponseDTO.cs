namespace BarberShopMcpDemo.Shared.DTOs;

public class HorariosDisponiveisResponseDTO : ResponseBaseDTO
{
    public string Profissional { get; set; }
    public DateOnly Dia { get; set; }
    public List<string> HorariosDisponiveis { get; set; }
}
