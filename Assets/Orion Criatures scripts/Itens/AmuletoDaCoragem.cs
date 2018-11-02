[System.Serializable]
public class AmuletoDaCoragem : ItemAntiStatusBase
{

    public AmuletoDaCoragem(int estoque = 1) : base(new ContainerDeCaracteristicasDeItem(nomeIDitem.amuletoDaCoragem)
    {
        valor = 10
    }
        )
    {
        Estoque = estoque;
        qualStatusRemover = TipoStatus.amedrontado;
    }


}
