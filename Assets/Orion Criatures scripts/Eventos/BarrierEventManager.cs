using UnityEngine;
using System.Collections;
//[ExecuteInEditMode]
public class BarrierEventManager : BarrierEventBase
{
    [Space(5)]
    [SerializeField] private GameObject barreira;
    [SerializeField] private GameObject acaoEfetivada;
    [SerializeField] private GameObject finalizaAcao;
    [SerializeField] private string somDaFinalizacao;

    new void Update()
    {
        if (JaIniciaou)
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
                    TempoDecorrido += Time.deltaTime;
                    VeririqueSom(TempoDeEfetivaAcao);
                    if (TempoDecorrido > TempoDeEfetivaAcao)
                    {
                        TempoDecorrido = 0;
                        finalizaAcao.SetActive(true);
                        barreira.SetActive(false);
                        Estado = BarrierEventsState.apresentaFinalizaAcao;
                        EventAgregator.Publish(new StandardSendStringEvent(gameObject, somDaFinalizacao, EventKey.disparaSom));
                    }
                    break;
                case BarrierEventsState.apresentaFinalizaAcao:
                    TempoDecorrido += Time.deltaTime;
                    if (TempoDecorrido > TempoDoFinalizaAcao)
                    {
                        EventAgregator.Publish(new StandardSendStringEvent(gameObject, SoundEffectID.Item.ToString(), EventKey.disparaSom));
                        gameObject.SetActive(false);
                        VoltarAoFLuxoDeJogo();
                    }
                    break;
            }
            base.Update();
        }
        else
        {
            Start();
        }
    }

    protected override void EfetivadorDaAcao()
    {
        acaoEfetivada.SetActive(true);
        AplicadorDeCamera.cam.NovoFocoBasico(transform, 10, 10, true, UsarForwardDoObjeto);
    }
}
