using UnityEngine;
using System.Collections;

[System.Serializable]
public class EscondeCoisa
{
    [SerializeField] private Transform escondivel;
    [SerializeField] private Vector3 posDeEscondido;
    [SerializeField] private Vector3 posInicial;
    [SerializeField] private GameObject particulaDeAcao;
    [SerializeField] private KeyShift chaveEspecial = KeyShift.nula;
    [SerializeField] private string chave = "";
    [SerializeField] private float tempoParaEsconder = 4;
    [SerializeField] private int numeroDePartuiculas = 10;
    

    private float contadorDeTempo = 0;
    private int particulasInstanciadas = 0;

    // Use this for initialization
    public void Start()
    {
        if (escondivel)
        {
            posInicial = escondivel.position;
            if (ExistenciaDoController.AgendaExiste(Start, GameObject.FindObjectOfType<MonoBehaviour>()))
            {
                if (GameController.g.MyKeys.VerificaAutoShift(chave))
                {
                    escondivel.gameObject.SetActive(false);
                }
            }
        }
        else
            Debug.LogWarning("Escondivel não setado");
    }

    public void AtivarParticula()
    {
        contadorDeTempo = 0;
        particulasInstanciadas = 0;
        GameObject G = MonoBehaviour.Instantiate(particulaDeAcao, particulaDeAcao.transform.position, particulaDeAcao.transform.rotation,escondivel.parent);
        MonoBehaviour.Destroy(G, 5);
        G.SetActive(true);
    }

    // Update is called once per frame
    public bool Update()
    {
        contadorDeTempo += Time.deltaTime;
        escondivel.position = Vector3.Lerp(posInicial, posDeEscondido, contadorDeTempo / tempoParaEsconder);

        if (contadorDeTempo>particulasInstanciadas*tempoParaEsconder/numeroDePartuiculas)
        {
            particulasInstanciadas++;
            GameObject G = MonoBehaviour.Instantiate(particulaDeAcao, particulaDeAcao.transform.position, particulaDeAcao.transform.rotation,escondivel.parent);
            MonoBehaviour.Destroy(G, 5);
            G.SetActive(true);
        }

        if (contadorDeTempo > tempoParaEsconder)
        {
            //GameController.g.Manager.AoCriature();
            //GameController.g.HudM.ligarControladores();
            //AndroidController.a.LigarControlador();
            GameController.g.MyKeys.MudaAutoShift(chave, true);
            GameController.g.MyKeys.MudaShift(chaveEspecial, true);
            FinalizaEspecifico();
            escondivel.gameObject.SetActive(false);
            
            return true;
        }
            return false;
    }

    protected virtual void FinalizaEspecifico()
    {

    }
}
