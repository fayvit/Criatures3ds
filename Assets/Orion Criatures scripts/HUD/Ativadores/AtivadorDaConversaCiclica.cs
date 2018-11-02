using UnityEngine;
using System.Collections;

public class AtivadorDaConversaCiclica : AtivadorDoBotaoConversa
{

    [SerializeField] private NPCdeFalasCiclicas npcCiclico;
    // Use this for initialization
    new void Start()
    {
        npc = npcCiclico;
        base.Start();
    }
}
