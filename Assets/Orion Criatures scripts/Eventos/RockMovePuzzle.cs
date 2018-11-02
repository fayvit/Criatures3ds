using UnityEngine;

public class RockMovePuzzle:EventoComGolpe
{
    [SerializeField]private float vel = 2;
    [SerializeField]private float smooth = 2;
    [SerializeField]private Transform posFinalizado;

    private float yInicial = 0;
    private float smoothVel = 0;
    private Vector3 dirDeMove = Vector3.zero;
    private Transform umAlvo;
    private CharacterController controle;
    private RockMoveState estado = RockMoveState.emEspera;

    private RockMovePuzzleManager rockManager;

    public RockMovePuzzleManager RockManager
    {
        get { return rockManager; }
        set { rockManager = value; }
    }

    public bool Feito
    {
        get { return estado == RockMoveState.feito; }
    }

    private enum RockMoveState
    {
        emEspera,
        movimentacao,
        parar,
        finalizar,
        feito
    }

    void Start()
    {
        yInicial = transform.position.y;
        if (ExistenciaDoController.AgendaExiste(Start, this))
        {
            if (GameController.g.MyKeys.VerificaAutoShift(Chave))
            {
                if (rockManager)
                {
                    rockManager.Realocar(this);
                    estado = RockMoveState.feito;
                }
                else
                    Invoke("Start", 0.15f);
            }
        }
    }

    new void Update()
    {
        switch (estado)
        {
            case RockMoveState.movimentacao:
                smoothVel = Mathf.Lerp(smoothVel, vel, Time.deltaTime * smooth);
                controle.Move(dirDeMove*Time.deltaTime*vel);
            break;
            case RockMoveState.parar:
                smoothVel = 0;
                Destroy(controle);
                transform.position = new Vector3(transform.position. x, yInicial, transform.position.z);
                estado = RockMoveState.emEspera;
                Destroy(
                Instantiate(GameController.g.El.retorna(DoJogo.poeiraAoVento),transform.position,Quaternion.identity),5);
                Invoke("TempoDeRetorno", 1);
            break;
            case RockMoveState.finalizar:
                Destroy(controle);
                transform.position = new melhoraPos().novaPos(umAlvo.position + Vector3.up);
                Invoke("TempoDeRetorno", 1);
                estado = RockMoveState.feito;
            break;     
        }
    }

    void TempoDeRetorno()
    {
        if (!rockManager.TodosFeitos())
        {
            GameController.g.Manager.AoCriature();
            //GameController.g.HudM.ligarControladores();
            //AndroidController.a.LigarControlador();
        }
        else
        {
            GameController.g.Manager.CriatureAtivo.Estado = CreatureManager.CreatureState.parado;
        }
        
    }

    void CalculaDirecaoDeMove()
    {
        Vector3 dir = (GameController.g.Manager.CriatureAtivo.transform.position - transform.position).normalized;
        Vector3 min = Vector3.forward;
        float ultimoDot = Vector3.Dot(min,dir);

        MenorDot(ref min, ref ultimoDot, dir, Vector3.right);
        MenorDot(ref min, ref ultimoDot, dir, -Vector3.right);
        MenorDot(ref min, ref ultimoDot, dir, -Vector3.forward);
        /*
        MenorDot(ref min, ref ultimoDot, dir, (Vector3.forward+Vector3.right).normalized);
        MenorDot(ref min, ref ultimoDot, dir, (-Vector3.forward + Vector3.right).normalized);
        MenorDot(ref min, ref ultimoDot, dir, (-Vector3.forward - Vector3.right).normalized);
        MenorDot(ref min, ref ultimoDot, dir, (Vector3.forward - Vector3.right).normalized);
        */

        Debug.Log(min+" : "+ultimoDot);

        dirDeMove = -min;
        AplicadorDeCamera.cam.NovoFocoBasico(transform, 10, 10,true);
        IniciaMovimentacao();
    }

    void IniciaMovimentacao()
    {
        estado = RockMoveState.movimentacao;
        controle = gameObject.AddComponent<CharacterController>();
    }

    void MenorDot(ref Vector3 min,ref float ultimoDot,Vector3 dir, Vector3 quem)
    {
        if (Vector3.Dot(dir, quem) > ultimoDot)
        {
            min = quem;
            ultimoDot = Vector3.Dot(dir, quem);
        }
    }

    public override void DisparaEvento(nomesGolpes nomeDoGolpe)
    {
        if (EsseGolpeAtiva(nomeDoGolpe) && !GameController.g.MyKeys.VerificaAutoShift(Chave))
        {
            FluxoDeBotao();
            CalculaDirecaoDeMove();
        }
    }

    public void RestauraShift()
    {
        GameController.g.MyKeys.MudaAutoShift(Chave,false);
        estado = RockMoveState.emEspera;
    }

    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "gatilhoDePuzzle")
            if (rockManager.VerificaTargetOcupado(hit.transform,transform))
            {
                Debug.Log("gatilhoDePuzzle");
                umAlvo = hit.transform;
                Destroy(
                Instantiate(GameController.g.El.retorna("teletransporte"), hit.transform.position, Quaternion.identity), 5);
                GameController.g.MyKeys.MudaAutoShift(Chave, true);
                estado = RockMoveState.finalizar;
            }
            else
            {
                Debug.Log("é um gatilho OCupado");
            }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (Vector3.ProjectOnPlane(hit.normal, Vector3.up).magnitude > 0.5f 
            && estado == RockMoveState.movimentacao)
        {
            Debug.Log("Parece uma parede");
            estado = RockMoveState.parar;
        }
    }

    public override void FuncaoDoBotao()
    {
        throw new System.NotImplementedException();
    }
}

