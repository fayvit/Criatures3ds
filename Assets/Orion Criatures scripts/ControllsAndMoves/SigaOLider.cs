using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[System.Serializable]
public class SigaOLider
{
    [SerializeField]private NavMeshAgent nav;
    [SerializeField]private Animator animator;

    float modVelocidade = 1;
    float velocidadeOriginal;

    public NavMeshAgent Nav
    {
        get { return nav; }
    }

    public float ModVelocidade
    {
        set { modVelocidade = value;
            nav.speed = velocidadeOriginal * modVelocidade;
        }
    }

    public SigaOLider() { }

    public SigaOLider(Transform T,float velocidade,float aceleracao,float baseOffset = -0.09f)
    {
        nav = T.GetComponent<NavMeshAgent>();

        velocidadeOriginal = velocidade;

        if (nav == null)
        {
            nav = T.gameObject.AddComponent<NavMeshAgent>();
            nav.stoppingDistance = 2.2f;
            nav.acceleration = aceleracao;
            nav.speed = velocidade;
            nav.baseOffset = baseOffset;
        }

        animator = T.GetComponent<Animator>();
    }

    // Use this for initialization
    public void Start(CreatureManager meuCriature)
    {
        nav = meuCriature.GetComponent<NavMeshAgent>();
        if (nav == null)
        {
            nav = meuCriature.gameObject.AddComponent<NavMeshAgent>();
            nav.stoppingDistance = nav.radius < 1 ? 5 : 2 * nav.radius;
            nav.baseOffset = 0;
            nav.speed = meuCriature.MeuCriatureBase.Mov.velocidadeCorrendo;//nav.radius < 1 ? 7 : 0;
            nav.baseOffset = meuCriature.MeuCriatureBase.CaracCriature.distanciaFundamentadora;
            nav.acceleration = 1.75f*nav.speed;
        }
        animator = meuCriature.GetComponent<Animator>();
        velocidadeOriginal = nav.speed;
    }

    // Update is called once per frame
    public void Update(Vector3 posAlvo)
    {
        if (nav)
        {
            if (nav.enabled && nav.isOnNavMesh)
            {
                
                //if(!nav.isActiveAndEnabled)
                    nav.Resume();

               // if(nav.isActiveAndEnabled)
                    nav.destination = posAlvo;
                animator.SetFloat("velocidade", Vector3.ProjectOnPlane(nav.velocity, Vector3.up).magnitude);
            }
        }
    }

    public void PareAgora()
    {
        //nav.destination = nav.transform.position;
        animator.SetFloat("velocidade", 0);
        if (nav.enabled)
        {
            nav.destination = nav.transform.position;
            nav.Stop();
        }
    }
}
