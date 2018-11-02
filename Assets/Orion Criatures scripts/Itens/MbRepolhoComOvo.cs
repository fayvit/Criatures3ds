[System.Serializable]
/// <summary>
/// Classe responsavel pelo uso do repolho com ovo
/// </summary>
public class MbRepolhoComOvo : ItemDeEnergiaBase
{
    public MbRepolhoComOvo(int estoque = 1) : base(new ContainerDeCaracteristicasDeItem(nomeIDitem.repolhoComOvo)
    {
        valor = 40
    }
        )
    {
        Estoque = estoque;
        recuperaDoTipo = nomeTipos.Gas;
        valorDeRecuperacao = 40;
    }
}