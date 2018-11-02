using UnityEngine;
using System.Collections;

public class AtivadorDoBotaoConversaComJogoDeCamera : AtivadorDoBotaoConversa
{

    [SerializeField]private ConversaComNpcMovimentandoCamera npcMov;
    // Use this for initialization
    new void Start()
    {
        npc = npcMov;
        base.Start();
    }
}
