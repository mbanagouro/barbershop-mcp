namespace BarberShopMcpDemo.Shared.Entidades;

public class Agendamento
{
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string Profissional { get; set; }
    public DateOnly Dia { get; set; }
    public string Horario { get; set; }
}
