using BarberShopMcpDemo.Shared.DTOs;
using BarberShopMcpDemo.Shared.Servicos;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;

namespace BarberShopMcpDemo.McpServer;

[McpServerToolType]
public class AgendamentoTools
{
    [McpServerTool, 
        Description("Lista os profissionais da barbearia")]
    public static string ObterProfissionais(ProfissionaisServico servico)
    {
        return JsonSerializer.Serialize(servico.ObterProfissionais());
    }

    [McpServerTool,
        Description("Lista os horarios disponíveis para agendamento por dia e profissional")]
    public static string ObterHorariosDisponiveis(AgendamentoServico servico,
        [Description("Nome do profissional da barbearia")] string profissional,
        [Description("Dia (yyyy-mm-dd) que o cliente deseja agendar com o profissional da barbearia.")] string dia)
    {
        var retorno = servico.ObterHorariosDisponiveis(profissional, dia);
        if (!retorno.Sucesso)
            return retorno.Mensagem;

        return JsonSerializer.Serialize(retorno);
    }

    [McpServerTool,
        Description("Agenda dia e horário na barbearia com um determinado profissional")]
    public static string AgendarHorario(AgendamentoServico servico,
        [Description("Dados do agendamento: nome do cliente, telefone do cliente, profissional, dia (yyyy-mm-dd) e hora (hh:mm)")] AgendamentoRequestDTO requisicao)
    {
        var retorno = servico.AgendarHorario(requisicao);
        if (!retorno.Sucesso)
            return retorno.Mensagem;

        return JsonSerializer.Serialize(retorno);
    }

    [McpServerTool,
        Description("Lista os agendamentos de um determinado cliente")]
    public static string ObterAgendamentos(AgendamentoServico servico, string nomeCliente)
    {
        return JsonSerializer.Serialize(servico.ObterAgendamentosPorCliente(nomeCliente));
    }

    [McpServerTool,
        Description("Remove o agendamento de um cliente pelo nome, dia e horario")]
    public static string RemoveAgendamento(AgendamentoServico servico,
        [Description("Dados do agendamento para exclusão: nome do cliente, dia (yyyy-mm-dd) e hora (hh:mm)")] AgendamentoRemoverRequestDTO requisicao)
    {
        var retorno = servico.RemoverAgendamento(requisicao);
        if (!retorno.Sucesso)
            return retorno.Mensagem;

        return JsonSerializer.Serialize(retorno);
    }

    [McpServerTool,
        Description("Remove todos os agendamentos de um cliente pelo nome")]
    public static string RemoverTodosAgendamentos(AgendamentoServico servico,
        [Description("Nome do cliente")] string nomeCliente)
    {
        servico.RemoverTodosAgendamentos(nomeCliente);
        return $"Agendamentos removidos para o cliente {nomeCliente}";
    }
}
