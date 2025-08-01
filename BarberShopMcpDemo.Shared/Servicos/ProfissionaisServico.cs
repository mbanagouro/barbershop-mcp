using BarberShopMcpDemo.Shared.Entidades;

namespace BarberShopMcpDemo.Shared.Servicos;

public class ProfissionaisServico
{
    public IEnumerable<Profissional> ObterProfissionais()
    {
        return BancoDadosMemoria.Profissionais;
    }
}
