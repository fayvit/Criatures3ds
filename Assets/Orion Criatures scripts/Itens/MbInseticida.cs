[System.Serializable]
/// <summary>
/// Classe responsavel pelo uso do inseticida
/// </summary>
public class MbInseticida : ItemDeEnergiaBase
{
    public MbInseticida(int estoque = 1) : base(new ContainerDeCaracteristicasDeItem(nomeIDitem.inseticida)
    {
        valor = 40
    }
        )
    {
        Estoque = estoque;
        recuperaDoTipo = nomeTipos.Veneno;
        valorDeRecuperacao = 40;
    }
}