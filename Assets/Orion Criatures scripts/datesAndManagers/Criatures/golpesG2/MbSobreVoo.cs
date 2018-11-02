using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class MbSobreVoo : GolpeBase
{
    private CaracteristicasDeImpacto carac = new CaracteristicasDeImpacto()
    {
        noImpacto = NoImpacto.impactoDeVento.ToString(),
        nomeTrail = Trails.umCuboETrail.ToString(),
        parentearNoOsso = true
    };
    private bool addView = false;
    private bool procurouAlvo = false;
    private bool ligaNav = false;
    private float tempoDecorrido = 0;    
    private float velocidadeSubindo = 20;
    [System.NonSerialized]private CharacterController controle;
    [System.NonSerialized]private Transform alvoProcurado;
    [System.NonSerialized]private NavMeshAgent nav;
    [System.NonSerialized]private Vector3 posInicial;

    public MbSobreVoo() : base(new ContainerDeCaracteristicasDeGolpe()
    {
        nome = nomesGolpes.sobreVoo,
        tipo = nomeTipos.Voador,
        carac = caracGolpe.colisaoComPow,
        custoPE = 3,
        potenciaCorrente = 7,
        potenciaMaxima = 14,
        potenciaMinima = 3,
        tempoDeReuso = 7.5f,
        TempoNoDano = 0.75f,
        velocidadeDeGolpe = 30f,
        distanciaDeRepulsao = 65f,
        velocidadeDeRepulsao = 126,
        tempoDeMoveMin = 0,//74
        tempoDeMoveMax = 1f,
        tempoDeDestroy = 1.85f,
        colisorScale = 4
    }
        )
    {

    }

    public override void IniciaGolpe(GameObject G)
    {
        addView = false;
        tempoDecorrido = 0;
        procurouAlvo = false;
        posInicial = G.transform.position;
        DirDeREpulsao = G.transform.forward;

        AnimadorCriature.AnimaAtaque(G, Nome.ToString());
        
        MonoBehaviour.Destroy(
        MonoBehaviour.Instantiate(
                GameController.g.El.retorna("subindoSobreVoo"),
                G.transform.position,
            Quaternion.identity),0.5f);

        nav = G.GetComponent<NavMeshAgent>();
        ligaNav = nav.enabled;
        nav.enabled = false;
    }

    public override void UpdateGolpe(GameObject G)
    {
        tempoDecorrido += Time.deltaTime;
        if (tempoDecorrido < TempoDeMoveMax)
        {
            if (!controle)
                controle = G.GetComponent<CharacterController>();

            controle.Move(Vector3.up * velocidadeSubindo * Time.deltaTime);
        }

        if (tempoDecorrido > TempoDeMoveMax && !procurouAlvo)
        {
            if (!procurouAlvo)
                alvoProcurado = CriaturesPerto.procureUmBomAlvo(G, 100);

            if(alvoProcurado)
                if (Vector3.SqrMagnitude(
                    Vector3.ProjectOnPlane(G.transform.position - alvoProcurado.position, Vector3.up)) < 25)
                {
                    alvoProcurado = null;
                }

            procurouAlvo = true;

            Debug.Log("Sobre voo, alvo procurado: " + alvoProcurado);
        }

        if (tempoDecorrido > TempoDeMoveMax && tempoDecorrido < TempoDeDestroy)
        {
            //if (!procurouAlvo)
             //   alvoProcurado = CriaturesPerto.procureUmBomAlvo(G, 100);

            if (!addView)
            {
                ColisorDeGolpe.AdicionaOColisor(G, this, carac, tempoDecorrido);
                addView = true;
            }

            Vector3 dir = G.transform.forward + 0.5f * Vector3.down;


            if (alvoProcurado)
                if (Vector3.ProjectOnPlane(posInicial - alvoProcurado.position, Vector3.up).sqrMagnitude > 25)
                    dir = alvoProcurado.position - G.transform.position + 0.5f * Vector3.down;

            dir.Normalize();

            controle.Move(dir * VelocidadeDeGolpe  * Time.deltaTime);
        }
    }

    public override void FinalizaEspecificoDoGolpe()
    {
        nav.enabled = ligaNav;   
    }
}

