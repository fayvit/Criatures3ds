using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoinhoDeVento : BarrierBarEventBase
{
    [SerializeField] private Transform parteQueGira;

    private float velDeGiro = 0;
    private float velAlvoDoGiro = -400;

    private EstadoDoCatavento estadoC = EstadoDoCatavento.neutro;

    private enum EstadoDoCatavento
    {
        neutro,
        acelerandoGiro,
        acelerado,
        desacelerandoGiro
    }

    protected override void AtivacaoEspecifica()
    {

        velDeGiro = Mathf.Lerp(0, velAlvoDoGiro, 1.5f * TempoDecorrido / TempoDeEfetivaAcao);
        parteQueGira.Rotate(parteQueGira.forward, velDeGiro * Time.deltaTime, Space.World);

        base.AtivacaoEspecifica();
    }

    protected override void BarraDescendo()
    {
        parteQueGira.Rotate(parteQueGira.forward, velDeGiro * Time.deltaTime, Space.World);
        base.BarraDescendo();
    }

    protected override void ApresentacaoDeFinalizacaoEspecifica()
    {
        velDeGiro = Mathf.Lerp(velAlvoDoGiro, 0, 1.5f * TempoDecorrido / TempoDoFinalizaAcao);
        parteQueGira.Rotate(parteQueGira.forward, velDeGiro * Time.deltaTime, Space.World);

        base.ApresentacaoDeFinalizacaoEspecifica();
    }

    /*
    protected override void CaseDoNaoFeito()
    {
        switch (Estado)
        {
            case BarrierEventsState.mensAberta:
                if (ActionManager.ButtonUp(0, GameController.g.Manager.Control))
                {
                    AcaoDeMensAberta();
                }
            break;
            case BarrierEventsState.ativou:
                AtivacaoEspecifica();
            break;
            case BarrierEventsState.barrasDescendo:
                VeririqueSom(tempoDeBarra);
                TempoDecorrido += Time.deltaTime;
                barras.position = Vector3.Lerp(originalPosition, hidePosition, TempoDecorrido / tempoDeBarra);
                
                if (TempoDecorrido > tempoDeBarra)
                {
                    TempoDecorrido = 0;
                    ParticulaDaAcao.SetActive(false);
                    Destroy(GetComponent<BoxCollider>());
                    Estado = BarrierEventsState.apresentaFinalizaAcao;
                }
            break;
            case BarrierEventsState.apresentaFinalizaAcao:
                
                TempoDecorrido += Time.deltaTime;
                if (TempoDecorrido > TempoDoFinalizaAcao)
                {
                    EventAgregator.Publish(new StandardSendStringEvent(gameObject, SoundEffectID.Item.ToString(), EventKey.disparaSom));
                    Estado = BarrierEventsState.emEspera;
                    VoltarAoFLuxoDeJogo();
                }
            break;
        }
    }*/

    protected override void CaseDoFeito()
    {
        switch (estadoC)
        {
            case EstadoDoCatavento.acelerandoGiro:
                TempoDecorrido += Time.deltaTime;

                velDeGiro = Mathf.Lerp(0, velAlvoDoGiro, 1.5f * TempoDecorrido / TempoDeEfetivaAcao);
                parteQueGira.Rotate(parteQueGira.forward, velDeGiro * Time.deltaTime, Space.World);

                if (TempoDecorrido > TempoDeEfetivaAcao)
                {
                    TempoDecorrido = 0;
                    estadoC = EstadoDoCatavento.desacelerandoGiro;
                }
                break;
            case EstadoDoCatavento.acelerado:
                TempoDecorrido += Time.deltaTime;
                parteQueGira.Rotate(parteQueGira.forward, velAlvoDoGiro * Time.deltaTime, Space.World);
                if (TempoDecorrido > TempoDeEfetivaAcao)
                {
                    TempoDecorrido = 0;
                    estadoC = EstadoDoCatavento.desacelerandoGiro;
                }
                break;
            case EstadoDoCatavento.desacelerandoGiro:
                velDeGiro = Mathf.Lerp(velAlvoDoGiro, 0, 1.5f * TempoDecorrido / TempoDoFinalizaAcao);
                parteQueGira.Rotate(parteQueGira.forward, velDeGiro * Time.deltaTime, Space.World);
                TempoDecorrido += Time.deltaTime;
                if (TempoDecorrido > TempoDoFinalizaAcao)
                {
                    estadoC = EstadoDoCatavento.neutro;
                }

                break;
        }
    }

    public override void DisparaEvento(nomesGolpes nomeDoGolpe)
    {
        if (GameController.g.MyKeys.VerificaAutoShift(ID))
        {
            TempoDecorrido = 0;
            if (estadoC == EstadoDoCatavento.neutro)
                estadoC = EstadoDoCatavento.acelerandoGiro;
            else
                estadoC = EstadoDoCatavento.acelerado;
        }
        else
            base.DisparaEvento(nomeDoGolpe);
    }

    protected override void EfetivadorDaAcao()
    {
        AplicadorDeCamera.cam.NovoFocoBasico(parteQueGira, 10, 10, true, UsarForwardDoObjeto);
    }


}
