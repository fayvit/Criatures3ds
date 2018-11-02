[System.Serializable]
public class Tonico : ItemAntiStatusBase
{

    public Tonico(int estoque = 1) : base(new ContainerDeCaracteristicasDeItem(nomeIDitem.tonico)
    {
        valor = 10
    }
        )
    {
        Estoque = estoque;
        qualStatusRemover = TipoStatus.fraco;
    }


}
