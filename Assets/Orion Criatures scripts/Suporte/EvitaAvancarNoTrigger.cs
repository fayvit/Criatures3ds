using UnityEngine;
using System.Collections;

public class EvitaAvancarNoTrigger
{
    public static void Evita()
    {
        GameController.g.Manager.AoHeroi();
        MonoBehaviour.Destroy(GameController.g.Manager.CriatureAtivo.gameObject);
        GameController.g.Manager.InserirCriatureEmJogo();
    }
}
