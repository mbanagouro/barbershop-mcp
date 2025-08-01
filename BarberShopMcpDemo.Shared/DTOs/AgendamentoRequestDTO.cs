namespace BarberShopMcpDemo.Shared.DTOs;

public record AgendamentoRequestDTO(
    string Nome, 
    string Telefone, 
    string Profissional, 
    string Dia, 
    string Horario);
