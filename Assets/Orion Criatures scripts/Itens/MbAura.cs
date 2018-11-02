[System.Serializable]
/// <summary>
/// Classe responsavel pelo uso da Aura dos criatures psiquicos
/// </summary>
public class MbAura : ItemDeEnergiaBase
{
    public MbAura(int estoque = 1) : base(new ContainerDeCaracteristicasDeItem(nomeIDitem.aura)
    {
        valor = 40
    }
        )
    {
        Estoque = estoque;
        recuperaDoTipo = nomeTipos.Psiquico;
        valorDeRecuperacao = 40;
    }
}