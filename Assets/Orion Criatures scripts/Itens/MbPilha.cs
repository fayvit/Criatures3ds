[System.Serializable]
/// <summary>
/// Classe responsavel pelo uso da Pilha
/// </summary>
public class MbPilha : ItemDeEnergiaBase
{
    public MbPilha(int estoque = 1) : base(new ContainerDeCaracteristicasDeItem(nomeIDitem.pilha)
    {
        valor = 40
    }
        )
    {
        Estoque = estoque;
        recuperaDoTipo = nomeTipos.Eletrico;
        valorDeRecuperacao = 40;
    }
}