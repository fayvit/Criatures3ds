using UnityEngine;
using System.Collections;

public class VisaoDeTreinadorEspecial : MonoBehaviour
{
    [SerializeField] private KeyShift chave = KeyShift.nula;
    [SerializeField] private Transform npc;
    [SerializeField] private MovimentoDeCamera[] movs;
    [SerializeField] private AtivadorDaLutaComTreinador ativaLut;
    [SerializeField] private float velocidadeTempoDeCamera = 0.75f;

    private NpcLutaContraTreinador npcLuta;
    private KeyVar keys;
    private GameController g;
    private Vector3 dirDeMove;
    private EstadoDaVisao estado = EstadoDaVisao.emEspera;
    private Vector3 posInicialDeMoveCamera = default(Vector3);
    private float contadorDetempo = 0;

    private enum EstadoDaVisao
    {
        emEspera,
        iniciou,
        segundoPasso,
        entraNaConversa,
        conversaComtreinador
    }
    // Use this for initialization
    void Start()
    {
        if (ExistenciaDoController.AgendaExiste(Start, this))
        {
            npcLuta = ativaLut.NPC_Luta;
            g = GameController.g;
            keys = g.MyKeys;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (estado)
        {
            case EstadoDaVisao.iniciou:
               
                if(Vector3.Distance(g.Manager.transform.position,npc.position)>5)
                    g.Manager.Mov.AplicadorDeMovimentos(dirDeMove);
                else
                    g.Manager.Mov.AplicadorDeMovimentos(Vector3.zero);

                if (AplicadorDeCamera.cam.FocarPonto(velocidadeTempoDeCamera, movs[0].distanciaDaCamera, movs[0].alturaDaCamera, true, posInicialDeMoveCamera,true))
                {
                    estado = EstadoDaVisao.segundoPasso;
                    AplicadorDeCamera.cam.InicializaCameraExibicionista(movs[1].AlvoDoMovimento,movs[1].alturaDaCamera,true);
                    contadorDetempo = 0;
                    posInicialDeMoveCamera = AplicadorDeCamera.cam.transform.position;
                }
            break;
            case EstadoDaVisao.segundoPasso:
                contadorDetempo += Time.deltaTime;
                if (contadorDetempo > 1.5f)
                {
                    if (Vector3.Distance(g.Manager.transform.position, npc.position) > 5)
                        g.Manager.Mov.AplicadorDeMovimentos(dirDeMove);
                    else
                        g.Manager.Mov.AplicadorDeMovimentos(Vector3.zero);

                    if (AplicadorDeCamera.cam.FocarPonto(velocidadeTempoDeCamera, movs[1].distanciaDaCamera, movs[1].alturaDaCamera, true, posInicialDeMoveCamera,true)
                        &&
                        Vector3.Distance(g.Manager.transform.position, npc.position) < 5
                        )
                    {
                        g.Manager.Mov.AplicadorDeMovimentos(Vector3.zero);
                        g.Manager.Estado = EstadoDePersonagem.parado;
                        estado = EstadoDaVisao.conversaComtreinador;
                        npcLuta.Start(npc);
                        npcLuta.IniciaConversa();
                    }
                }
            break;
            case EstadoDaVisao.conversaComtreinador:
                if (npcLuta.Update())
                {
                    estado = EstadoDaVisao.emEspera;
                    g.Manager.AoHeroi();
                }
            break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (keys.VerificaAutoShift(chave))
            Destroy(gameObject);
        else
        {
            if (estado == EstadoDaVisao.emEspera)
            {
                if (other.tag == "Player")
                {
                    g.ContarPassos = false;
                    dirDeMove = (npc.position - other.transform.position).normalized;
                    GameController.EntrarNoFluxoDeTexto();
                    g.Manager.Estado = EstadoDePersonagem.movimentoDeFora;
                    Collider esseCol = GetComponent<Collider>();
                    AplicadorDeCamera.cam.InicializaCameraExibicionista(movs[0].AlvoDoMovimento, movs[0].alturaDaCamera, true);
                    posInicialDeMoveCamera = AplicadorDeCamera.cam.transform.position;
                    esseCol.enabled = false;
                    esseCol.isTrigger = false;
                    estado = EstadoDaVisao.iniciou;
                    keys.MudaShift(chave, true);
                }
                else if (other.tag == "Criature"&& !GameController.g.estaEmLuta)
                {
                    EvitaAvancarNoTrigger.Evita();
                }
            }
        }
    }
}
