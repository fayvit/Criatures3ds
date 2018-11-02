using UnityEngine;
using System.Collections;

public class MbAlternancia
{
    public static void AoCriature(CreatureManager C,CreatureManager inimigo = null)
    {
        
        if (inimigo!=null)
        {
            AplicadorDeCamera aCam = AplicadorDeCamera.cam;
            aCam.InicializaCameraDeLuta(C, inimigo.transform);
            C.Estado = CreatureManager.CreatureState.emLuta;
            inimigo.Estado = CreatureManager.CreatureState.selvagem;
            GameController.g.HudM.ModoCriature(true);
        }
        else
        {
            GameController.g.HudM.ModoCriature(false);
            AplicadorDeCamera.cam.FocarBasica(C.transform, C.MeuCriatureBase.alturaCamera, C.MeuCriatureBase.distanciaCamera);
            C.Estado = CreatureManager.CreatureState.aPasseio;                        
        }

        C.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
    }

    public static void AoHeroi(CharacterManager manager/*,bool retornaCamera = true*/)
    {
        AplicadorDeCamera.cam.FocarBasica(manager.transform, 3, 7);

        CreatureManager C = manager.CriatureAtivo;

        if (C != null)
        {
            C.Estado = CreatureManager.CreatureState.seguindo;
            if(C.GetComponent<UnityEngine.AI.NavMeshAgent>())
                C.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
        }
        manager.Estado = EstadoDePersonagem.aPasseio;

        if(GameController.g.MyKeys.VerificaAutoShift(KeyShift.estouNoTuto))
            GameController.g.HudM.ModoHeroi();
        else
            GameController.g.HudM.ModoLimpo();
    }
}
