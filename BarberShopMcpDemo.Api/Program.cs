using BarberShopMcpDemo.Shared.DTOs;
using BarberShopMcpDemo.Shared.Servicos;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ProfissionaisServico>();
builder.Services.AddScoped<AgendamentoServico>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

// Map endpoints
app.MapGet("/api/profissionais", (
    [FromServices] ProfissionaisServico servico) =>
{
    return Results.Ok(servico.ObterProfissionais());
});

app.MapGet("/api/horarios", (
    [FromServices] AgendamentoServico servico, 
    [FromQuery] string profissional, 
    [FromQuery] string dia) =>
{
    var retorno = servico.ObterHorariosDisponiveis(profissional, dia);
    if (!retorno.Sucesso)
        return Results.BadRequest(retorno.Mensagem);

    return Results.Ok(retorno);
});

app.MapPost("/api/agendar", (
    [FromServices] AgendamentoServico servico, 
    [FromBody] AgendamentoRequestDTO request) =>
{
    var retorno = servico.AgendarHorario(request);
    if (!retorno.Sucesso)
        return Results.BadRequest(retorno.Mensagem);

    return Results.Ok(retorno.Mensagem);
});

app.MapGet("/api/agendamento", (
    [FromServices] AgendamentoServico servico, 
    [FromQuery] string nomeCliente) =>
{
    return Results.Ok(servico.ObterAgendamentosPorCliente(nomeCliente));
});

app.MapDelete("/api/agendamento", (
    [FromServices] AgendamentoServico servico, 
    [FromBody] AgendamentoRemoverRequestDTO requisicao) =>
{
    var retorno = servico.RemoverAgendamento(requisicao);
    if (!retorno.Sucesso)
        return Results.BadRequest(retorno.Mensagem);

    return Results.Ok(retorno);
});

app.MapDelete("/api/agendamento/todos", (
    [FromServices] AgendamentoServico servico, 
    [FromQuery] string nomeCliente) =>
{
    servico.RemoverTodosAgendamentos(nomeCliente);
    return Results.Ok($"Agendamentos removidos para o cliente {nomeCliente}");
});

app.Run();