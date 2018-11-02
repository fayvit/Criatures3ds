[System.Serializable]
/// <summary>
/// Classe responsavel pelo uso do adubo
/// </summary>
public class MbAdubo : ItemDeEnergiaBase
{
    public MbAdubo(int estoque = 1) : base(new ContainerDeCaracteristicasDeItem(nomeIDitem.adubo)
    {
        valor = 40
    }
        )
    {
        Estoque = estoque;
        recuperaDoTipo = nomeTipos.Terra;
        valorDeRecuperacao = 40;
    }
}