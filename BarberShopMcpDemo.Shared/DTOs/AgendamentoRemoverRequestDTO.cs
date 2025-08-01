namespace BarberShopMcpDemo.Shared.DTOs;

public record AgendamentoRemoverRequestDTO(
    string Nome,
    string Dia, 
    string Horario);
