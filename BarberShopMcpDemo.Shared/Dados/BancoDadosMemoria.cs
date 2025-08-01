using BarberShopMcpDemo.Shared.Entidades;

namespace BarberShopMcpDemo.Shared;

public static class BancoDadosMemoria
{
    public static readonly List<Profissional> Profissionais = new() {
        new Profissional
        {
            Nome = "João",
            Experiencia = "5 anos de experiência em corte e coloração.",
            Especialidades = new List<string> { "Corte", "Coloração", "Penteados" }
        },
        new Profissional
        {
            Nome = "Maria",
            Experiencia = "3 anos de experiência em manicure e pedicure.",
            Especialidades = new List<string> { "Manicure", "Pedicure", "Design de Unhas" }
        },
        new Profissional
        {
            Nome = "Carlos",
            Experiencia = "10 anos de experiência em barbearia.",
            Especialidades = new List<string> { "Barba", "Corte Masculino", "Penteados Masculinos" }
        }
    };

    public static readonly List<string> HorariosDisponiveis = new() {
        "09:00",
        "10:00",
        "11:00",
        "14:00",
        "15:00"
    };

    public static readonly List<Agendamento> Agendamentos = new();
}
