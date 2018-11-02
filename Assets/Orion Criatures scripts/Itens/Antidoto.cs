[System.Serializable]
public class Antidoto : ItemAntiStatusBase
{

    public Antidoto(int estoque = 1) : base(new ContainerDeCaracteristicasDeItem(nomeIDitem.antidoto)
    {
        valor = 10
    }
        )
    {
        Estoque = estoque;
        qualStatusRemover = TipoStatus.envenenado;
    }


}
