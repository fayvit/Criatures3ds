using UnityEngine;
using System.Collections;

[System.Serializable]
public class ItemModificadorDeAtributoConsumivel : ItemModificadorDeAtributoBase
{
    public ItemModificadorDeAtributoConsumivel(ContainerDeCaracteristicasDeItem cont) : base(cont) { }

    protected AtributoConsumivel ContaDeSubida(AtributoConsumivel A)
    {
        A = new AtributoConsumivel(
                A.Corrente,
                A.Taxa,
                A.Maximo + 4,
                A.ModMaximo
                );
        A.Corrente = A.Maximo;
        return A;
    }
}
