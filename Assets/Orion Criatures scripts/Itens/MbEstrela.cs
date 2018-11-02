[System.Serializable]
/// <summary>
/// Classe responsavel pelo uso da estrela para normais
/// </summary>
public class MbEstrela : ItemDeEnergiaBase
{
    public MbEstrela(int estoque = 1) : base(new ContainerDeCaracteristicasDeItem(nomeIDitem.estrela)
    {
        valor = 40
    }
        )
    {
        Estoque = estoque;
        recuperaDoTipo = nomeTipos.Normal;
        valorDeRecuperacao = 40;
    }
}