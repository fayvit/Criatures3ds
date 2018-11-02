using UnityEngine;
using System.Collections;

public class MbComportamentoDoMultiplicado : ColisorDeDanoBase
{

    public Transform alvo;
    public Vector3 direcaoMovimento;
    public float tempoDestroy = 10;

    private bool alvoFoiNaoNulo = false;
    private CharacterController controle;
    private Animator animator;

    private float tempoAcumulado = 0;
    private CreatureManager C;

    // Use this for initialization
    void Start()
    {
        controle = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        
        if (dono.name == "CriatureAtivo")
        {
            C = GameController.g.Manager.CriatureAtivo;
           
        }
        else
        {
            C = GameController.g.InimigoAtivo;
            
        }
        noImpacto = "impactoDeGosma";
    }

    bool podeAtualizar()
    {
        return
            C.Estado != CreatureManager.CreatureState.morto &&
            C.Estado != CreatureManager.CreatureState.parado;
            ;
    }

    // Update is called once per frame
    void Update()
    {
        
        tempoAcumulado += Time.deltaTime;
        

        if (podeAtualizar())
        {
            if (!alvo)
                direcaoMovimento = transform.forward;
            else
            {
                alvoFoiNaoNulo = true;
                if (Vector3.Distance(transform.position, alvo.position) > 2.5f)
                {
                    direcaoMovimento = Vector3.Slerp(direcaoMovimento, alvo.position - transform.position, 0.9f * Time.deltaTime);
                    direcaoMovimento.Normalize();
                }
                else
                    direcaoMovimento = (alvo.position - transform.position).normalized;
            }

            controle.Move(direcaoMovimento * velocidadeProjetil * Time.deltaTime);
            
            animator.SetFloat("velocidade", controle.velocity.magnitude);

            transform.rotation = Quaternion.Lerp(transform.rotation,
                                                 Quaternion.LookRotation(direcaoMovimento),
                                                 Time.deltaTime * 5);
        }
        if (C.MeuCriatureBase.CaracCriature.meusAtributos.PV.Corrente <= 0 || tempoAcumulado > tempoDestroy 
            ||(alvo==null && alvoFoiNaoNulo))
            meDestrua();
    }

    void meDestrua()
    {
        Destroy(
            Instantiate(GameController.g.El.retorna("impactoDeGosma"), transform.position + Vector3.up,
                    Quaternion.LookRotation(transform.forward)),
            2);

        Destroy(gameObject, 2.1f);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider hit)
    {
        
        if (hit.gameObject.tag!="cenario"&& hit.gameObject.layer != 10 && hit.gameObject != dono)
        {
            //print(hit.gameObject.name+" hit do multiplicado");
            Qparticles = Quaternion.LookRotation(transform.forward);
            funcaoTrigger(hit);
            meDestrua();
        }

    }

}
