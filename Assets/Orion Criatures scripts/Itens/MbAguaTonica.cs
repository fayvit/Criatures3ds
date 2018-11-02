[System.Serializable]
/// <summary>
/// Classe responsavel pelo uso da AguaComGas
/// </summary>
public class MbAguaTonica : ItemDeEnergiaBase
{
    public MbAguaTonica(int estoque = 1) : base(new ContainerDeCaracteristicasDeItem(nomeIDitem.aguaTonica)
    {
        valor = 40
    }
        )
    {
        Estoque = estoque;
        recuperaDoTipo = nomeTipos.Agua;
        valorDeRecuperacao = 40;
    }
}