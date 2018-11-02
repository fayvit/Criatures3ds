[System.Serializable]
/// <summary>
/// Classe responsavel pelo uso do quartzo
/// </summary>
public class MbQuartzo : ItemDeEnergiaBase
{
    public MbQuartzo(int estoque = 1) : base(new ContainerDeCaracteristicasDeItem(nomeIDitem.quartzo)
    {
        valor = 40
    }
        )
    {
        Estoque = estoque;
        recuperaDoTipo = nomeTipos.Pedra;
        valorDeRecuperacao = 40;
    }
}