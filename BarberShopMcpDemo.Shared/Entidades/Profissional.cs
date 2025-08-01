namespace BarberShopMcpDemo.Shared.Entidades;

public class Profissional
{
    public string Nome { get; set; }
    public string Experiencia { get; set; } = string.Empty;
    public List<string> Especialidades { get; set; } = new();
}
