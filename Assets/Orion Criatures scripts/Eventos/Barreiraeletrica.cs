using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barreiraeletrica : BarrierBarEventBase
{
    [SerializeField] private string somDaParticula;
    [SerializeField] private GameObject particulaDoAcionamento;
    [SerializeField] private GameObject raioDoMotor;

    protected override void AtivacaoEspecifica()
    {
        VeririqueSom(TempoDeEfetivaAcao, somDaParticula);

        base.AtivacaoEspecifica();

        if (Estado == BarrierEventsState.barrasDescendo)
            NumJaRepetidos = 0;


    }
    protected override void EfetivadorDaAcao()
    {
        AplicadorDeCamera.cam.NovoFocoBasico(transform, 10, 10, true, UsarForwardDoObjeto);
        raioDoMotor.SetActive(true);
    }

    public override void DisparaEvento(nomesGolpes nomeDoGolpe)
    {
        if (EsseGolpeAtiva(nomeDoGolpe))
            particulaDoAcionamento.SetActive(true);

        if (!GameController.g.MyKeys.VerificaAutoShift(ID))
            base.DisparaEvento(nomeDoGolpe);
    }

}
