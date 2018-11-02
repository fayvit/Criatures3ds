using UnityEngine;
using System.Collections;

[System.Serializable]
public class CanetaSagradaDeLog : ItemNaoUsavel
{
    public CanetaSagradaDeLog(int estoque) : base(new ContainerDeCaracteristicasDeItem(nomeIDitem.canetaSagradaDeLog)
    {
        valor = 0
    }
        )
    {
        Estoque = estoque;
    }

}
