using BarberShopMcpDemo.Shared.DTOs;

namespace BarberShopMcpDemo.Shared.Servicos;

public class AgendamentoServico
{
    public IEnumerable<AgendamentoResponseDTO> ObterAgendamentosPorCliente(string nomeCliente)
    {
        return BancoDadosMemoria.Agendamentos
            .Where(a => a.Nome.Equals(nomeCliente, StringComparison.OrdinalIgnoreCase))
            .Select(a => new AgendamentoResponseDTO
            {
                Nome = a.Nome,
                Profissional = a.Profissional,
                Dia = a.Dia,
                Horario = a.Horario
            });
    }

    public HorariosDisponiveisResponseDTO ObterHorariosDisponiveis(
        string profissional, 
        string dia)
    {   
        DateOnly diaConvertido;
        if (!DateOnly.TryParse(dia, out diaConvertido))
            return new() { Mensagem = "A data informada é inválida" };

        if (!BancoDadosMemoria.Profissionais.Any(x => x.Nome == profissional))
            return new() { Mensagem = "Profissional não encontrado." };

        var horariosOcupados = BancoDadosMemoria.Agendamentos
            .Where(a => a.Profissional == profissional && a.Dia == diaConvertido)
            .Select(a => a.Horario)
            .ToHashSet();

        var horariosLivres = BancoDadosMemoria.HorariosDisponiveis
            .Where(h => !horariosOcupados.Contains(h))
            .ToList();

        return new()
        {
            Sucesso = true,
            Profissional = profissional,
            Dia = diaConvertido,
            HorariosDisponiveis = horariosLivres
        };
    }

    public HorarioAgendadoResponseDTO AgendarHorario(AgendamentoRequestDTO requisicao)
    {
        if (!BancoDadosMemoria.Profissionais.Any(x => x.Nome == requisicao.Profissional))
            return new() { Mensagem = "Profissional não encontrado." };

        if (!BancoDadosMemoria.HorariosDisponiveis.Contains(requisicao.Horario))
            return new() { Mensagem = "Horário inválido." };

        DateOnly diaConvertido;
        if (!DateOnly.TryParse(requisicao.Dia, out diaConvertido))
            return new() { Mensagem = "A data informada é inválida" };

        var existe = BancoDadosMemoria.Agendamentos.Any(a =>
            a.Profissional == requisicao.Profissional &&
            a.Dia == diaConvertido &&
            a.Horario == requisicao.Horario);

        if (existe)
            return new() { Mensagem = "Horário já agendado para este profissional e dia." };

        BancoDadosMemoria.Agendamentos.Add(new Entidades.Agendamento
        {
            Nome = requisicao.Nome,
            Telefone = requisicao.Telefone,
            Profissional = requisicao.Profissional,
            Dia = diaConvertido,
            Horario = requisicao.Horario
        });

        return new() 
        { 
            Sucesso = true, 
            Mensagem = "Agendamento realizado com sucesso!" 
        };
    }

    public AgendamentoRemovidoResponseDTO RemoverAgendamento(AgendamentoRemoverRequestDTO requisicao)
    {
        DateOnly diaConvertido;
        if (!DateOnly.TryParse(requisicao.Dia, out diaConvertido))
            return new() { Mensagem = "A data informada é inválida" };

        var agendamento = BancoDadosMemoria.Agendamentos.FirstOrDefault(a =>
            a.Nome.Equals(requisicao.Nome, StringComparison.OrdinalIgnoreCase) &&
            a.Dia == diaConvertido &&
            a.Horario == requisicao.Horario);

        if (agendamento != null)
        {
            BancoDadosMemoria.Agendamentos.Remove(agendamento);
        }

        return new()
        {
            Sucesso = agendamento != null,
            Mensagem = agendamento != null
                ? "Agendamento removido com sucesso!"
                : "Agendamento não encontrado.",
            Nome = agendamento?.Nome,
            Profissional = agendamento?.Profissional,
            Dia = agendamento?.Dia ?? default,
            Horario = agendamento?.Horario ?? string.Empty
        };
    }

    public void RemoverTodosAgendamentos(string nomeCliente)
    {
        BancoDadosMemoria.Agendamentos.RemoveAll(a => 
            a.Nome.Equals(nomeCliente, StringComparison.OrdinalIgnoreCase));
    }
}

