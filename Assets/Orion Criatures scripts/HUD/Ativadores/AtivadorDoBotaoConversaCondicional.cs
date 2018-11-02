using UnityEngine;
using System.Collections;

public class AtivadorDoBotaoConversaCondicional: AtivadorDoBotaoConversa
{
    [SerializeField]private NPCfalasCondicionais npcCondicionais;
    // Use this for initialization
    new void Start()
    {
        npc = npcCondicionais;
        base.Start();
    }
}
