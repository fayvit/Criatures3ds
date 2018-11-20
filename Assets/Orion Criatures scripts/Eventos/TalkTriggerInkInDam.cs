using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkTriggerInkInDam : MonoBehaviour {

	[SerializeField] private string ID;
    [SerializeField] private Transform alvos;
    [SerializeField] private Transform posFinalDoNpc;
    [SerializeField] private Transform posInicialDoNpc;
    [SerializeField] private Transform posDeFugaDoNpc;
    [SerializeField] private TalkManagerState state = TalkManagerState.emEspera;
    [SerializeField] private Animator animatorDoNPC;
    [SerializeField] private NPCdeConversa npc;
    [SerializeField] private AudioClip clip;

    private ControlledMoveForCharacter controll;
    private ControlledMoveForCharacter controllNpc;

    private enum TalkManagerState
    {
        emEspera,
        movimentandoHeroi,
        movimentandoNpc,
        conversando,
        npcEmFuga,
        finalizado
    }

	// Use this for initialization
	void Start () {
       
    }

    private void OnDestroy()
    {
        
    }

    // Update is called once per frame
    void Update () {
        switch (state)
        {
            case TalkManagerState.movimentandoHeroi:
                Movimentando();
            break;
            case TalkManagerState.movimentandoNpc:
                MovimentandoNpc();
            break;
            case TalkManagerState.conversando:
                if (npc.Update())
                {
                    controllNpc.ModificarOndeChegar(posDeFugaDoNpc.position,6);
                    state = TalkManagerState.npcEmFuga;
                }
            break;
            case TalkManagerState.npcEmFuga:
                if (controllNpc.UpdatePosition())
                {
                    FinalizandoConversa();
                }
            break;
        }
	}

    void FinalizandoConversa()
    {
        GlobalController.g.Musica.IniciarMusica(LocalResources.l.Mlocal.Musica, LocalResources.l.Mlocal.Volume);

        CharacterManager m = GameController.g.Manager;

        m.AoHeroi();        

        state = TalkManagerState.finalizado;

        GameController.g.MyKeys.MudaAutoShift(ID, true);

        animatorDoNPC.gameObject.SetActive(false);
    }

    void Movimentando()
    {
        CharacterManager m = GameController.g.Manager;// SM[i].Manager;

        if (controll.UpdatePosition())
        {
            controllNpc.ModificarOndeChegar(posFinalDoNpc.position,6);
            //animatorDoNPC.SetBool("apoieChao", true);
            state = TalkManagerState.movimentandoNpc;

        }  
        
    }

    void MovimentandoNpc()
    {
        if (controllNpc.UpdatePosition())
        {
            npc.Start(animatorDoNPC.transform);
            npc.IniciaConversa();
            state = TalkManagerState.conversando;
        }
    }

    private void OnValidate()
    {
        BuscadorDeID.Validate(ref ID, this);
    }    

    void IniciandoMovimentoControlado()
    {
        GlobalController.g.Musica.IniciarMusica(clip);

        CharacterManager m = GameController.g.Manager;

        GameController.EntrarNoFluxoDeTexto();
        m.Estado = EstadoDePersonagem.movimentoDeFora;

        controll = new ControlledMoveForCharacter(new CaracteristicasDeMovimentacao()
        {
            caracPulo = new CaracteristicasDePulo()
        }
            , m.transform);

        controllNpc = new ControlledMoveForCharacter(new CaracteristicasDeMovimentacao()
        {
            caracPulo = new CaracteristicasDePulo()
        }
            , animatorDoNPC.transform);

        controll.ModificarOndeChegar(alvos.position, 6);

        animatorDoNPC.transform.position = posInicialDoNpc.position;

        state = TalkManagerState.movimentandoHeroi;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!GameController.g.MyKeys.VerificaAutoShift(ID))
            {
                IniciandoMovimentoControlado();
            }
            else
            {
                Destroy(gameObject);
            }
        }

    }
}
