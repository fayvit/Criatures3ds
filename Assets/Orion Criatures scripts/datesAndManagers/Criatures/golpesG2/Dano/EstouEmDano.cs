using UnityEngine;
using System.Collections;

public class EstouEmDano : MonoBehaviour
{

    private float tempoDeDano = 0;
    private float alturaAtual;
    private float alturaDoDano;
    private Vector3 direcao = Vector3.zero;
    private Vector3 vMove = Vector3.zero;
    private Vector3 posInicial;
    

    private CharacterController controle;

    public IGolpeBase esseGolpe;
    public Animator animator;
    public CreatureManager gerente;
    //public Vector3 direcaoDoDano = Vector3.back;

    // Use this for initialization
    void Start()
    {
        controle = GetComponent<CharacterController>();
        posInicial = transform.position;
        alturaDoDano = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        tempoDeDano += Time.deltaTime;

        alturaAtual = transform.position.y;
        direcao = Vector3.zero;
        if (alturaAtual < alturaDoDano + 0.5f)
        {
            direcao += 12 * Vector3.up;
        }
        if ((transform.position - posInicial).sqrMagnitude < esseGolpe.DistanciaDeRepulsao)
            direcao += esseGolpe.VelocidadeDeRepulsao * esseGolpe.DirDeREpulsao;//direcaoDoDano;

        vMove = Vector3.Lerp(vMove, direcao, 10 * Time.deltaTime);
        controle.Move(vMove * Time.deltaTime);

        if (tempoDeDano > esseGolpe.TempoNoDano)
        {
            gerente.LiberaMovimento(CreatureManager.CreatureState.emDano);    
            animator.SetBool("dano1", false);
            Destroy(this);
        }
    }

    
}
