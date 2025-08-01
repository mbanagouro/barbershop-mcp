using BarberShopMcpDemo.McpServer;
using BarberShopMcpDemo.Shared.Servicos;
using ModelContextProtocol.Protocol;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ProfissionaisServico>();
builder.Services.AddScoped<AgendamentoServico>();

// Add services to the container.
var serverInfo = new Implementation { Name = "BarberShopMcpDemo.McpServer", Version = "1.0.0" };
builder.Services
    .AddMcpServer(mcp =>
    {
        mcp.ServerInfo = serverInfo;
    })
    .WithHttpTransport()
    .WithToolsFromAssembly(typeof(AgendamentoTools).Assembly);

var app = builder.Build();

app.MapMcp();

await app.RunAsync();
