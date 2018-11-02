[System.Serializable]
/// <summary>
/// Classe responsavel pelo uso do ventilador
/// </summary>
public class MbVentilador : ItemDeEnergiaBase
{
    public MbVentilador(int estoque = 1) : base(new ContainerDeCaracteristicasDeItem(nomeIDitem.ventilador)
    {
        valor = 40
    }
        )
    {
        Estoque = estoque;
        recuperaDoTipo = nomeTipos.Voador;
        valorDeRecuperacao = 40;
    }
}