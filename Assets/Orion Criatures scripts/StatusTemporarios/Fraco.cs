using UnityEngine;
using System.Collections;

public class Fraco : StatusTemporarioSimplesBase
{
    public override void Start()
    {
        if (CDoAfetado != null)
            ColocaAParticulaEAdicionaEsseStatus(DoJogo.particulasFraco.ToString(), CDoAfetado.transform);
    }
}
