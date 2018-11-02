using UnityEngine;
using System.Collections;

public class StatusTemporarioSimplesBase : StatusTemporarioBase
{
    private float tempoAcumulado = 0;
    public override void Update()
    {
        tempoAcumulado += Time.deltaTime;

        if (tempoAcumulado >= Dados.TempoSignificativo||OAfetado.CaracCriature.meusAtributos.PV.Corrente<=0)
        {
            RetiraComponenteStatus();
        }
    }
}
